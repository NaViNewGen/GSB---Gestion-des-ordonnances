using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSB.medecin
{
    public partial class delete_ordonnance : System.Web.UI.Page
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
            if (Session["medecin"] == null)
            {
                Response.Redirect("login_medecin.aspx");
            }

            if (IsPostBack) return;

            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "DELETE ordonnance_medicament Where ordonnance_id =" + Convert.ToInt32(Request.QueryString["id"].ToString()) + "";
            cmd2.ExecuteNonQuery();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE ordonnance Where Id =" + Convert.ToInt32(Request.QueryString["id"].ToString()) + "";
            cmd.ExecuteNonQuery();

            Response.Redirect("listeordonnances.aspx");

        }
    }
}