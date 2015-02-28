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


        try
        {
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
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return applications;
    }

    public static JobApplication get(int _id)
    {
        throw new NotImplementedException();
    }

    public static int insert(JobApplication _job)
    {
        int newID = -1;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO JobApplications ([JobTitle], [CompanyName], [JobDescription], "
            + "[BusinessSector], [Postcode], [Town], [County], [RecruiterName], [RecruiterNumber], [RecruiterEmail], "
            + "[ApplicationNotes]) "
            + "VALUES (@JobTitle, @CompanyName, @JobDescription, @BusinessSector, @PostCode, @Town, @County, @RecruiterName, "
            + "@RecruiterNumber, @RecruiterEmail, @ApplicationNotes); SELECT CAST(Scope_Identity() as int);", con);

        cmd.Parameters.AddWithValue("@JobTitle", _job.JobTitle);
        cmd.Parameters.AddWithValue("@CompanyName", _job.CompanyName);
        cmd.Parameters.AddWithValue("@JobDescription", _job.JobDescription);
        cmd.Parameters.AddWithValue("@BusinessSector", _job.BusinessSector);
        cmd.Parameters.AddWithValue("@Postcode", _job.JobPostcode);
        cmd.Parameters.AddWithValue("@Town", _job.JobTown);
        cmd.Parameters.AddWithValue("@County", _job.JobCounty);
        cmd.Parameters.AddWithValue("@RecruiterName", _job.RecruiterName);
        cmd.Parameters.AddWithValue("@RecruiterNumber", _job.RecruiterNumber);
        cmd.Parameters.AddWithValue("@RecruiterEmail", _job.RecruiterEmail);
        cmd.Parameters.AddWithValue("@ApplicationNotes", _job.ApplicationNotes);

        try
        {
            using (con)
            {
                con.Open();

                newID = (int)cmd.ExecuteScalar();
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
        return newID;
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