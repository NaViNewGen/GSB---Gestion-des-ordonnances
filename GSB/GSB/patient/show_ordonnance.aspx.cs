using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSB.patient
{
    public partial class show_ordonnance : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["GSB"].ConnectionString;
        SqlConnection conn = null;
        int id;
        int medicinsTestBoxNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            if (Session["patient"] == null)
            {
                Response.Redirect("login_patient.aspx");
            }

            id = Convert.ToInt32(Request.QueryString["Id"].ToString());


            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT patient_id From ordonnance Where Id=" + id + "";
            int patientID = (int)cmd.ExecuteScalar();


            //patient information:
            SqlCommand cmdpatient = conn.CreateCommand();
            cmdpatient.CommandType = CommandType.Text;
            cmdpatient.CommandText = "SELECT * From patient Where Id=" + patientID + "";
            cmdpatient.ExecuteNonQuery();
            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(cmdpatient);
            DA.Fill(DT);
            foreach (DataRow dr in DT.Rows)
            {
                patientid.Items.Add(new ListItem(dr["Id"].ToString() + " - " + dr["username"].ToString(), dr["Id"].ToString()));
                patientfirstname.Text = dr["firstname"].ToString();
                patientlastname.Text = dr["lastname"].ToString();
                string query = "SELECT DATEDIFF(year, date_naissance, GETDATE()) AS Age FROM patient WHERE Id = @patientId";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@patientId", patientid.SelectedItem.Value);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientage.Text = reader.GetInt32(0).ToString();
                        }
                    }
                }
            }


            //medicines information:
            SqlCommand cmdmedicament = conn.CreateCommand();
            cmdmedicament.CommandType = CommandType.Text;
            cmdmedicament.CommandText = "SELECT COUNT(*) FROM ordonnance_medicament WHERE ordonnance_id = @ordonnance_id";
            cmdmedicament.Parameters.AddWithValue("@ordonnance_id", id);
            int count = (int)cmdmedicament.ExecuteScalar();
            medicinsTestBoxNumber = count;
            medicinsNumber.Text = count.ToString();


            //retreive the medicines:
            if (medicinsNumber.Text != "")
            {
                medicinsTestBoxNumber = Convert.ToInt32(medicinsNumber.Text.ToString());
                medicinsPanel.Visible = true;
                addControls();

                SqlCommand cmdmediclist = conn.CreateCommand();
                cmdmediclist.CommandType = CommandType.Text;
                cmdmediclist.CommandText = "SELECT * From ordonnance_medicament Where ordonnance_id = @ordonnance_id ";
                cmdmediclist.Parameters.AddWithValue("@ordonnance_id", id);
                cmdmediclist.ExecuteNonQuery();
                DataTable DTmed = new DataTable();
                SqlDataAdapter DAmed = new SqlDataAdapter(cmdmediclist);
                DAmed.Fill(DTmed);
                int i = 0;
                foreach (DataRow dr in DTmed.Rows)
                {
                    DropDownList medicinsName = (DropDownList)medicinsPanel.FindControl("medicinsName" + (i + 1).ToString());
                    medicinsName.SelectedValue = dr["medicament_id"].ToString();
                    medicinsName.Enabled = false;

                    TextBox numberPerDay = (TextBox)medicinsPanel.FindControl("numberPerDay" + (i + 1).ToString());
                    numberPerDay.Text = dr["nombre_fois_par_jr"].ToString();
                    numberPerDay.Enabled = false;

                    TextBox period = (TextBox)medicinsPanel.FindControl("period" + (i + 1).ToString());
                    period.Text = dr["periode_jr"].ToString();
                    period.Enabled = false;

                    i++;
                }
            }
            else
            {
                medicinsTestBoxNumber = 0;
                medicinsPanel.Visible = false;
            }
        }

        protected void addControls()
        {

            int count = medicinsTestBoxNumber;
            medicinsPanel.Controls.Clear();
            int i = 0;
            while (i < count)
            {
                Label medicinsNamelabel = new Label();
                medicinsNamelabel.Text = "Médicament " + (i + 1).ToString() + " : ";
                medicinsPanel.Controls.Add(medicinsNamelabel);
                DropDownList medicinsName = new DropDownList();
                medicinsName.ID = "medicinsName" + (i + 1).ToString();

                using (SqlCommand command = new SqlCommand("SELECT Id, nom_medicament FROM medicament", conn))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        medicinsName.Items.Add(new ListItem(reader["nom_medicament"].ToString(), reader["Id"].ToString()));
                    }
                    reader.Close();
                }
                medicinsName.Attributes.Add("style", "width: 280px;");
                medicinsName.Items.Insert(0, new ListItem("--Choisir un médicament--", "-1"));
                medicinsPanel.Controls.Add(medicinsName);

                Label numberPerDaylabel = new Label();
                numberPerDaylabel.Text = " Nombre de fois par jour  : ";
                medicinsPanel.Controls.Add(numberPerDaylabel);
                TextBox numberPerDay = new TextBox();
                numberPerDay.ID = "numberPerDay" + (i + 1).ToString();
                medicinsPanel.Controls.Add(numberPerDay);

                Label periodlabel = new Label();
                periodlabel.Text = " Période (jour) : ";
                medicinsPanel.Controls.Add(periodlabel);
                TextBox period = new TextBox();
                period.ID = "period" + (i + 1).ToString();
                medicinsPanel.Controls.Add(period);
                medicinsPanel.Controls.Add(new LiteralControl("<br />"));
                medicinsPanel.Controls.Add(new LiteralControl("<br />"));

                i++;
            }
        }

        protected void previewButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("preview.aspx?id=" + id);

        }
    }
}