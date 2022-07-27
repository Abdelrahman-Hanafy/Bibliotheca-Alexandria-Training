using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoKda
{
    public partial class Add : System.Web.UI.Page
    {
        DataTable sectable;
        DataTable deptable;
        DataTable projTable;
        DataTable depProjTable;
        DataTable empDepProjTable;
        DataBase db;
        string defult = "<----- Please Select ----->";


        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();

            if (!IsPostBack)
            {
                secddl.Items.Add(defult);
                depddl.Items.Add(defult);
                projdepddl.Items.Add(defult);
                projddl.Items.Add(defult);
                projddl2.Items.Add(defult);
                projdepddl2.Items.Add(defult);


                projTable = db.fetchProj();
                Session["projTable"] = projTable;
                sectable = db.fetchSectors();
                Session["sectable"] = sectable;
                deptable = db.fetchDeps();
                Session["deptable"] = deptable;
            }
            else
            {
                sectable = (DataTable)Session["sectable"];
                deptable = (DataTable)Session["deptable"];
                projTable = (DataTable)Session["projTable"];
                depProjTable = (DataTable)Session["depProjTable"];

            }


            foreach (DataRow row in sectable.Rows)
            {
                string record = row["Name"].ToString();
                if (secddl.Items.FindByText(record) == null)
                    secddl.Items.Add(record);
            }

            foreach (DataRow row in projTable.Rows)
            {
                string record = row["Name"].ToString();
                if (projddl.Items.FindByText(record) == null)
                    projddl.Items.Add(record);

                if (projddl2.Items.FindByText(record) == null)
                    projddl2.Items.Add(record);
            }

            foreach (DataRow row in deptable.Rows)
            {
                string record = row["Name"].ToString();
                if (depddl.Items.FindByText(record) == null)
                    depddl.Items.Add(record);

                if (projdepddl.Items.FindByText(record) == null)
                    projdepddl.Items.Add(record);
            }
        }

        protected void addbtn_Click(object sender, EventArgs e)
        {
            string name = secname.Text;
            db.insertSec(name);
            secname.Text = string.Empty;
        }

        protected void depbtn_Click(object sender, EventArgs e)
        {
            string name = depname.Text;
            string id = _Default.getId(secddl.SelectedValue,sectable);
            db.insertDep(name, id);
            depname.Text = string.Empty;
        }

        protected void projbtn_Click(object sender, EventArgs e)
        {
            db.insertProj(projname.Text,projst.Text,projend.Text);
            projname.Text = string.Empty;
            projst.Text = string.Empty;
            projend.Text = string.Empty;
        }

        protected void empbtn_Click(object sender, EventArgs e)
        {
            string id= _Default.getId(depddl.SelectedValue, deptable);
            db.insertEmp(empname.Text,empsalary.Text,empbirth.Text,id);
            empname.Text = string.Empty;
            empsalary.Text = string.Empty;
            empbirth.Text = string.Empty;
        }

        protected void projDep_Click(object sender, EventArgs e)
        {
            string proj = _Default.getId(projddl.SelectedValue, projTable);
            string dep = _Default.getId(projdepddl.SelectedValue, deptable);
            db.insertProjDep(proj,dep);
            //projddl.SelectedIndex = 0;
            projdepddl.SelectedIndex = 0;
        }

        protected void projddl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            projdepddl2.Items.Clear();
            projdepddl2.Items.Add(defult);

            string id = _Default.getId(projddl2.SelectedValue, projTable);
            depProjTable = db.fetchdepProj(id);
            Session["depProjTable"] = depProjTable;

            foreach (DataRow row in depProjTable.Rows)
            {
                string record = row["Name"].ToString();
                if (projdepddl2.Items.FindByText(record) == null)
                    projdepddl2.Items.Add(record);
            }

            if (projdepddl2.Items.FindByText("All") == null)
                projdepddl2.Items.Add("All");
        }

        protected void projdepddl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Empls.Items.Clear();
            string id = _Default.getId(projddl2.SelectedValue, projTable);
            string depId = _Default.getId(projdepddl2.SelectedValue, depProjTable);
            empDepProjTable = db.fetchNoProjEmps(id, depId);


            foreach (DataRow row in empDepProjTable.Rows)
            {
                string record = row["Name"].ToString();
                if (Empls.Items.FindByText(record) == null)
                    Empls.Items.Add(record);
            }
            

        }

        protected void emptoProject_Click(object sender, EventArgs e)
        {


        }
    }
}