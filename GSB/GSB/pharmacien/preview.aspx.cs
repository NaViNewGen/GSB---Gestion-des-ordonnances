using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GSB.pharmacien
{
    public partial class preview : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["GSB"].ConnectionString;
        SqlConnection conn = null;
        int id;
        int patient_id;
        int medecin_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            if (Session["pharmacien"] == null)
            {
                Response.Redirect("login_pharmacien.aspx");
            }


            id = Convert.ToInt32(Request.QueryString["Id"].ToString());
            previewTextBox.Text = "";
            previewTextBox.Text += "\n";
            previewTextBox.Text += "************************************************************************************************************\n";
            previewTextBox.Text += "                                                                 Ordonnance N " + id + "\n";
            previewTextBox.Text += "************************************************************************************************************\n";
            previewTextBox.Text += "\n";
            previewTextBox.Text += "\n";
            previewTextBox.Text += "\n";





            //patient information:

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT patient_id From ordonnance Where Id=" + id + "";
            patient_id = (int)cmd.ExecuteScalar();

            string firstNamePatient="", firstNameMedecin = "";
            string lastNamePatient = "", lastNameMedecin = "";
            string dateOrdonnance = "";
            string speciality = "";
            string agePatient = "";

            SqlCommand cmdpatient = conn.CreateCommand();
            cmdpatient.CommandType = CommandType.Text;
            cmdpatient.CommandText = "SELECT * From patient Where Id=" + patient_id + "";
            SqlDataReader reader = cmdpatient.ExecuteReader();
            if (reader.Read())
            {
                firstNamePatient = reader.GetString(reader.GetOrdinal("firstname"));
                lastNamePatient = reader.GetString(reader.GetOrdinal("lastname"));
            }
            reader.Close();
            string query = "SELECT DATEDIFF(year, date_naissance, GETDATE()) AS Age FROM patient WHERE Id = @patientId";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@patientId", patient_id);
                using (SqlDataReader readerA = command.ExecuteReader())
                {
                    if (readerA.Read())
                    {
                        agePatient = readerA.GetInt32(0).ToString();
                    }
                }
            }

            //medecin information:
            SqlCommand cmdm = conn.CreateCommand();
            cmdm.CommandType = CommandType.Text;
            cmdm.CommandText = "SELECT medecin_id From ordonnance Where Id=" + id + "";
            medecin_id = (int)cmdm.ExecuteScalar();
            
            SqlCommand cmdmedecin = conn.CreateCommand();
            cmdmedecin.CommandType = CommandType.Text;
            cmdmedecin.CommandText = "SELECT * From medecin Where Id=" + medecin_id + "";
            SqlDataReader readermedecin = cmdmedecin.ExecuteReader();
            if (readermedecin.Read())
            {
                firstNameMedecin = readermedecin.GetString(readermedecin.GetOrdinal("firstname"));
                lastNameMedecin = readermedecin.GetString(readermedecin.GetOrdinal("lastname"));
                speciality = readermedecin.GetString(readermedecin.GetOrdinal("speciality"));
            }
            readermedecin.Close();

            //date of ordonnance:
            SqlCommand cmdd = conn.CreateCommand();
            cmdd.CommandType = CommandType.Text;
            cmdd.CommandText = "SELECT date_ordonnance From ordonnance Where Id=" + id + "";
            SqlDataReader readerd = cmdd.ExecuteReader();
            if (readerd.HasRows) 
            {
                readerd.Read();
                DateTime date = (DateTime)readerd["date_ordonnance"];
                dateOrdonnance = date.ToString("dd/MM/yyyy");
            }
            readerd.Close();

            previewTextBox.Text += "Dr." + lastNameMedecin + " " + firstNameMedecin + "                                                                                         Patient : " + lastNamePatient + " " + firstNamePatient+ "\n";
            previewTextBox.Text += "Spécialité :"+speciality+ "                                                                              Age :" + agePatient+ "\n";
            previewTextBox.Text += "Date : " + dateOrdonnance + "\n";
            previewTextBox.Text += "\n";
            previewTextBox.Text += "\n";


            //medicines information:
            SqlCommand cmdmedicament = conn.CreateCommand();
            cmdmedicament.CommandType = CommandType.Text;
            cmdmedicament.CommandText = "SELECT COUNT(*) FROM ordonnance_medicament WHERE ordonnance_id = @ordonnance_id";
            cmdmedicament.Parameters.AddWithValue("@ordonnance_id", id);
            int count = (int)cmdmedicament.ExecuteScalar();

            previewTextBox.Text += "                                                                 Liste des médicament\n";
            previewTextBox.Text += "\n";
            previewTextBox.Text += "\n";

            //retreive the medicines:
            if (count != 0)
            {
                SqlCommand cmdmediclist = conn.CreateCommand();
                cmdmediclist.CommandType = CommandType.Text;
                cmdmediclist.CommandText = "SELECT om.*, m.nom_medicament " +
                                           "FROM ordonnance_medicament om " +
                                           "INNER JOIN medicament m ON om.medicament_id = m.Id " +
                                           "WHERE om.ordonnance_id = @ordonnance_id";
                cmdmediclist.Parameters.AddWithValue("@ordonnance_id", id);
                cmdmediclist.ExecuteNonQuery();
                DataTable DTmed = new DataTable();
                SqlDataAdapter DAmed = new SqlDataAdapter(cmdmediclist);
                DAmed.Fill(DTmed);
                int i = 0;
                foreach (DataRow dr in DTmed.Rows)
                {
                    previewTextBox.Text += (i + 1).ToString() + "- " + dr["nom_medicament"].ToString() + "    " + dr["nombre_fois_par_jr"].ToString() + "/jr   " + dr["periode_jr"].ToString() + " jr\n";
                    previewTextBox.Text += "\n";
                    i++;
                }
            }
            else
            {
                previewTextBox.Text += "\n";
            }
        }

        
        protected void saveButton_Click(object sender, EventArgs e)
        {
            // Create a new MemoryStream to hold the PDF contents
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create a new Document object
                using (Document document = new Document())
                {
                    // Create a new PdfWriter object to write to the memory stream
                    using (PdfWriter writer = PdfWriter.GetInstance(document, memoryStream))
                    {
                        // Open the document
                        document.Open();

                        // Add the content from the previewTextBox to the document
                        Paragraph paragraph = new Paragraph(previewTextBox.Text);
                        document.Add(paragraph);

                        // Close the document
                        document.Close();
                    }
                }

                // Prompt the user to save the PDF file
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Test.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                // Write the PDF contents to the response stream
                Response.OutputStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
            }
        }
    }
}