using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint;

namespace AssetMgmt
{
    public class ReportUtils
    {
        public string Error = "";

        public void log(string msg, bool isError)
        {

        }

        //public class MailsByUser
        //{
        //    public string UserName;
        //    public List<string> mailsSendAs = new List<string>();
        //    public List<string> mailsForwardTo = new List<string>();

        //    public void addMail(string mail, bool forwardTo, bool sendAs)
        //    {
        //        if (forwardTo) { mailsForwardTo.Add(mail); }
        //        if (sendAs) { mailsSendAs.Add(mail); }
        //    }

        //    public void removeMail(string mail, bool forwardTo, bool sendAs)
        //    {
        //        if (forwardTo) { if (mailsForwardTo.Contains(mail)) mailsForwardTo.Remove(mail); }
        //        if (sendAs) { if (mailsSendAs.Contains(mail)) mailsSendAs.Remove(mail); }
        //    }
        //}
        //public Hashtable MailsByAllUsers; // hashed on mail 

        public class FoldersByUser
        {
            public string UserName;
            public List<string> folders = new List<string>();

            public void addMail(string mail)
            {
                folders.Add(mail);
            }

            public void removeMail(string folder)
            {
                if (folders.Contains(folder)) folders.Remove(folder);
            }
        }

        public Hashtable FoldersByAllUsers; // hashed on folder 


        public int getFirstID(SPList l, bool Ascending)
        {
            try
            {
                SPQuery qry = new SPQuery();
                string sqry = "<Where><Eq><FieldRef Name='_status'/><Value Type='Text'>closed</Value></Eq></Where>"; 
                 sqry += "<OrderBy><FieldRef Name='ID' Ascending='" + (Ascending ? "TRUE" : "FALSE") + "'/></OrderBy>";
                qry.Query = sqry; 
                // qry.RowLimit = 1;
                SPListItemCollection col = l.GetItems(qry);
                if (col.Count > 0) { return col[0].ID; } else { return 0; }
            }
            catch (Exception e) { Error += "getFirstID -->" + e.Message; return 0; }
        }


        /// <summary>
        /// MAILS REPORTS 
        /// </summary>
        public SPListItem getLastItemByEMailAccount(SPList l, string emailAccount)
        {
            try
            {
                Guid webID = l.ParentWeb.ID;
                Guid listID = l.ID;
                Guid siteID = l.ParentWeb.Site.ID;
                SPListItem itm = null;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite siteElevated = new SPSite(siteID))
                    {
                        using (SPWeb webElevated = siteElevated.OpenWeb(webID))
                        {
                            SPList listElevated = webElevated.Lists[listID];
                            SPQuery qry = new SPQuery();
                            string sqry = "<Eq> <FieldRef Name='_status'/><Value Type='Text'>closed</Value> </Eq>";
                            sqry = "<And>" + "<Eq> <FieldRef Name='EMailAddress'/><Value Type='Text'>" + emailAccount + "</Value> </Eq>" + sqry + "</And>";
                            sqry = "<Where>" + sqry + "</Where>";
                            sqry += "<OrderBy> <FieldRef Name='Implemented' Ascending='FALSE'/></OrderBy>";
                           
                            qry.Query = sqry;
                            // qry.ViewFields = "<FieldRef Name='Implemented'/><FieldRef Name='Title'/><FieldRef Name='Serial'/><FieldRef Name='Implemented'/>";

                            SPListItemCollection col = listElevated.GetItems(qry);
                            if (col.Count > 0) { itm = col[0]; };
                        }
                    }
                });
                return itm;
            }
            catch (Exception e) { Error += "getLastItemByEMailAccount -->" + e.Message; return null; }
        }





      
        struct LastItem
        {
            public int id;
            public DateTime implemented;
        }


        public Hashtable EMailAccountsByUsers(SPList l)
        {
            Hashtable rs = new Hashtable(); // <user as string>   string[1], [0] is SendAs1#SendAs2... , [1] is ForwardTo1#ForwardTo2 ... 
            Hashtable htLastItems = LastItemByEmailAccount(l);
            foreach (string key in htLastItems.Keys)
            {
                try
                {
                    int id = ((LastItem)htLastItems[key]).id;
                    SPListItem itm = l.GetItemById(id);
                    string emailAddress = (itm["EMailAddress"] ?? "").ToString();
                    string sendAsAll = (itm["SendAs"] ?? "").ToString();
                    string forwardToAll = (itm["ForwardTo"] ?? "").ToString();
                    foreach (string sendAs in sendAsAll.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (rs.ContainsKey(sendAs))
                        {
                            rs[sendAs] = emailAddress + "#" + rs[sendAs];
                        }
                        else { rs[sendAs] = emailAddress + "$"; }
                    }
                    foreach (string forwardTo in forwardToAll.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (rs.ContainsKey(forwardTo))
                        {
                            rs[forwardTo] = rs[forwardTo] + "#" + emailAddress;
                        }
                        else { rs[forwardTo] = "$" + emailAddress; }
                    }
                } catch (Exception e) { log("EMailAccountsByUser:" + e.Message, true);  }
            }
            return rs; 
        }

        public Hashtable UsersByEMailAccounts(SPList l)
        {
            Hashtable rs = new Hashtable(); // <email as string>   SendAs1#SendAs2...SendAsn$ForwardTo1 ... 
            Hashtable htLastItems = LastItemByEmailAccount(l);
           
            foreach (string key in htLastItems.Keys)
            {
                try
                {
                    LastItem lastItem = (LastItem)htLastItems[key];
                    SPListItem itm = l.GetItemById(lastItem.id);
                    string details = (itm["SendAs"] ?? "").ToString() + "$" + (itm["ForwardTo"] ?? "").ToString();
                    rs[(itm["EMailAddress"] ?? "").ToString()] = details;
                }
                catch (Exception e) { log("UsersByEMailAccounts:" + e.Message, true); }
            }
            return rs;
        }


        private Hashtable LastItemByEmailAccount(SPList l) // returns 
        {

            Hashtable lastRequests = new Hashtable();  // <email as string> LastIte    
            int firstID = getFirstID(l, true);
            int lastID = getFirstID(l, false);
            //take a hashtable with all last mails by email account ; 
            for (int i = firstID; i <= lastID; i++)
            {
                try
                {
                    SPListItem itm = l.GetItemById(i);
                    string _status = (itm["_status"] ?? "").ToString();
                    string emailAddress = (itm["EMailAddress"] ?? "").ToString();
                    if (_status.Equals("closed") && !emailAddress.Trim().Equals("") && !(itm["Implemented"] ?? "").ToString().Equals(""))
                    {
                        if (!lastRequests.ContainsKey(emailAddress))
                        {
                            try { lastRequests[emailAddress] = new LastItem() { id = itm.ID, implemented = (DateTime)itm["Implemented"] }; } catch { };
                        }
                        else
                        {
                            try
                            {
                                LastItem lastItem = (LastItem)lastRequests[emailAddress];
                                if (DateTime.Compare (lastItem.implemented, (DateTime)itm["Implemented"])<0) { lastRequests[emailAddress] = new LastItem() { id = itm.ID, implemented = (DateTime)itm["Implemented"] }; }
                                
                            }
                            catch { };
                        }
                    }
                }
                catch { };
            }
            return lastRequests;
        }
    }


}