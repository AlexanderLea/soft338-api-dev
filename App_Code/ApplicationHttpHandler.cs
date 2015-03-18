using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

/// <summary>
/// HttpHandler handles HTTP Requests aimed at /applications/{id}
/// </summary>   

public class ApplicationHttpHandler : IHttpHandler
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
                        get(_context, id);
                        break;
                    case "put":
                        //update individual
                        update(_context, id);
                        break;
                    case "delete":
                        //delete individual
                        delete(_context, id);
                        break;
                    default:
                        _context.Response.StatusCode = 405;
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
                    getAll(_context);
                    break;
                case "post":
                    //insert
                    insert(_context);
                    break;
                default:
                    _context.Response.StatusCode = 405;
                    break;
            }
        }
    }

    private void getAll(HttpContext _context)
    {
        Stream outputStream = _context.Response.OutputStream;

        // Notify caller that the response resource is in JSON.
        _context.Response.ContentType = "application/json";

        //Create the new serializer object - NOTE the type entered into the constructor!
        DataContractJsonSerializer jsonData = new DataContractJsonSerializer(typeof(IEnumerable<JobApplication>));

        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        IEnumerable<JobApplication> appList = JobApplicationDB.getList();

        if (appList.Count() > 0)
        {
            jsonData.WriteObject(outputStream, appList);
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 204;
            _context.Response.StatusDescription = "No data";
        }
    }

    private void get(HttpContext _context, int _id)
    {
        Stream outputStream = _context.Response.OutputStream;

        // Notify caller that the response resource is in JSON.
        _context.Response.ContentType = "application/json";

        //Create the new serializer object - NOTE the type entered into the constructor!
        DataContractJsonSerializer jsonData = new DataContractJsonSerializer(typeof(JobApplication));

        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        JobApplication app = JobApplicationDB.get(_id);

        if (app != null)
        {
            jsonData.WriteObject(outputStream, app);
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 204;
            _context.Response.StatusDescription = "No data";
        }
    }

    private void insert(HttpContext _context)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JobApplication));

        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);

        if (job.JobPostcode != null)
        {
            //get location information
        }

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
    }

    private void update(HttpContext _context, int _id)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JobApplication));

        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);

        bool success = JobApplicationDB.update(job, _id);

        if (success)
        {
            _context.Response.StatusDescription = "http://xserve.uopnet.plymouth.ac.uk/modules/soft338/alea/applications/" + _id;
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 500;
            _context.Response.StatusDescription = JobApplicationDB.ErrorMessage;
        }
    }

    private void delete(HttpContext _context, int _id)
    {
        bool success = JobApplicationDB.delete(_id);

        if (success)
        {           
            _context.Response.StatusCode = 204;
        }
        else
        {
            _context.Response.StatusCode = 500;
            _context.Response.StatusDescription = JobApplicationDB.ErrorMessage;
        }
    }
}