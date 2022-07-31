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

            }

            

            

        }
        

        protected void secddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (e is GridViewDeleteEventArgs)
            {
                
            }
            else
            {
                depddl.Items.Clear();
                depddl.Items.Add(defult);
            }
                

            string id = getId(secddl.SelectedValue, sectable);
            deptable = db.fetchDeps(id);
            Session["deptable"] = deptable;

            string[] cols = { "Name", "Emp#" };
            View(deptable, Deps, cols );


            foreach (DataRow row in deptable.Rows)
            {
                string record = row["Name"].ToString();
                if (depddl.Items.FindByText(record) == null)
                    depddl.Items.Add(record);
            }
            
        }

        protected void depddl_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = getId(depddl.SelectedValue, deptable);
            emptable = db.fetchEmps(id);
            Session["emptable"] = emptable;

            string[] cols = { "Name", "Salary", "Age" };
            View(emptable, Emps,cols);
        }


        protected void Emps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = Emps.Rows[e.RowIndex];
            string n = row.Cells[2].Text;
            string id = getId(n,emptable);
            db.removeEmp(id);
            depddl_SelectedIndexChanged(sender, e);
            secddl_SelectedIndexChanged(sender,e);

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
            depddl_SelectedIndexChanged(sender, e);
            secddl_SelectedIndexChanged(sender, e);

        }


        string sec_name = "";
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


        protected void Emps_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            Emps.EditIndex = e.NewEditIndex;

            string[] cols = { "Name", "Salary", "Age" };
            View(emptable, Emps,cols);
        }
        protected void Emps_OnRowCancelingEdit(object sender, EventArgs e)
        {
            Emps.EditIndex = -1;

            string[] cols = { "Name", "Salary", "Age" };
            View(emptable, Emps, cols);
        }
        protected void Emps_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

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

    }

}