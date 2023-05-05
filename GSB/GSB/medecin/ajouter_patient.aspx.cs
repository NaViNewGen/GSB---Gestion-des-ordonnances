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
    public partial class ajouter_patient : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                // Populate dayList with days 1 to 31
                for (int i = 1; i <= 31; i++)
                {
                    dayDropDownList.Items.Add(i.ToString());
                }

                // Populate yearList with years from 1900 to current year
                int currentYear = DateTime.Now.Year;
                for (int i = currentYear; i >= 1900; i--)
                {
                    yearDropDownList.Items.Add(i.ToString());
                }
            }

            groupage.Items.Clear();
            groupage.Items.Add("Selectionner le groupage");

            using (SqlCommand command = new SqlCommand("SELECT groupe FROM groupage", conn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    groupage.Items.Add(new ListItem(reader["groupe"].ToString()));
                }
                reader.Close();
            }

        }

        protected void ajoutButton_Click(object sender, EventArgs e)
        {
            int usernameFound = 0;

            SqlCommand cmdUsername = conn.CreateCommand();
            cmdUsername.CommandType = CommandType.Text;
            cmdUsername.CommandText = "SELECT * From patient Where username = '" + username.Text + "'";
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

                string nomPhotoPatient = RandomNameFileClass.GetRandomName(10) + ".jpg";
                string path = "";
                photopatient.SaveAs(Request.PhysicalApplicationPath + "/administrateur/image_patient/" + nomPhotoPatient.ToString());
                path = "administrateur/image_patient/" + nomPhotoPatient.ToString();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO patient (firstname,lastname,date_naissance,username,password,email,contact,groupage,photo_patient,sexe) " +
                    "VALUES (@firstname,@lastname,CONVERT(DATE, @date_naissance, 103),@username,@password,@email,@contact,@groupage,@photo_patient,@sexe)";
                cmd.Parameters.AddWithValue("@firstname", prenom.Text);
                cmd.Parameters.AddWithValue("@lastname", nom.Text);
                DateTime dt = Convert.ToDateTime(datenaissance.Text);
                cmd.Parameters.AddWithValue("@date_naissance", dt);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@contact", contact.Text);
                cmd.Parameters.AddWithValue("@groupage", groupage.Text);
                cmd.Parameters.AddWithValue("@photo_patient", path.ToString());
                cmd.Parameters.AddWithValue("@sexe", sexeDropDownList.SelectedValue.ToString());
                cmd.ExecuteNonQuery();

                Response.Write("<script>alert('Ajouté(e) avec succès !');</script >");

            }
        }

        protected void dayDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int day = Convert.ToInt32(dayDropDownList.SelectedValue);
            int month = Convert.ToInt32(monthDropDownList.SelectedValue);
            int year = Convert.ToInt32(yearDropDownList.SelectedValue);

            DateTime date = new DateTime(year, month, day);
            datenaissance.Text = date.ToString("dd/MM/yyyy");

        }

        protected void monthDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int day = Convert.ToInt32(dayDropDownList.SelectedValue);
            int month = Convert.ToInt32(monthDropDownList.SelectedValue);
            int year = Convert.ToInt32(yearDropDownList.SelectedValue);

            DateTime date = new DateTime(year, month, day);
            datenaissance.Text = date.ToString("dd/MM/yyyy");

        }

        protected void yearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int day = Convert.ToInt32(dayDropDownList.SelectedValue);
            int month = Convert.ToInt32(monthDropDownList.SelectedValue);
            int year = Convert.ToInt32(yearDropDownList.SelectedValue);

            DateTime date = new DateTime(year, month, day);
            datenaissance.Text = date.ToString("dd/MM/yyyy");

        }
    }
}