using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Postcode check class to store Postcodes.io response to postcode validity request
/// </summary>
public class PostcodeCheck
{
    public int status { get; set; }

    public bool result { get; set; }

    public PostcodeCheck()
	{		
	}

    /// <summary>
    /// Parameterised constructor
    /// </summary>
    /// <param name="_status">status returned from the HTTP GET request</param>
    /// <param name="_result">true/false depending on the validity of the postcode</param>
    public PostcodeCheck(int _status, bool _result)
    {
        this.status = _status;
        this.result = _result;
    }

    /// <summary>
    /// Coordinates the validation of the postcode against postcodes.io 3rd party API
    /// </summary>
    /// <param name="_postcode"></param>
    /// <returns></returns>
    internal static bool validatePostcode(string _postcode)
    {
        //use different serializer, because it doesn't require everything to be xml
        var serializer = new JavaScriptSerializer();
        PostcodeCheck pcheck = new PostcodeCheck();

        //check if postcode is valid
        string apiUrl = "http://api.postcodes.io/postcodes/?/validate";
        apiUrl = apiUrl.Replace("?", _postcode);

        using (var client = new WebClient())
        {
            client.Headers.Add("Content-Type", "application/json");

            Stream data = client.OpenRead(apiUrl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            pcheck = serializer.Deserialize<PostcodeCheck>(s);

            data.Close();
            reader.Close();
        }

        if (pcheck.result == true && pcheck.status == 200)
            return true;
        else
            return false;
    }
}