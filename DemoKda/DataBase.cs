using System;
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


        //Update Queries
        public void updateEmp(string id, string name,string salary,string birth,string dep)
        {
            string query = $"exec updateEmp {id},{name},{salary},'{birth}',{dep} ";
            exec(query);
        }

        public void updateSec(string id, string name)
        {
            string query = $"exec updateSec {id},{name} ";
            exec(query);
        }

        public void updateDep(string id, string name)
        {
            string query = $"exec updateDep {id},{name} ";
            exec(query);
        }

        public void updateProj(string id, string name,string st,string end)
        {
            string query = $"exec updateProj {name},'{st}','{end}',{id} ";
            exec(query);
        }

        //Delete Queries
        public void removeEmp(string id)
        {
            string query = $"exec removeEmp {id} ";
            exec(query);
        }

        public void removeProjEmp(string id,string proj)
        {
            string query = $"exec removeProjEmp {id},{proj} ";
            exec(query);
        }

        public void removeProjDep(string id, string proj)
        {
            string query = $"exec removeProjDep {id},{proj} ";
            exec(query);
        }

        public void removeDep(string id)
        {
            string query = $"exec removeDep {id} ";
            exec(query);
        }

        public void removeProj(string id)
        {
            string query = $"exec removeProj {id} ";
            exec(query);
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

        public void insertEmp(string name, string salary, string birth, string id)
        {
            string query = $"exec insertEmp {name},{salary},'{birth}',{id} ";
            exec(query);
        }

        public void insertProj(string name, string st, String end)
        {
            string query = $"exec insertProj {name},'{st}','{end}' ";
            exec(query);
        }

        public void insertProjDep(string proj, string dep)
        {
            string query = $"exec insertProjDep {proj},{dep} ";
            exec(query);
        }

        public void assignEmp(string id,string proj, string dep)
        {
            string query = $"exec assignEmp {id},{proj},{dep} ";
            exec(query);
        }

        private void exec(string query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        // Fetching Quries
        public int countProjs(string id)
        {
            string query = $"exec countProjs {id}";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int ans = Int32.Parse(reader["num"].ToString());
            reader.Close();
            return ans;
        }

        public DataTable fetchProj()
        {
            string[] cols = {"ID","Name", "Duration", "DailyCost", "TotalCost" , "IsActive", "StDate", "EndDate" };
            string query = $"exec fetchProj";
            return getRecords(query, cols);
        }


        public DataTable fetchEmps( string dep)
        {

            string[] cols = { "ID", "Name", "Salary","Age", "BirthDate" };
            string query = $"exec fetchEmps {dep}";

            return getRecords(query, cols);

        }

        public DataTable fetchProjEmps(string pro)
        {

            string[] cols = {"ID","Name", "department", "DailyRate" };
            string query = $"exec fetchProjEmps2 {pro}" ;

            return getRecords(query, cols);

        }

        public DataTable fetchProjEmps(string pro,string dep)
        {

            string[] cols = { "ID", "Name", "DailyRate" };
            string query = $"exec fetchProjEmps {pro},{dep}";

            return getRecords(query, cols);

        }
        public DataTable fetchNoProjEmps(string pro, string dep)
        {

            string[] cols = { "ID","Name", "DailyRate" };
            string query = $"exec fetchNoProjEmps {pro},{dep}";

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
            string[] cols = { "ID", "Name", "Emp#" };
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