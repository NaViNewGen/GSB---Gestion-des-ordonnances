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
    public partial class edit_admin : System.Web.UI.Page
    {
        int id;
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

            id = Convert.ToInt32(Request.QueryString["Id"].ToString());

            if (IsPostBack) return;

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From admin Where Id=" + id + "";
            cmd.ExecuteNonQuery();
            DataTable DTadmin = new DataTable();
            SqlDataAdapter DAadmin = new SqlDataAdapter(cmd);
            DAadmin.Fill(DTadmin);
            foreach (DataRow dr in DTadmin.Rows)
            {
                firstname.Text = dr["firstname"].ToString();
                lastname.Text = dr["lastname"].ToString();
                username.Text = dr["username"].ToString();
                password.Text = dr["password"].ToString();
                email.Text = dr["email"].ToString();
                contact.Text = dr["contact"].ToString();
            }
        }

        protected void updateAdmin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE admin SET firstname = '" + firstname.Text + "',lastname = '" + lastname.Text + "',"
            + "username = '" + username.Text + "',password = '" + password.Text + "'," +
                "email = '" + email.Text + "',contact = '" + contact.Text + "' Where Id = " + id + "";
            cmd.ExecuteNonQuery();


            Response.Redirect("listeadmin.aspx");
        }
    }
}