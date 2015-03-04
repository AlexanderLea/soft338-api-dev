using System;
using System.Collections.Generic;
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
        throw new NotImplementedException();
    }

    public static User get(int _id)
    {
        throw new NotImplementedException();
    }

    public static int insert(User _user)
    {
        throw new NotImplementedException();
    }

    public static bool update(User _user, int _id)
    {
        throw new NotImplementedException();
    }

    public static bool delete(int _id)
    {
        throw new NotImplementedException();
    }


}