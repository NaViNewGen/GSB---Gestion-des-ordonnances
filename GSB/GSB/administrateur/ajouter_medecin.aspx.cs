using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;


namespace GSB.administrateur
{
    public partial class ajouter_medecin : System.Web.UI.Page
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


            specialites.Items.Clear();
            specialites.Items.Add("Selectionner une spécialité");



            using (SqlCommand command = new SqlCommand("SELECT specialite FROM specialite", conn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    specialites.Items.Add(new ListItem(reader["specialite"].ToString()));
                }
                reader.Close();
            }
        }


        protected void ajoutButton_Click(object sender, EventArgs e)
        {
            int usernameFound = 0;

            SqlCommand cmdUsername = conn.CreateCommand();
            cmdUsername.CommandType = CommandType.Text;
            cmdUsername.CommandText = "SELECT * From medecin Where username = '" + username.Text + "'";
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

                string nomPhotoMedecin = RandomNameFileClass.GetRandomName(10) + ".jpg";
                string path = "";
                photomedecin.SaveAs(Request.PhysicalApplicationPath + "/administrateur/image_medecin/" + nomPhotoMedecin.ToString());
                path = "administrateur/image_medecin/" + nomPhotoMedecin.ToString();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO medecin (firstname,lastname,speciality,username,password,email,contact,photo_medecin) " +
                    "VALUES (@firstname,@lastname,@speciality,@username,@password,@email,@contact,@photo_medecin)";
                cmd.Parameters.AddWithValue("@firstname", prenom.Text);
                cmd.Parameters.AddWithValue("@lastname", nom.Text);
                cmd.Parameters.AddWithValue("@speciality", specialites.Text);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@contact", contact.Text);
                cmd.Parameters.AddWithValue("@photo_medecin", path.ToString());
                cmd.ExecuteNonQuery();

                Response.Write("<script>alert('Ajouté(e) avec succès !');</script >");
                
            }
        }
    }
}