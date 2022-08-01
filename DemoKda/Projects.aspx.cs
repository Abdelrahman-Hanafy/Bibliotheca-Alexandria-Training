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

                updateProj();
                depTable = db.fetchDeps();
                Session["depTable"] = depTable;
                

            }
            else
            {
                projTable = (DataTable)Session["projTable"];
                depTable = (DataTable)Session["depTable"];
                depProjTable = (DataTable)Session["depProjTable"];
                empDepProjTable = (DataTable)Session["empDepProjTable"];
                projname = (string)Session["projname"];
                Delete.Text = "";
                Delete1.Text = "";
            }


        }

        protected void Projsddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDep();
        }


        protected void projdepsddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateEmp();
        }


        protected void Projs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = projs.Rows[e.RowIndex];

            if (row.Cells[6].Text != "0")
            {
                Delete1.Text = "The Project is still Acctive";
                Delete1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string n = row.Cells[2].Text;
            string id = _Default.getId(n, projTable);

            db.removeProj(id);
            //Page_Load( sender,  e);
            updateProj();
        }

        protected void Emps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = emps.Rows[e.RowIndex];
            string n = row.Cells[1].Text;
            string id = _Default.getId(n, empDepProjTable);
            string proj = _Default.getId(projsddl.SelectedValue, projTable);
            db.removeProjEmp(id,proj);
            updateEmp();
            updateDep();
            updateProj();

        }

        protected void Deps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = Dets.Rows[e.RowIndex];
            string n = row.Cells[1].Text;
            string id = _Default.getId(n, depProjTable);
            string proj = _Default.getId(projsddl.SelectedValue, projTable);

            if (row.Cells[2].Text != "0")
            {
                Delete.Text = "You Should MOVE all Employees First!";
                Delete.ForeColor = System.Drawing.Color.Red;
                return;
            }

            db.removeProjDep(id, proj);
            updateDep();

        }


        // OnEditing
        string projname;
        protected void Projs_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = projs.Rows[e.NewEditIndex];
            projname = row.Cells[2].Text;
            Session["projname"] = projname;
            projs.EditIndex = e.NewEditIndex;
            Projeditview();
        }
        protected void Projs_OnRowCancelingEdit(object sender, EventArgs e)
        {
            projs.EditIndex = -1;
            updateProj();

        }
        protected void Projs_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = projs.Rows[e.RowIndex];
            string n = (row.Cells[2].Controls[0] as TextBox).Text;
            string id = _Default.getId(projname, projTable);
            string st = (row.Cells[3].Controls[0] as TextBox).Text; 
            string end = (row.Cells[4].Controls[0] as TextBox).Text; 
            db.updateProj(id, n,st,end);
            
            projs.EditIndex = -1;
            updateProj();
        }

        private void Projeditview()
        {
            projTable = db.fetchProj();
            Session["projTable"] = projTable;
            string[] cols = { "Name", "StDate", "EndDate" };
            _Default.View(projTable, projs, cols);
        }

        private void updateProj()
        {
            projTable = db.fetchProj();
            Session["projTable"] = projTable;
            string[] cols = { "Name", "Duration", "DailyCost", "TotalCost", "IsActive" };
            _Default.View(projTable, projs, cols);

            foreach (DataRow row in projTable.Rows)
            {
                string record = row["Name"].ToString();
                if (projsddl.Items.FindByText(record) == null )
                    projsddl.Items.Add(record);
            }
        }

        private void updateDep()
        {
            projdepsddl.Items.Clear();
            projdepsddl.Items.Add(defult);

            string id = _Default.getId(projsddl.SelectedValue, projTable);
            depProjTable = db.fetchdepProj(id);
            Session["depProjTable"] = depTable;

            string[] cols = { "Name", "Emp#" };
            _Default.View(depProjTable, Dets, cols);

            foreach (DataRow row in depProjTable.Rows)
            {
                string record = row["Name"].ToString();
                if (projdepsddl.Items.FindByText(record) == null)
                    projdepsddl.Items.Add(record);
            }

            if (projdepsddl.Items.FindByText("All") == null)
                projdepsddl.Items.Add("All");
        }

        private void updateEmp()
        {
            string id = _Default.getId(projsddl.SelectedValue, projTable);
            string[] cols;
            if (projdepsddl.SelectedValue == "All" || projdepsddl.SelectedValue == defult)
            {
                empDepProjTable = db.fetchProjEmps(id);
                cols = new string[] { "Name", "department", "DailyRate" };
            }
            else
            {
                string depId = _Default.getId(projdepsddl.SelectedValue, depTable);
                empDepProjTable = db.fetchProjEmps(id, depId);
                cols = new string[] { "Name", "DailyRate" };
            }

            Session["empDepProjTable"] = empDepProjTable;
            _Default.View(empDepProjTable, emps, cols);
        }

    }
}