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
        DataBase db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();

            if (Session["sectable"] == null)
            {
                sectable = db.fetchSectors();
                Session["sectable"] = sectable;
            }
            else
            {
                sectable = (DataTable)Session["sectable"];
            }

            foreach (DataRow row in sectable.Rows)
            {
                string record = row["Name"].ToString();
                if (secddl.Items.FindByText(record) == null)
                    secddl.Items.Add(record);
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
    }
}