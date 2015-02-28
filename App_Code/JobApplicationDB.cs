using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for JobApplicationDB
/// </summary>
public class JobApplicationDB
{
    private static string connectionString = WebConfigurationManager.ConnectionStrings["SOFT338_ConnectionString"].ConnectionString;
    public static string ErrorMessage;

    public static List<JobApplication> getList()
    {
        List<JobApplication> applications = new List<JobApplication>();

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM JobApplications", con);

        using (con)
        {
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                JobApplication temp = new JobApplication(
                    (string)reader["JobTitle"],
                    (string)reader["CompanyName"],
                    (string)reader["JobDescription"],
                    (string)reader["BusinessSector"],
                    (string)reader["Postcode"],
                    (string)reader["Town"],
                    (string)reader["County"],
                    (string)reader["RecruiterName"],
                    (string)reader["RecruiterNumber"],
                    (string)reader["RecruiterEmail"],
                    (string)reader["ApplicationNotes"]);

                applications.Add(temp);
            }
        }

        return applications;
    }

    public static JobApplication get(int _id)
    {
        throw new NotImplementedException();
    }

    public static JobApplication insert(JobApplication _job)
    {
        throw new NotImplementedException();
    }

    public static JobApplication update(JobApplication _job)
    {
        throw new NotImplementedException();
    }

    public static int delete(int _id)
    {
        throw new NotImplementedException();
    }
}