using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for UserDB
/// </summary>
public class UserDB
{
        private static string connectionString = WebConfigurationManager.ConnectionStrings["SOFT338_ConnectionString"].ConnectionString;
    public static string ErrorMessage;

	public static List<User> getList(){
        List<User> users = new List<User>();

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);

        try
        {
            using (con)
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User temp = new User(
                        (string)reader["Email"],
                        (string)reader["ApiKey"]);

                    users.Add(temp);
                }
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return users;
    }

    public static User get(int _id)
    {
        User temp = null;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE ID = @id", con);

        cmd.Parameters.AddWithValue("@id", _id);

        try
        {
            using (con)
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    temp = new User(
                        (string)reader["Email"],
                        (string)reader["ApiKey"]);
                }
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return temp;
    }

    public static int insert(User _user)
    {
        int newID = -1;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO Users ([Email], [ApiKey]"
            + "VALUES (@Email, @ApiKey); SELECT CAST(Scope_Identity() as int);", con);

        cmd.Parameters.AddWithValue("@Email", _user.Email);
        cmd.Parameters.AddWithValue("@ApiKey", _user.Key);

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

    public static bool update(User _user, int _id)
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

    public static bool delete(int _id)
    {
        throw new NotImplementedException();
    }


}