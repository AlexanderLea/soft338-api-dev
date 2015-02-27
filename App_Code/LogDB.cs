using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogDB
/// </summary>
public class LogDB
{
    private static string conString = "";
    public static string errorMessage;

	public LogDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static List<Log> getList()
    {
        throw new NotImplementedException();
    }

    //get
    public static Log get(int id){
        throw new NotImplementedException();
    }

    //post
    public static int insert(Log _log){
        throw new NotImplementedException();
    }

    //put
    public static int update(Log _log){
        throw new NotImplementedException();
    }

    //delete
    public static int delete(Log _log){
        throw new NotImplementedException();
    }
}