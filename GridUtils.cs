using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetMgmt
{
    public class GridUtils
    {
        [Serializable]
        public class NameStruct
        {
            private string _name;
            public string Name { get { return _name; } set { _name = value; } }
        }



        public string serializeSimpleGrid(List<NameStruct> l)
        {
            if (l == null) return "";

            List<string> temp = new List<string>();
            foreach (NameStruct n in l)
            {
                if (!n.Name.Trim().Equals("")) { temp.Add(n.Name.Trim()); }
            }
            temp.Sort();
            return String.Join("#", temp.ToArray());
        }


        public class FolderAccessStruct
        {
            private string _name;
            public string Name { get { return _name; } set { _name = value; } }

            private string _folder;
            public string Folder { get { return _folder; } set { _folder = value; } }

            private string _am;
            public string AM { get { return _am; } set { _am = value; } }
            
            //private string _addremove;
            //public string AddRemove { get { return _addremove; } set { _addremove  = value; } }
            private string _rights;
            public string Rights { get { return _rights; } set { _rights = value; } }
        }


        [Serializable]
        public class FolderAccessStructV2
        {
            private string _name;
            public string Name { get { return _name; } set { _name = value; } }

            
            private string _am;
            public string AM { get { return _am; } set { _am = value; } }

            //private string _addremove;
            //public string AddRemove { get { return _addremove; } set { _addremove  = value; } }
            private string _rights;
            public string Rights { get { return _rights; } set { _rights = value; } }
        }

        public string serializeFolderAccessGrid(List<FolderAccessStruct> l)
        {
            if (l == null) return "";
            List<string> temp = new List<string>();
            foreach (FolderAccessStruct n in l)
            {
                if (!n.Name.Trim().Equals("")) { temp.Add(n.Name + "^^" + n.AM + "^^" + n.Folder + "^^"  + n.Rights); }
            }
            temp.Sort();
            return String.Join("##", temp.ToArray());
        }

        public List<FolderAccessStruct> deserializeFolderAccessGrid(string grid)
        {
            List<FolderAccessStruct> rs = new List<FolderAccessStruct>();
            foreach (string row in grid.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries))
            {
                FolderAccessStruct rowStruct = new AssetMgmt.GridUtils.FolderAccessStruct();
                string[] vals = row.Split(new string[] { "^^" }, StringSplitOptions.None);
                rowStruct.Name = vals[0];
                rowStruct.AM = vals[1];
                rowStruct.Folder = vals[2];
                //rowStruct.AddRemove = vals[3];
                rowStruct.Rights = vals[3]; 
                rs.Add(rowStruct);
            }
            return rs;
        }


        public string serializeFolderAccessGridV2(List<FolderAccessStructV2> l)
        {
            if (l == null) return "";
            List<string> temp = new List<string>();
            foreach (FolderAccessStructV2 n in l)
            {
                if (!n.Name.Trim().Equals("")) { temp.Add(n.Name + "^^" + n.AM + "^^" +   n.Rights); }
            }
            temp.Sort();
            return String.Join("##", temp.ToArray());
        }

        public List<FolderAccessStructV2> deserializeFolderAccessGridV2(string grid)
        {
            List<FolderAccessStructV2> rs = new List<FolderAccessStructV2>();
            foreach (string row in grid.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries))
            {
                FolderAccessStructV2 rowStruct = new AssetMgmt.GridUtils.FolderAccessStructV2();
                string[] vals = row.Split(new string[] { "^^" }, StringSplitOptions.None);
                rowStruct.Name = vals[0];
                rowStruct.AM = vals[1];
                rowStruct.Rights = vals[2];
                rs.Add(rowStruct);
            }
            return rs;
        }
    }
}