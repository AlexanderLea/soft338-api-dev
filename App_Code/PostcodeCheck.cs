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
    public int status { get; set; }

    public bool result { get; set; }

    public PostcodeCheck()
	{		
	}

    public PostcodeCheck(int _status, bool _result)
    {
        this.status = _status;
        this.result = _result;
    }


}