using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSB.medecin
{
    public partial class edit_patient : System.Web.UI.Page
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
            if (Session["medecin"] == null)
            {
                Response.Redirect("login_medecin.aspx");
            }

            id = Convert.ToInt32(Request.QueryString["Id"].ToString());

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

            using (SqlCommand command = new SqlCommand("SELECT groupe FROM groupage", conn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (groupage.Text != reader["groupe"].ToString())
                        groupage.Items.Add(new ListItem(reader["groupe"].ToString()));
                }
                reader.Close();
            }

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From patient Where Id=" + id + "";
            cmd.ExecuteNonQuery();
            DataTable DTadmin = new DataTable();
            SqlDataAdapter DAadmin = new SqlDataAdapter(cmd);
            DAadmin.Fill(DTadmin);
            foreach (DataRow dr in DTadmin.Rows)
            {
                firstname.Text = dr["firstname"].ToString();
                lastname.Text = dr["lastname"].ToString();
                DateTime d = (DateTime)dr["date_naissance"];
                string dd = d.ToString("dd/MM/yyyy");
                datenaissance.Text = dd;

                DateTime date = DateTime.ParseExact(datenaissance.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int day = date.Day;
                int month = date.Month;
                int year = date.Year;

                dayDropDownList.SelectedValue = day.ToString();
                monthDropDownList.SelectedValue = month.ToString("0");
                yearDropDownList.SelectedValue = year.ToString();

                username.Text = dr["username"].ToString();
                password.Text = dr["password"].ToString();
                email.Text = dr["email"].ToString();
                contact.Text = dr["contact"].ToString();
                groupage.SelectedValue = dr["groupage"].ToString();
                photopatientlabel.Text = dr["photo_patient"].ToString();
                sexeDropDownList.SelectedValue = dr["sexe"].ToString();

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

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string nomPhoto = "";
            string path = "";
            if (photopatient.FileName.ToString() != "")
            {
                nomPhoto = RandomNameFileClass.GetRandomName(10) + ".jpg";
                photopatient.SaveAs(Request.PhysicalApplicationPath + "/administrateur/image_patient/" + nomPhoto.ToString());
                path = "administrateur/image_patient/" + nomPhoto.ToString();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "UPDATE patient SET " +
                  "firstname = @firstname, " +
                  "lastname = @lastname, " +
                  "date_naissance = CONVERT(DATE, @date_naissance, 103), " +
                  "username = @username, " +
                  "password = @password, " +
                  "email = @email, " +
                  "contact = @contact, " +
                  "groupage = @groupage, " +
                  "photo_patient = @photo_patient, " +
                  "sexe = @sexe " +
                  "WHERE Id = " + id + "";
                cmd.Parameters.AddWithValue("@firstname", firstname.Text);
                cmd.Parameters.AddWithValue("@lastname", lastname.Text);
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
            }
            else if (photopatient.FileName.ToString() == "")
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE patient SET " +
                  "firstname = @firstname, " +
                  "lastname = @lastname, " +
                  "date_naissance = CONVERT(DATE, @date_naissance, 103), " +
                  "username = @username, " +
                  "password = @password, " +
                  "email = @email, " +
                  "contact = @contact, " +
                  "groupage = @groupage, " +
                  "sexe = @sexe " +
                  "WHERE Id = " + id + "";
                cmd.Parameters.AddWithValue("@firstname", firstname.Text);
                cmd.Parameters.AddWithValue("@lastname", lastname.Text);
                DateTime dt = Convert.ToDateTime(datenaissance.Text);
                cmd.Parameters.AddWithValue("@date_naissance", dt);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@contact", contact.Text);
                cmd.Parameters.AddWithValue("@groupage", groupage.Text);
                cmd.Parameters.AddWithValue("@sexe", sexeDropDownList.SelectedValue.ToString());

                cmd.ExecuteNonQuery();
            }
            Response.Redirect("listepatients.aspx");
        }
    }
}