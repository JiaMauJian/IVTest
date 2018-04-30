using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Npgsql;
using System.Data;

namespace IVTest
{
    /// <summary>
    ///Service1 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {

        // http://boywhy.blogspot.tw/2014/12/ccpostgresql.html
        // http://pgfoundry.org/frs/?group_id=1000140&release_id=2106#_npgsql2-npgsql-2.2.3-title-content

        [WebMethod]
        public DataTable QueryData()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=172.20.40.116;Port=5409;User Id=postgres;Password=supersecret;Database=prolight;");
            conn.Open();

            string sql = @"SELECT * FROM ""DataServer"".""ComputationDone"" ORDER BY ""#id"" DESC LIMIT 100";
            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
            DataTable dt = new DataTable();
            dt.TableName = "IVTest";

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();                
                dt.Load(reader);                
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}