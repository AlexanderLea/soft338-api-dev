using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{

    public static string isAuthenticated(HttpRequest _req)
    {
        string apiKey = "";

        if (_req.Headers["Application-ApiKey"] != null)
        {
            apiKey = _req.Headers.GetValues("Application-ApiKey").First();

            if (User.isAuthenticated(apiKey))
                return apiKey;
            else
                return "";
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// Method to output object _data as JSON
    /// </summary>
    /// <param name="_context">HTTPContext of Request</param>
    /// <param name="_d">Data object to output</param>
    /// <param name="_jsonData">DataContractJsonSerializer object to use to output JSON</param>
    public static void outputJson(HttpContext _context, object _data, DataContractJsonSerializer _jsonData)
    {
        var d = _data;

        Stream outputStream = _context.Response.OutputStream;

        // Notify caller that the response resource is in JSON.
        _context.Response.ContentType = "application/json";

        _jsonData.WriteObject(outputStream, d);
    }

    public static bool isPostcodeValid(JobApplication _job)
    {
        //use different serializer, because it doesn't require everything to be xml
        var serializer = new JavaScriptSerializer();
        PostcodeCheck pcheck = new PostcodeCheck();

        //check if postcode is valid
        string apiUrl = "http://api.postcodes.io/postcodes/?/validate";
        apiUrl = apiUrl.Replace("?", _job.JobPostcode);

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