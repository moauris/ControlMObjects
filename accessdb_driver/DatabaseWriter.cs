using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlM;
using System.Data.OleDb;
using System.Diagnostics;
using System.Data;

namespace CtlmDBDriver_Access
{
    public class DatabaseWriter
    {
        public static void WriteMachineInfo(ClientMachine clientMachine)
        {
            string conn_str = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\40137\source\repos\ControlMObjects\ControlMObjects\accessdb_driver\Assets\cmmgui_schema.accdb;Persist Security Info=True";

            Debug.Print(conn_str);
            using (OleDbConnection conn = new OleDbConnection(conn_str))
            {
                /*
                OleDbCommandBuilder cmd_Builder = new OleDbCommandBuilder();
                cmd_Builder.QuotePrefix = "'";
                cmd_Builder.QuoteSuffix = "'";
                */
                conn.Open();
                string insertCMDsql = @"INSERT INTO tbl_machines ([host_name], [domain], [ipv4address], [ipv6address], [osinfo]) VALUES (@host_name, @domain, @ipv4address, @ipv6address, @osinfo);";
                //insertCMDsql = @"INSERT INTO tbl_machines (ID, host_name, domain, ipv4address, ipv6address, osinfo) VALUES ('2', 'ctms00101', '.ibm.com.cn', '126.148.3.2', '2001:0:2851:b9f0:c57:8bfe:4b94:bc75', 'Windows 2018 Server');";
                OleDbCommand insert_cmd = new OleDbCommand(insertCMDsql, conn);
                
                insert_cmd.Parameters.Add("@host_name", OleDbType.VarChar);
                insert_cmd.Parameters.Add("@domain", OleDbType.VarChar);
                insert_cmd.Parameters.Add("@ipv4address", OleDbType.VarChar);
                insert_cmd.Parameters.Add("@ipv6address", OleDbType.VarChar);
                insert_cmd.Parameters.Add("@osinfo", OleDbType.VarChar);
                
                
                insert_cmd.Parameters["@host_name"].Value = clientMachine.Hostname;
                insert_cmd.Parameters["@domain"].Value = clientMachine.Domain;
                insert_cmd.Parameters["@ipv4address"].Value = clientMachine.IPv4.ToString();
                insert_cmd.Parameters["@ipv6address"].Value = clientMachine.IPv6.ToString();
                insert_cmd.Parameters["@osinfo"].Value = clientMachine.OSInformation.ToString();

                Debug.Print(insert_cmd.CommandText);
                insert_cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
}
