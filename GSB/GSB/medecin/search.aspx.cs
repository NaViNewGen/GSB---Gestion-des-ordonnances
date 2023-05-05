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
    public partial class search : System.Web.UI.Page
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

        }

        protected void ordonnancenumber_CheckedChanged(object sender, EventArgs e)
        {
            patientname.Checked = !ordonnancenumber.Checked;
        }

        protected void patientname_CheckedChanged(object sender, EventArgs e)
        {
            ordonnancenumber.Checked = !patientname.Checked;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT o.Id, p.firstname, p.lastname, m.firstname as medfirstname ,m.lastname as medlastname, " +
                "o.date_ordonnance, o.status " +
                "FROM ordonnance o " +
                "JOIN patient p ON o.patient_id = p.Id " +
                "JOIN medecin m ON o.medecin_id = m.Id ";
            if (patientname.Checked)
            {
                cmd.CommandText += " WHERE (p.firstname = '" + searchfield.Text + "' OR p.lastname = '" + searchfield.Text + "') " +
                    "AND m.Id = " + Convert.ToInt32(Session["id_medecin"].ToString());
                cmd.ExecuteNonQuery();
            }
            else if (ordonnancenumber.Checked)
            {
                if (int.TryParse(searchfield.Text, out int number))
                {
                    cmd.CommandText += " WHERE o.Id = " + Convert.ToInt32(searchfield.Text.ToString())+" " +
                        "AND m.Id = " + Convert.ToInt32(Session["id_medecin"].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                cmd.CommandText += " WHERE m.Id = " + Convert.ToInt32(Session["id_medecin"].ToString());
                cmd.ExecuteNonQuery();
                error.Style.Add("display", "block");
            }
            DataTable DT = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DT);
            listPatient.DataSource = DT;
            listPatient.DataBind();
            error.Style.Clear();
            error.Style.Add("display", "none");

        }
    }
}