using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;
using System.Web.Configuration;

namespace GSB.medecin
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
            if (Session["medecin"] == null)
            {
                Response.Redirect("login_medecin.aspx");
            }
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT o.Id, p.firstname, p.lastname, o.patient_age, o.date_ordonnance, o.status , o.date_delivree " +
                "FROM ordonnance o " +
                "INNER JOIN patient p ON o.patient_id = p.Id " +
                "WHERE o.medecin_id = " + Convert.ToInt32(Session["id_medecin"].ToString())+"";
            cmd.ExecuteNonQuery();
            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DT);
            listPatient.DataSource = DT;
            listPatient.DataBind();

        }

        protected string GetStatusLink(object statusObj)
        {
            string status = statusObj.ToString();
            string page = "";

            if (status == "En attente")
            {
                page = "<a href='cancel_submit_ordonnance.aspx?id=" + Eval("Id") + "' style='color:orange'>Annulée</a>";
            }
            else if (status == "Annulée")
            {
                page = "<a href='cancel_submit_ordonnance.aspx?id=" + Eval("Id") + "' style='color:green'>Soumettre</a>";
            }
            return page;
        }
        protected string GetStatusLinkMod(object statusObj)
        {
            string status = statusObj.ToString();
            string page = "";

            if (status == "En attente")
            {
                page = "<a href='edit_ordonnance.aspx?id=" + Eval("Id") + "' style='color:gray'>Modifier</a>";
            }
            
            return page;
        }
        protected string GetStatusLinkSup(object statusObj)
        {
            string status = statusObj.ToString();
            string page = "";
                
            if (status == "Annulée")
            {
                page = "<a href='delete_ordonnance?id=" + Eval("Id") + "' style='color:red'>Supprimer</a>";
            }

            return page;
        }
    }
}