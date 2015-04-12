using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;

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


        //THIS bit isn't necessary
        Uri baseAddress = new Uri("http://xserve.uopnet.plymouth.ac.uk/Modules/SOFT338/alea/");
        UriTemplate idTemplate = new UriTemplate("applications/{id}");

        UriTemplateMatch matchResults = idTemplate.Match(baseAddress, uri);

        string apiKey = Utils.isAuthenticated(request);

        if (!String.IsNullOrEmpty(apiKey))
        {
            if (matchResults != null) //must have an ID
            {
                int id;

                if (int.TryParse(
                    matchResults.BoundVariables.GetValues(0).First().ToString()
                    , out id))
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
                        insert(_context, apiKey);
                        break;
                    default:
                        _context.Response.StatusCode = 405;
                        break;
                }
            }
        }
        else
        {
            _context.Response.StatusCode = 401;
        }
    }

    private void getAll(HttpContext _context)
    {
        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        IEnumerable<JobApplication> appList = JobApplicationDB.getList();

        if (appList.Count() > 0)
        {
            Utils.outputJson(_context, 
                appList, 
                new DataContractJsonSerializer(typeof(IEnumerable<JobApplication>)));
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 204;
        }
    }

    private void get(HttpContext _context, int _id)
    {
        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        JobApplication app = JobApplicationDB.get(_id);

        if (app != null)
        {
            Utils.outputJson(_context,
                app,
                new DataContractJsonSerializer(typeof(JobApplication)));
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 204;
        }
    }

    private void insert(HttpContext _context, string _apiKey)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JobApplication));

        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);
        
        if (isPostcodeValid(job))
        {
            job.UserID = UserDB.getUserFromKey(_apiKey);

            job = JobApplicationDB.insert(job);

            if (job != null)
            {
                Utils.outputJson(_context, job, json);
                _context.Response.StatusCode = 201;
            }
            else
            {
                _context.Response.StatusCode = 500;
            }
        }
        else
        {
            _context.Response.StatusCode = 400;
        }
    }

    private bool isPostcodeValid(JobApplication _job)
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

    private void update(HttpContext _context, int _id)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(IEnumerable<JobApplication>))
        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);

        if (isPostcodeValid(job))
        {
            bool success = JobApplicationDB.update(job, _id);

            if (success)
            {
                Utils.outputJson(_context, job, json);
                _context.Response.StatusCode = 200;
            }
            else
            {
                _context.Response.StatusCode = 500;
            }
        }
        else
        {
            _context.Response.StatusCode = 400;
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
        }
    }
}