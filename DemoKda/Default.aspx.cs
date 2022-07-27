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
                secddl.Items.Add(defult);
                depddl.Items.Add(defult);
                sectable = db.fetchSectors();
                Session["sectable"] = sectable;
            }

            if (IsPostBack)
            {
                deptable = (DataTable)Session["deptable"];
                sectable = (DataTable)Session["sectable"];
                emptable = (DataTable)Session["emptable"];
            }

            foreach (DataRow row in sectable.Rows)
            {
                string record = row["Name"].ToString();
                if (secddl.Items.FindByText(record) == null)
                    secddl.Items.Add(record);
            }

            View(sectable, secs, new string[]{ "Name"});

        }

        protected void secddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            depddl.Items.Clear();
            depddl.Items.Add(defult);

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
                g.DataSource = new DataView(dt).ToTable(false,cols);
                g.DataBind();
            
        }
    }

}