using APITaskManagement.Logic.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace APITaskManagement.Logic.Api
{
    public class HelloDialogMailFrom
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class HelloDialogReplace
    {
        public string find { get; set; }
        public string replace { get; set; }
    }

    public class HelloDialogTemplate
    {
        public int id { get; set; }
        public IList<HelloDialogReplace> replaces { get; set; }

        public HelloDialogTemplate()
        {
            replaces = new List<HelloDialogReplace>();
        }
    }

    public class HelloDialogMail
    {
        public string to { get; set; }
        public HelloDialogMailFrom from { get; set; }
        public string subject { get; set; }
        public string tag { get; set; }
        public HelloDialogTemplate template { get; set; }

        public HelloDialogMail()
        {
            
        }
    }

    public class HelloDialogEmailFormatter : IContentFormatter
    {
        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            string connectionstring;
            if (!properties.TryGetValue("connectionstring", out connectionstring))
            {
                if (properties.TryGetValue("connection_string_name", out connectionstring))
                {
                    connectionstring = ConfigurationManager.ConnectionStrings[properties["connection_string_name"]].ConnectionString;
                }
            }
            else
            {
                connectionstring = properties["connectionstring"];
            }

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                connection.Open();

                // Get the email header
                adapter.SelectCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM EEK_vw_API_HD_Header " +
                    "WHERE Id = " + key,
                    connection
                    );
                DataSet emailHeaders = new DataSet("EMAILHEADERS");
                adapter.Fill(emailHeaders);

                if (emailHeaders.Tables[0].Rows.Count > 0)
                {
                    DataRow emailHeader = emailHeaders.Tables[0].Rows[0];

                    var email = new HelloDialogMail
                    {
                        to = (string)emailHeader["emailto"],
                        from = new HelloDialogMailFrom
                        {
                            email = (string)emailHeader["emailfrom"],
                            name = (string)emailHeader["emailfromname"]
                        },
                        subject = (string)emailHeader["subject"],
                        tag = (string)emailHeader["tag"],
                        template = new HelloDialogTemplate
                        {
                            id = Convert.ToInt32(emailHeader["template"])
                        }

                    };

                    // Get the email lines
                    adapter.SelectCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM EEK_vw_API_HD_Lines " +
                    "WHERE Id = " + key,
                    connection
                    );
                    DataSet emailLines = new DataSet("EMAILLINES");
                    adapter.Fill(emailLines);

                    if (emailLines.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow emailLine in emailLines.Tables[0].Rows)
                        {
                            var replace = new HelloDialogReplace
                            {
                                find = (string)emailLine["tagname"],
                                replace = (string)emailLine["tagtext"]
                            };

                            email.template.replaces.Add(replace);
                        }
                    }
                    else
                    {
                        throw new Exception("No emails found in _AB_VW_TH_Orders_email_lines");
                    }

                    return new JavaScriptSerializer().Serialize(email);
                }
                else
                {
                    throw new Exception("No emails found in _AB_VW_TH_Orders_email_hdr");
                }
            }
        }
    }
}
