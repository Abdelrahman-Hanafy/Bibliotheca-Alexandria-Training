﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoKda
{
    public class DataBase
    {

        private static DataBase DB;
        private SqlConnection conn;

        private DataBase()
        {

            string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            //create instanace of database connection
            conn = new SqlConnection(connStr);

            //open connection
            conn.Open();

        }

        public static DataBase ConnectDB()
        {
            if(DB == null)
            {
                DB = new DataBase();
            }

            return DB;
        }
                

        // Insertion Queirs
        public void insertSec(string name)
        {
            string query = $"exec insertSec {name} ";
            exec(query);
        }

        public void insertDep(string name,string id)
        {
            string query = $"exec insertDep {name},{id} ";
            exec(query);
        }

        private void exec(string query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        // Fetching Quries
        public DataTable fetchProj()
        {
            string[] cols = {"ID","Name", "Duration", "DailyCost", "TotalCost" };
            string query = $"exec fetchProj";
            return getRecords(query, cols);
        }


        public DataTable fetchEmps( string dep)
        {

            string[] cols = { "ID", "Name", "Salary","Age" };
            string query = $"exec fetchEmps {dep}";

            return getRecords(query, cols);

        }

        public DataTable fetchProjEmps(string pro)
        {

            string[] cols = {"Name", "department", "DailyRate" };
            string query = $"exec fetchProjEmps2 {pro}" ;

            return getRecords(query, cols);

        }

        public DataTable fetchProjEmps(string pro,string dep)
        {

            string[] cols = { "Name", "DailyRate" };
            string query = $"exec fetchProjEmps {pro},{dep}";

            return getRecords(query, cols);

        }
        public DataTable fetchDeps()
        {
            string[] cols = { "ID", "Name"};
            string query = $"Select * from [DepartmentsData] ";
            return getRecords(query, cols);
        }

        public DataTable fetchDeps(string sec)
        {
            string[] cols = {"ID" ,"Name","Emp#" };
            string query = $"exec fetchDeps {sec}";
            return getRecords(query, cols);
        }

        public DataTable fetchdepProj(string pro)
        {
            string[] cols = { "ID", "Name" };
            string query = $"exec fetchdepProj {pro}";
            return getRecords(query, cols);
        }

        public DataTable fetchSectors()
        {
            string[] cols = {"ID","Name"};
            string query = "exec fetchSectors";
            return getRecords(query, cols);
        }


        public DataTable getRecords(string query, string[] cols)
        {
            DataTable dt = new DataTable();

            foreach(string col in cols)
            {
                dt.Columns.Add(col);
            }

            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    DataRow r = dt.NewRow();
                    foreach(string col in cols)
                    {
                        r[col] = reader[col].ToString();
                    }
                    dt.Rows.Add(r);
                    
                }
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }

            return dt;

        }
    }

}