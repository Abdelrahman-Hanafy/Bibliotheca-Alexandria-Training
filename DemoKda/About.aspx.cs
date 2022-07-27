using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoKda
{
    public partial class About : Page
    {

        DataBase db;
        string defult = "<----- Please Select ----->";
        DataTable projTable;
        DataTable depTable;
        DataTable depProjTable;
        DataTable empDepProjTable;



        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();

            if (!IsPostBack)
            {
                projsddl.Items.Add(defult);
                projdepsddl.Items.Add(defult);

                projTable = db.fetchProj();
                depTable = db.fetchDeps();

                Session["projTable"] = projTable;
                Session["depTable"] = depTable;
                

            }
            else
            {
                projTable = (DataTable)Session["projTable"];
                depTable = (DataTable)Session["depTable"];
                depProjTable = (DataTable)Session["depProjTable"];
                empDepProjTable = (DataTable)Session["empDepProjTable"];

            }

            string[] cols = { "Name", "Duration", "DailyCost", "TotalCost" };
            _Default.View(projTable, projs,cols);

            foreach (DataRow row in projTable.Rows)
            {
                string record = row["Name"].ToString();
                if (projsddl.Items.FindByText(record) == null)
                    projsddl.Items.Add(record);
            }

        }

        protected void Projsddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            projdepsddl.Items.Clear();
            projdepsddl.Items.Add(defult);

            string id = _Default.getId(projsddl.SelectedValue,projTable);
            depProjTable = db.fetchdepProj(id);
            Session["depProjTable"] = depTable;

            string[] cols = { "Name"};
            _Default.View(depProjTable,Dets,cols);

            foreach (DataRow row in depProjTable.Rows)
            {
                string record = row["Name"].ToString();
                if (projdepsddl.Items.FindByText(record) == null)
                    projdepsddl.Items.Add(record);
            }

            if (projdepsddl.Items.FindByText("All") == null)
                projdepsddl.Items.Add("All");

        }


        protected void projdepsddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = _Default.getId(projsddl.SelectedValue,projTable);
            string[] cols;
            if (projdepsddl.SelectedValue == "All")
            {
                empDepProjTable = db.fetchProjEmps(id);
                 cols= new string[]{ "Name", "department", "DailyRate" };
            }
            else
            {
                string depId = _Default.getId(projdepsddl.SelectedValue, depTable);
                empDepProjTable = db.fetchProjEmps(id,depId);
                cols = new string[] { "Name", "DailyRate" };
            }

            
            _Default.View(empDepProjTable, emps,cols);
        }

    }
}