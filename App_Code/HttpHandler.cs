using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

/// <summary>
/// Summary description for HttpHandler
/// </summary>
public class HttpHandler : IHttpHandler
{
    public bool IsReusable { get { return true; } }

    public void ProcessRequest(HttpContext _context)
    {
        HttpRequest request = _context.Request;

        //get folder bit of path
        string path = request.Path.Split('/').Last();

        Uri baseAddress = new Uri("http://xserve.uopnet.plymouth.ac.uk/Modules/SOFT338/alea/");
        UriTemplate idTemplate = new UriTemplate("{log}/{id}");
        UriTemplate defaultTemplate = new UriTemplate("{log}");

        

    }

    private void listAll(HttpContext _context)
    {
        Stream outputStream = _context.Response.OutputStream;

        // Notify caller that the response resource is in JSON.
        _context.Response.ContentType = "application/json";

        //Create the new serializer object - NOTE the type entered into the constructor!
        DataContractJsonSerializer jsonData = new DataContractJsonSerializer(typeof(IEnumerable<Log>));

        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        IEnumerable<Log> logList = LogDB.getList();
        jsonData.WriteObject(outputStream, logList);
    }

    private void get(HttpContext _context)
    {
        throw new NotImplementedException();
    }

    private void insert(HttpContext _context)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Log));

        Log l = (Log)json.ReadObject(_context.Request.InputStream);

        int lID = LogDB.insert(l);

        _context.Response.StatusDescription = "New Log saved. ID= " + lID;
        //TODO: return newly inserted object, or error!
    }

    private void update(HttpContext _context)
    {
        throw new NotImplementedException();
    }

    private void delete(HttpContext _context)
    {
        throw new NotImplementedException();
    }
}