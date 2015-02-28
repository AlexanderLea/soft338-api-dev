using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

/// <summary>
/// HttpHandler handles HTTP Requests aimed at /log/{id}
/// </summary>
/// TODO:
///     - Create database
///     - Implement basic API
///     - Comply with best practises
///     

public class HttpHandler : IHttpHandler
{
    public bool IsReusable { get { return true; } }

    public void ProcessRequest(HttpContext _context)
    {
        HttpRequest request = _context.Request;

        //get folder bit of path
        Uri uri = new Uri(request.Url.AbsoluteUri);

        Uri baseAddress = new Uri("http://xserve.uopnet.plymouth.ac.uk/Modules/SOFT338/alea/");
        UriTemplate idTemplate = new UriTemplate("applications/{id}");
        UriTemplate defaultTemplate = new UriTemplate("applications");

        UriTemplateMatch matchResults = idTemplate.Match(baseAddress, uri);

        if (matchResults != null) //must have an ID
        {
            string strID = matchResults.BoundVariables.GetValues(0).First().ToString();
            int id;

            if (int.TryParse(strID, out id))
            {
                switch (request.HttpMethod.ToLower())
                {
                    case "get":
                        //get individual
                        //get(_context, id);
                        _context.Response.Write("ID: " + id + ", Method: get");
                        break;
                    case "put":
                        //update individual
                        //update(_context, id);
                        _context.Response.Write("ID: " + id + ", Method: put");
                        break;
                    case "delete":
                        //delete individual
                        //delete(_context, id);
                        _context.Response.Write("ID: " + id + ", Method: delete");
                        break;
                    default:
                        break;
                }
            }
        }
        else //default
        {
            switch (request.HttpMethod.ToLower())
            {
                case "get":
                    //get list
                    listAll(_context);
                    break;
                case "post":
                    //insert
                    insert(_context);
                    //_context.Response.Write("Post");
                    break;
                default:
                    break;
            }
        }
    }

    private void listAll(HttpContext _context)
    {
        Stream outputStream = _context.Response.OutputStream;

        // Notify caller that the response resource is in JSON.
        _context.Response.ContentType = "application/json";

        //Create the new serializer object - NOTE the type entered into the constructor!
        DataContractJsonSerializer jsonData = new DataContractJsonSerializer(typeof(IEnumerable<JobApplication>));

        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        IEnumerable<JobApplication> appList = JobApplicationDB.getList();
        jsonData.WriteObject(outputStream, appList);
    }

    private void get(HttpContext _context, int _id)
    {
        throw new NotImplementedException();
    }

    private void insert(HttpContext _context)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JobApplication));

        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);

        int id = JobApplicationDB.insert(job);


        if (id != -1)
        {
            _context.Response.StatusDescription = "http://xserve.uopnet.plymouth.ac.uk/modules/soft338/alea/applications/" + id;
            _context.Response.StatusCode = 201;
        }
        else
        {
            _context.Response.StatusCode = 500;
            _context.Response.StatusDescription = JobApplicationDB.ErrorMessage;
        }
        //
        //TODO: return newly inserted object, or error!
    }

    private void update(HttpContext _context, int _id)
    {
        throw new NotImplementedException();
    }

    private void delete(HttpContext _context, int _id)
    {
        throw new NotImplementedException();
    }
}