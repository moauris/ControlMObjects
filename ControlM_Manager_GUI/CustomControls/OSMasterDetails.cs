using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;

namespace ControlM_Manager_GUI.CustomControls
{
    public class OSArchitecture
    {
        public OSArchitecture(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public override string ToString() => Name;
    }

    public class OSArchitectureList : ObservableCollection<OSArchitecture> { }
    public class OSVersionDetail
    {
        public OSVersionDetail(string name)
        {
            Name = name;
            osArchitectureList = new OSArchitectureList();
        }
        public string Name { get; set; }
        public OSArchitectureList osArchitectureList { get; set; }
        public override string ToString() => Name;
    }
    public class OSVersionList : ObservableCollection<OSVersionDetail> { }
    public class OSNameDetail
    {
        public OSNameDetail(string name)
        {
            Name = name;
            osVersionList = new OSVersionList();
        }
        public string Name { get; set; }
        public OSVersionList osVersionList { get; set; }
        public override string ToString() => Name;
    }
    public class OSNameList : ObservableCollection<OSNameDetail> 
    {
        /// <summary>
        /// Generate New OSNamelist from Database
        /// </summary>
        public OSNameList()
        {
            string conn_str = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\40137\source\repos\ControlMObjects\accessdb_driver\Assets\cmmgui_schema.accdb;Persist Security Info=True";

            using (OleDbConnection conn = new OleDbConnection(conn_str))
            {
                conn.Open();
                string strSQL = @"SELECT DISTINCT [os_name] FROM [tbl_osinfo];";
                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                DataSet dataset = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataset);
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    //Populating OSNames
                    string osname = row.ItemArray[0].ToString();
                    OSNameDetail n = new OSNameDetail(osname);
                    strSQL = @"SELECT DISTINCT [os_version] FROM [tbl_osinfo] WHERE os_name = @os_name;";
                    OleDbCommand select_cmd = new OleDbCommand(strSQL, conn);
                    select_cmd.Parameters.Add("@os_name", OleDbType.VarChar);
                    select_cmd.Parameters["@os_name"].Value = osname;
                    DataSet dataset1 = new DataSet();
                    OleDbDataAdapter adapter1 = new OleDbDataAdapter(select_cmd);
                    adapter1.Fill(dataset1);

                    OSVersionList v = new OSVersionList();
                    foreach (DataRow row1 in dataset1.Tables[0].Rows)
                    {
                        string osversion = row1.ItemArray[0].ToString();
                        Debug.Print($"Addeding OSVersion: {osversion}");
                        OSVersionDetail d = new OSVersionDetail(osversion);
                        d.osArchitectureList.Add(new OSArchitecture("32 bit"));
                        d.osArchitectureList.Add(new OSArchitecture("64 bit"));


                        v.Add(d);
                    }
                    n.osVersionList = v;
                    this.Add(n);
                }

            }
        }
    }
}
