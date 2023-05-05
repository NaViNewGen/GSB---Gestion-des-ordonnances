using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GSB.pharmacien
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
            if (Session["pharmacien"] == null)
            {
                Response.Redirect("login_pharmacien.aspx");
            }
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT o.Id, p.firstname, p.lastname, m.firstname as medfirstname ,m.lastname as medlastname, " +
                "o.date_ordonnance, o.status , o.date_delivree " +
                "FROM ordonnance o " +
                "JOIN patient p ON o.patient_id = p.Id " +
                "JOIN medecin m ON o.medecin_id = m.Id " ;
            cmd.ExecuteNonQuery();
            DataTable DTmembers = new DataTable();
            SqlDataAdapter DAmembers = new SqlDataAdapter(cmd);
            DAmembers.Fill(DTmembers);
            listPatient.DataSource = DTmembers;
            listPatient.DataBind();

        }

        protected string GetStatusLink(object statusObj)
        {
            string status = statusObj.ToString();
            string page = "";

            if (status == "En attente")
            {
                page = "<a href='change_status.aspx?id=" + Eval("Id") + "' style='color:green'>Traiter</a>";
            }
            else if (status == "En traitement")
            {
                page = "<a href='change_status.aspx?id=" + Eval("Id") + "' style='color:green'>Délivrer</a>";
            }
            
            return page;
        }
    }
}