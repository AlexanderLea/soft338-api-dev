using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Utils class providing objects and methods that have no place elsewhere
/// </summary>
public class Utils
{
    /// <summary>
    /// Retrieves application API key from request
    /// </summary>
    /// <param name="_req">HTTPRequest from request</param>
    /// <returns>API Key as a string</returns>
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

    
}