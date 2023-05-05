using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSB.administrateur
{
    public partial class listepharmaciens : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["GSB"].ConnectionString;
        SqlConnection conn = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            if (Session["admin"] == null)
            {
                Response.Redirect("login_admin.aspx");
            }
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM pharmacien ";
            cmd.ExecuteNonQuery();
            DataTable DTmembers = new DataTable();
            SqlDataAdapter DAmembers = new SqlDataAdapter(cmd);
            DAmembers.Fill(DTmembers);
            listPharmacien.DataSource = DTmembers;
            listPharmacien.DataBind();

        }
    }
}