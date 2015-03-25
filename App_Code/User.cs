using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for User
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

	public User(string _email, string _key)
	{
        this.Email = _email;
        this.Key = _key;
	}

    public User(int _id, string _email, string _key)
    {
        this.Id = _id;
        this.Email = _email;
        this.Key = _key;
    }

    public static bool isAuthenticated(string _key)
    {
        if (UserDB.getUserFromKey(_key) != -1)
            return true;
        else
            return false;

    }
}