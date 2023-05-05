using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSB.pharmacien
{
    public partial class change_status : System.Web.UI.Page
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
            if (Session["pharmacien"] == null)
            {
                Response.Redirect("login_pharmacien.aspx");
            }

            if (IsPostBack) return;

            SqlCommand cmdstatus = conn.CreateCommand();
            cmdstatus.CommandType = CommandType.Text;
            cmdstatus.CommandText = "SELECT status FROM ordonnance WHERE Id = @Id";
            cmdstatus.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["id"].ToString()));
            string status = cmdstatus.ExecuteScalar().ToString();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (status == "En attente")
            {
                cmd.CommandText = "UPDATE ordonnance SET status = 'En traitement' WHERE Id = @Id";
            }
            else if (status == "En traitement")
            {
                cmd.CommandText = "UPDATE ordonnance SET status = 'Délivrée' , date_delivree = GETDATE()" +
                    " WHERE Id = @Id";
            }
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Request.QueryString["id"].ToString()));
            cmd.ExecuteNonQuery();

            Response.Redirect("listeordonnances.aspx");

        }
    }
}