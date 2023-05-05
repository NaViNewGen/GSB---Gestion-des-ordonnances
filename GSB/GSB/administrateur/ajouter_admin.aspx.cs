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
    public partial class ajouter_admin : System.Web.UI.Page
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
            if (IsPostBack) return;


        }

        protected void ajoutButton_Click(object sender, EventArgs e)
        {
            int usernameFound = 0;

            SqlCommand cmdUsername = conn.CreateCommand();
            cmdUsername.CommandType = CommandType.Text;
            cmdUsername.CommandText = "SELECT * From admin Where username = '" + username.Text + "'";
            cmdUsername.ExecuteNonQuery();
            DataTable DTusername = new DataTable();
            SqlDataAdapter DAusername = new SqlDataAdapter(cmdUsername);
            DAusername.Fill(DTusername);
            usernameFound = Convert.ToInt32(DTusername.Rows.Count.ToString());
            if (usernameFound > 0)
            {
                Response.Write("<script>alert('Username est déjà existe!');</script >");
            }
            else
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO admin (firstname,lastname,username,password,email,contact) " +
                    "VALUES (@firstname,@lastname,@username,@password,@email,@contact)";
                cmd.Parameters.AddWithValue("@firstname", prenom.Text);
                cmd.Parameters.AddWithValue("@lastname", nom.Text);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@contact", contact.Text);
                cmd.ExecuteNonQuery();

                Response.Write("<script>alert('Ajouté(e) avec succès !');</script >");

            }
        }
    }
}