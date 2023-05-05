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
    public partial class listeordonnances : System.Web.UI.Page
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
            if (Session["patient"] == null)
            {
                Response.Redirect("login_patient.aspx");
            }
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT o.Id, m.firstname as medfirstname ,m.lastname as medlastname, " +
                "o.date_ordonnance, o.status , o.date_delivree " +
                "FROM ordonnance o " +
                "INNER JOIN medecin m ON o.medecin_id = m.Id " +
                "WHERE o.patient_id = " + Convert.ToInt32(Session["id_patient"].ToString());
            cmd.ExecuteNonQuery();
            DataTable DTmembers = new DataTable();
            SqlDataAdapter DAmembers = new SqlDataAdapter(cmd);
            DAmembers.Fill(DTmembers);
            listPatient.DataSource = DTmembers;
            listPatient.DataBind();

        }
    }
}