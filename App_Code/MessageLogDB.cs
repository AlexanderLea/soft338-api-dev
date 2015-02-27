using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MessageMessageLogDB
/// </summary>
public class MessageLogDB
{
    private static string _connectionString = "";
    public static string ErrorMessage;

    public MessageLogDB()
    {
        //
        // TODO: Add constructor MessageLogic here
        //
    }

    public static List<MessageLog> getList()
    {
        List<MessageLog> MessageLogList = new List<MessageLog>();

        SqlConnection con = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM message_log", con);

        try
        {
            using (con)
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MessageLog temp = new MessageLog((string)reader["sender_mac"],
                        (string)reader["recipient_mac"], (string)reader["cmd_type"],
                        (string)reader["cmd"], (DateTime)reader["timestamp"]);
                    MessageLogList.Add(temp);
                }
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return MessageLogList;
    }

    //get
    public static MessageLog get(int id)
    {
        MessageLog temp = new MessageLog(); ;

        SqlConnection con = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM message_log WHERE id = @id", con);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            using (con)
            {
                con.Open();


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    temp = new MessageLog((string)reader["sender_mac"],
                        (string)reader["recipient_mac"], (string)reader["cmd_type"],
                        (string)reader["cmd"], (DateTime)reader["timestamp"]);
                }
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return temp;
    }

    //post
    public static MessageLog insert(MessageLog _MessageLog)
    {
        SqlConnection con = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("INSERT into", con);
        cmd.Parameters.AddWithValue("","");

        try
        {
            using (con)
            {
                con.Open();

            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return new MessageLog();
    }

    //put
    public static MessageLog update(MessageLog _MessageLog)
    {
        SqlConnection con = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE message_log SET s", con);
        cmd.Parameters.AddWithValue("", "");

        try
        {
            using (con)
            {
                con.Open();

            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return new MessageLog();
    }

    //delete
    public static int delete(int _id)
    {
        SqlConnection con = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("DELETE * FROM message_log WHERE @id = id", con);
        cmd.Parameters.AddWithValue("@id", _id);

        try
        {
            using (con)
            {
                con.Open();

            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

        return -1;
    }
}