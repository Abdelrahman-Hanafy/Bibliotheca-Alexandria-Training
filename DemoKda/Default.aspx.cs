using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoKda
{
    public partial class _Default : Page
    {
        DataBase db;
        string defult = "<----- Please Select ----->";

        DataTable sectable;
        DataTable deptable;
        DataTable emptable;

        string sec_name = "";
        string dep_name = "";
        string emp_name = "";


        string[] depCols = { "Name", "Emp#" };

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();

            if (!IsPostBack)
            {
                
                depddl.Items.Add(defult);
                updateSecs();
            }

            if (IsPostBack)
            {
                Delete.Text = "";
                deptable = (DataTable)Session["deptable"];
                sectable = (DataTable)Session["sectable"];
                emptable = (DataTable)Session["emptable"];
                sec_name = (string)Session["sec_name"];
                dep_name = (string)Session["dep_name"];
                emp_name = (string)Session["emp_name"];

            }

        }
        
        // OnSelection
        protected void secddl_SelectedIndexChanged(object sender, EventArgs e)
        {

            updateDeps();
        }

        protected void depddl_SelectedIndexChanged(object sender, EventArgs e)
        {

            updateEmp();
        }

        // OnDeleting
        protected void Emps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = Emps.Rows[e.RowIndex];
            string n = row.Cells[3].Text;
            string id = getId(n,emptable);
            db.removeEmp(id);

            updateEmp();
            updateDeps();
            
        }

        protected void Deps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = Deps.Rows[e.RowIndex];

            if (row.Cells[3].Text != "0" )
            {
                Delete.Text = "You Should MOVE all Employees First!";
                Delete.ForeColor = System.Drawing.Color.Red;
                return;
            }
            
            string n = row.Cells[2].Text;
            string id = getId(n, deptable);
            db.removeDep(id);
           
            updateDeps();

        }


        // OnEditing
        protected void secs_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = secs.Rows[e.NewEditIndex];
            sec_name = row.Cells[1].Text;
            Session["sec_name"] = sec_name;

            secs.EditIndex = e.NewEditIndex;

            View(sectable, secs, new string[] { "Name" });
        }
        protected void secs_OnRowCancelingEdit(object sender, EventArgs e)
        {
            secs.EditIndex = -1;

            View(sectable, secs, new string[] { "Name" });
        }
        protected void secs_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = secs.Rows[e.RowIndex];
            string n = (row.Cells[1].Controls[0] as TextBox).Text;
            string id = getId(sec_name, sectable);
            db.updateSec(id,n);
            secs.EditIndex = -1;
            updateSecs();
        }

        protected void Deps_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = Deps.Rows[e.NewEditIndex];
            dep_name = row.Cells[2].Text;
            Session["dep_name"] = dep_name;

            Deps.EditIndex = e.NewEditIndex;

            View(deptable, Deps, depCols);
        }
        protected void Deps_OnRowCancelingEdit(object sender, EventArgs e)
        {
            Deps.EditIndex = -1;

            View(deptable, Deps, depCols);
        }
        protected void Deps_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = Deps.Rows[e.RowIndex];
            string n = (row.Cells[2].Controls[0] as TextBox).Text;
            string id = getId(dep_name, deptable);
            db.updateDep(id, n);
            Deps.EditIndex = -1;
            updateDeps();
        }



        protected void Emps_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = Emps.Rows[e.NewEditIndex];
            emp_name = row.Cells[3].Text;
            Session["emp_name"] = emp_name;

            Emps.EditIndex = e.NewEditIndex;

            string[] cols = { "Name", "Salary", "BirthDate" };
            View(emptable, Emps,cols);
            Emps.Columns[2].Visible = true;

        }
        protected void Emps_OnRowCancelingEdit(object sender, EventArgs e)
        {
            Emps.EditIndex = -1;
            updateEmp();
        }
        protected void Emps_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = Emps.Rows[e.RowIndex];
            string n = (row.Cells[3].Controls[0] as TextBox).Text;
            string id = getId(emp_name, emptable);
            string birth = "";
        }

        // Utilities
        public static String getId(String name, DataTable dt)
        {
            foreach (DataRow r in dt.Rows)
            {
                if (name == r["Name"].ToString())
                    return r["ID"].ToString();
            }
            return "";
        }

        public static void View(DataTable dt, GridView g, string[] cols)
        {
            g.DataSource = new DataView(dt).ToTable(false, cols);
            g.DataBind();
            
        }

        private void updateSecs()
        {
            sectable = db.fetchSectors();
            Session["sectable"] = sectable;
            View(sectable, secs, new string[] { "Name" });

            secddl.Items.Clear();
            secddl.Items.Add(defult);
            foreach (DataRow row in sectable.Rows)
            {
                string record = row["Name"].ToString();
                if (secddl.Items.FindByText(record) == null)
                    secddl.Items.Add(record);
            }
        }

        private void updateDeps()
        {
            string id = getId(secddl.SelectedValue, sectable);
            deptable = db.fetchDeps(id);
            Session["deptable"] = deptable;

            View(deptable, Deps, depCols);

            depddl.Items.Clear();
            depddl.Items.Add(defult);
            foreach (DataRow row in deptable.Rows)
            {
                string record = row["Name"].ToString();
                if (depddl.Items.FindByText(record) == null)
                    depddl.Items.Add(record);
            }
        }

        private void updateEmp()
        {
            string id = getId(depddl.SelectedValue, deptable);
            emptable = db.fetchEmps(id);
            Session["emptable"] = emptable;

            string[] cols = { "Name", "Salary", "Age" };
            View(emptable, Emps, cols);
            Emps.Columns[2].Visible = false;
        }

    }

}