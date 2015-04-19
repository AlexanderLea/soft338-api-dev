using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// User class to hold information relating to user DB obeject
/// </summary>
/// 

[DataContract] 
public class User
{
    //ID
    private int _id;
    [DataMember]
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    //Email
    private string _email;
    [DataMember]
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    private string _key;
    [DataMember]
    public string Key
    {
        get { return _key; }
        set { _key = value; }
    }

    public User()
    {

    }

    /// <summary>
    /// Parameterised constructor
    /// </summary>
    /// <param name="_id">ID of user object from DB</param>
    /// <param name="_email">User's email address</param>
    /// <param name="_key">User's API key</param>
    public User(int _id, string _email, string _key)
    {
        this.Id = _id;
        this.Email = _email;
        this.Key = _key;
    }

    /// <summary>
    /// Coordinates checking if the user is authenticated
    /// </summary>
    /// <param name="_key">User's API key</param>
    /// <returns>bool - true/false depending on authentication status</returns>
    public static bool isAuthenticated(string _key)
    {
        if (UserDB.getUserFromKey(_key) != -1)
            return true;
        else
            return false;

    }
}