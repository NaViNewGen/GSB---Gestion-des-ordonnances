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
    public partial class edit_pharmacien : System.Web.UI.Page
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
            cmd.CommandText = "SELECT * From pharmacien Where Id=" + id + "";
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
                photopharmacienlabel.Text = dr["photo_pharmacien"].ToString();

            }

        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string nomPhoto = "";
            string path = "";
            if (photopharmacien.FileName.ToString() != "")
            {
                nomPhoto = RandomNameFileClass.GetRandomName(10) + ".jpg";
                photopharmacien.SaveAs(Request.PhysicalApplicationPath + "/administrateur/image_pharmacien/" + nomPhoto.ToString());
                path = "administrateur/image_pharmacien/" + nomPhoto.ToString();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE pharmacien SET firstname = '" + firstname.Text + "',lastname = '" + lastname.Text + "'," +
                    " username = '" + username.Text + "',password = '" + password.Text + "'," +
                    "email = '" + email.Text + "',contact = '" + contact.Text + "',photo_pharmacien = '" + path.ToString() + "' Where Id = " + id + "";
                cmd.ExecuteNonQuery();
            }
            else if (photopharmacien.FileName.ToString() == "")
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE pharmacien SET firstname = '" + firstname.Text + "',lastname = '" + lastname.Text + "'," +
                    "username = '" + username.Text + "',password = '" + password.Text + "'," +
                    "email = '" + email.Text + "',contact = '" + contact.Text + "' Where Id = " + id + "";
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("listepharmaciens.aspx");
        }
    }
}