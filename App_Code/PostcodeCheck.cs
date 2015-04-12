using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for Postcode
/// </summary>
public class PostcodeCheck
{
    private int _status;
    [DataMember]
    public int Status
    {
        get { return _status; }
        set { _status = value; }
    }

    private string _result;
    [DataMember]
    public string Result
    {
        get { return _result; }
        set { _result = value; }
    }

    public PostcodeCheck()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public PostcodeCheck(string _status, string _result)
    {
        this.Status = _status;
        this.Result = _result;
    }


}