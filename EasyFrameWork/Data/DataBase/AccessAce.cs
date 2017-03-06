/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;

namespace Easy.Data.DataBase
{
    public class AccessAce : Access
    {
        public AccessAce()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
            if (!ConnectionString.Contains(":"))
            {
                ConnectionString = AppDomain.CurrentDomain.BaseDirectory + ConnectionString;
            }
        }
        protected override DbConnection GetDbConnection()
        {
            return
                new OleDbConnection(
                    string.Format(
                        "{1};Data Source={0};persist security info=false;Jet OLEDB:Database Password=easyframework",
                        ConnectionString, "Provider=Microsoft.ACE.OLEDB.12.0;"));
        }
        public override IEnumerable<string> DataBaseTypeNames()
        {
            yield return "Access-Ace";
        }
    }
}