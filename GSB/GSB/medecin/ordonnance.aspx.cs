using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using System.IO;

namespace GSB.medecin
{
    public partial class ordonnance : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["GSB"].ConnectionString;
        SqlConnection conn = null;
        int medicinsTestBoxNumber;
        int patientIDd;
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
            if (IsPostBack)
            {
                if (medicinsNumber.Text != "")
                {
                    medicinsTestBoxNumber = Convert.ToInt32(medicinsNumber.Text.ToString());
                    medicinsPanel.Visible = true;
                    addControls();

                }
                else
                {
                    medicinsTestBoxNumber = 0;
                    medicinsPanel.Visible = false;
                }
                return;
            }

            

            patientid.Items.Clear();
            patientid.Items.Add("Sélectionner ou Taper un ID d'un Patient");

            using (SqlCommand command = new SqlCommand("SELECT Id, username FROM patient", conn))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Add each book to the ListBox with both the id and the title
                    patientid.Items.Add(new ListItem(reader["Id"].ToString() + " - " + reader["username"].ToString(), reader["Id"].ToString()));
                }
                reader.Close();
            }

            medicinsPanel.Visible = false;
            

        }

        protected void patientid_SelectedIndexChanged(object sender, EventArgs e)
        {
            patientfirstname.Text = "";
            patientlastname.Text = "";
            patientage.Text = "";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From patient Where Id = "+ Convert.ToInt32(patientid.SelectedValue.ToString()) +"";
            cmd.ExecuteNonQuery();

            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DT);
            foreach (DataRow dr in DT.Rows)
            {
                patientfirstname.Text = dr["firstname"].ToString();
                patientlastname.Text = dr["lastname"].ToString();
                
            }


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

        protected void ajoutordonnance_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO ordonnance (patient_id,medecin_id,date_ordonnance,status,patient_age) OUTPUT INSERTED.Id " +
            "VALUES (@patient_id,@medecin_id,CONVERT(DATE, @date_ordonnance, 103),@status,@patient_age)";
            cmd.Parameters.AddWithValue("@patient_id", Convert.ToInt32(patientid.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("@medecin_id", Convert.ToInt32(Session["id_medecin"].ToString()));
            DateTime dt = DateTime.Now;
            cmd.Parameters.AddWithValue("@date_ordonnance", dt);
            cmd.Parameters.AddWithValue("@status", "En attente");
            cmd.Parameters.AddWithValue("@patient_age", Convert.ToInt32(patientage.Text));
            int ordonnaceId = (int)cmd.ExecuteScalar(); // retrieve the inserted Id using ExecuteScalar
            //cmd.ExecuteNonQuery();

            for (int i = 0; i < medicinsTestBoxNumber ; i++)
            {
                DropDownList medic = (DropDownList)medicinsPanel.FindControl("medicinsName" + (i + 1).ToString());
                TextBox numPerDay = (TextBox)medicinsPanel.FindControl("numberPerDay" + (i + 1).ToString());
                TextBox prd = (TextBox)medicinsPanel.FindControl("period" + (i + 1).ToString());
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "INSERT INTO ordonnance_medicament (ordonnance_id,medicament_id,nombre_fois_par_jr,periode_jr) " +
                "VALUES (@ordonnance_id,@medicament_id,@nombre_fois_par_jr,@periode_jr)";
                cmd1.Parameters.AddWithValue("@ordonnance_id", ordonnaceId);
                cmd1.Parameters.AddWithValue("@medicament_id", Convert.ToInt32(medic.SelectedValue.ToString()));
                cmd1.Parameters.AddWithValue("@nombre_fois_par_jr", Convert.ToInt32(numPerDay.Text));
                cmd1.Parameters.AddWithValue("@periode_jr", Convert.ToInt32(prd.Text));
                cmd1.ExecuteNonQuery();
            }
            Response.Write("<script>alert('Ajouté(e) avec succès !');</script >");


        }

        protected void medicinsNumber_TextChanged(object sender, EventArgs e)
        {
            addControls();
        }

        protected void addControls()
        {
            
            int count = medicinsTestBoxNumber;
            medicinsPanel.Controls.Clear();
            int i = 0;
            while (i < count)
            {
                Label medicinsNamelabel = new Label();
                medicinsNamelabel.Text = "Médicament " + (i + 1).ToString()+" : ";
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
    }
}