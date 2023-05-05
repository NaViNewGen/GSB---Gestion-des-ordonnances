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
    public partial class login_admin : System.Web.UI.Page
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
        }
        

        protected void loginButton_Click(object sender, EventArgs e)
        {
            int userFound = 0;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From admin where username = '" + username.Text + "' and password = '" + password.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable DTuserFound = new DataTable();
            SqlDataAdapter DAuserFoud = new SqlDataAdapter(cmd);
            DAuserFoud.Fill(DTuserFound);
            userFound = Convert.ToInt32(DTuserFound.Rows.Count.ToString());
            if (userFound > 0)
            {
                foreach (DataRow dr in DTuserFound.Rows)
                {
                    Session["id_admin"] = dr["Id"].ToString();
                }
                Session["admin"] = username.Text;
                Response.Redirect("adminpage.aspx");
            }
            else
            {
                error.Style.Add("display", "block");
            }

        }
    }
}