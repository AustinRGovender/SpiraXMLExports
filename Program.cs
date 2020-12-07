using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TableToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            returnDivsions();
            Console.ReadLine();
        }

        static void returnDivsions()
        {

            string returnDivisionsQ = "select prj.name as ProjectName, inc.incident_id, inc.name as IncidentName, inc.SEVERITY_ID from dbo.tst_incident inc  join dbo.tst_project prj on inc.project_id = prj.project_id where prj.name = 'Supplier Enablement' order by inc.incident_id";

            SqlDataAdapter sda;
            DataTable dt;

            try {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"xxx\xxx",
                    IntegratedSecurity = true,//windows authentication - AD Login 
                    InitialCatalog = "xxx"
                };

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(returnDivisionsQ, connection))
                    {

                        dt = new DataTable("Incident");
                        sda = new SqlDataAdapter(command);
                        sda.Fill(dt);
                        dt.WriteXml("Incidents.xml");
                        Console.WriteLine("Success");
                        connection.Close();
                    }//close using
                }
            }
            catch (Exception e)
            {  
                throw;
            }
        }
    }
}
