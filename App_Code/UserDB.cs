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
        //Generate API key
        int newID = -1;

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO Users ([Email], [ApiKey])"
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
        SqlCommand cmd = new SqlCommand("UPDATE Users SET [Email] = @Email WHERE [ID] = @ID", con);

        cmd.Parameters.AddWithValue("@Email", _user.Email);
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