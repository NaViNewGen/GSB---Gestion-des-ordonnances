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
    public partial class edit_account : System.Web.UI.Page
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

            using (SqlCommand command = new SqlCommand("SELECT specialite FROM specialite", conn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (specialites.Text != reader["specialite"].ToString())
                        specialites.Items.Add(new ListItem(reader["specialite"].ToString()));
                }
                reader.Close();
            }

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From medecin Where Id=" + Convert.ToInt32(Session["id_medecin"].ToString()) + "";
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
                specialites.Text = dr["speciality"].ToString();
                photomedecinlabel.Text = dr["photo_medecin"].ToString();

            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string nomPhoto = "";
            string path = "";
            if (photomedecin.FileName.ToString() != "")
            {
                nomPhoto = RandomNameFileClass.GetRandomName(10) + ".jpg";
                photomedecin.SaveAs(Request.PhysicalApplicationPath + "/administrateur/image_medecin/" + nomPhoto.ToString());
                path = "administrateur/image_medecin/" + nomPhoto.ToString();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE medecin SET firstname = '" + firstname.Text + "',lastname = '" + lastname.Text + "'," +
                    " username = '" + username.Text + "',password = '" + password.Text + "'," +
                    "email = '" + email.Text + "',contact = '" + contact.Text + "',speciality = '" + specialites.Text + "',photo_medecin = '" + path.ToString() + "' Where Id = " + Convert.ToInt32(Session["id_medecin"].ToString()) + "";
                cmd.ExecuteNonQuery();
            }
            else if (photomedecin.FileName.ToString() == "")
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE medecin SET firstname = '" + firstname.Text + "',lastname = '" + lastname.Text + "'," +
                    "username = '" + username.Text + "',password = '" + password.Text + "'," +
                    "email = '" + email.Text + "',contact = '" + contact.Text + "', speciality = '" + specialites.Text + "' Where Id = " + Convert.ToInt32(Session["id_medecin"].ToString()) + "";
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("medecinpage.aspx");
        }
    }
}
