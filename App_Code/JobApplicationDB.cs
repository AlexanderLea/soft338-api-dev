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

    /// <summary>
    /// Gets list of job applications from DB
    /// </summary>
    /// <returns>List of JobApplications</returns>
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
                        (int)reader["ID"],
                        (string)reader["JobTitle"],
                        (string)reader["JobDescription"],
                        (string)reader["CompanyName"],                        
                        (string)reader["BusinessSector"],
                        (string)reader["Postcode"],
                        (string)reader["Town"],
                        (string)reader["County"],
                        (string)reader["RecruiterName"],
                        (string)reader["RecruiterNumber"],
                        (string)reader["RecruiterEmail"],                        
                        (string)reader["ApplicationNotes"],
                        (int)reader["UserID"]); 

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

    /// <summary>
    /// Get single job application from DB
    /// </summary>
    /// <param name="_id">ID of requested job application</param>
    /// <returns>JobApplication related to the given ID</returns>
    public static JobApplication get(int _id)
    {
        JobApplication temp = null;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM JobApplications WHERE ID = @id", con);

        cmd.Parameters.AddWithValue("@id", _id);

        try
        {
            using (con)
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    temp = new JobApplication(
                       (int)reader["ID"],
                       (string)reader["JobTitle"],
                       (string)reader["JobDescription"],
                       (string)reader["CompanyName"],
                       (string)reader["BusinessSector"],
                       (string)reader["Postcode"],
                       (string)reader["Town"],
                       (string)reader["County"],
                       (string)reader["RecruiterName"],
                       (string)reader["RecruiterNumber"],
                       (string)reader["RecruiterEmail"],
                       (string)reader["ApplicationNotes"],
                       (int)reader["UserID"]);
                }
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return temp;
    }

    /// <summary>
    /// Inserts Job Application into database
    /// </summary>
    /// <param name="_job">JobApplication to insert</param>
    /// <returns>Inserted JobApplication (inc. ID)</returns>
    public static JobApplication insert(JobApplication _job)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO JobApplications ([JobTitle], [CompanyName], [JobDescription], "
            + "[BusinessSector], [Postcode], [Town], [County], [RecruiterName], [RecruiterNumber], [RecruiterEmail], "
            + "[ApplicationNotes], [UserID]) "
            + "VALUES (@JobTitle, @CompanyName, @JobDescription, @BusinessSector, @PostCode, @Town, @County, @RecruiterName, "
            + "@RecruiterNumber, @RecruiterEmail, @ApplicationNotes, @UserID); SELECT CAST(Scope_Identity() as int);", con);

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
        cmd.Parameters.AddWithValue("@UserID", _job.UserID);

        try
        {
            using (con)
            {
                con.Open();

                _job.Id = (int)cmd.ExecuteScalar();
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            _job = null;
        }
        return _job;
    }

    /// <summary>
    /// Updates Job Application in database
    /// </summary>
    /// <param name="_job">Job Application containing updated values</param>
    /// <param name="_id">ID of application to update</param>
    /// <returns></returns>
    public static bool update(JobApplication _job, int _id)
    {
        bool successful;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE JobApplications SET [JobTitle] = @JobTitle, [CompanyName] = @CompanyName, "
            + "[JobDescription] = @JobDescription, [BusinessSector] = @BusinessSector, [Postcode] = @PostCode, [Town] = @Town, "
            + "[County] = @County, [RecruiterName] = @RecruiterName, [RecruiterNumber] = @RecruiterNumber, [RecruiterEmail] = @RecruiterEmail, "
            + "[ApplicationNotes] =  @ApplicationNotes WHERE [ID] = @ID", con);

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
        cmd.Parameters.AddWithValue("@ID", _id);

        try
        {
            using (con)
            {
                con.Open();

                cmd.ExecuteNonQuery();
                successful = true;
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            successful = false;
        }

        return successful;
    }

    /// <summary>
    /// Deletes Job Application from DB
    /// </summary>
    /// <param name="_id">ID of application to delete</param>
    /// <returns>true if deleted successfull; false otherwise</returns>
    public static bool delete(int _id)
    {
        bool successful;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DELETE FROM JobApplications WHERE [ID] = @ID", con);

        cmd.Parameters.AddWithValue("@ID", _id);

        try
        {
            using (con)
            {
                con.Open();

                cmd.ExecuteNonQuery();
                successful = true;
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            successful = false;
        }

        return successful;
    }
}