﻿using System;
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

    /// <summary>
    /// Process request method, called by web.config if /applications is hit.
    /// Coordinates routes through the code, and distributes to following methods
    /// </summary>
    /// <param name="_context">HTTPContext of the request</param>
    public void ProcessRequest(HttpContext _context)
    {
        HttpRequest request = _context.Request;


        var x = request.Url.AbsoluteUri.Split('/');


        Uri baseAddress = new Uri("http://xserve.uopnet.plymouth.ac.uk/Modules/SOFT338/alea/");
        //for local debugging
        //Uri baseAddress = new Uri("http://localhost:13946/");
        UriTemplate idTemplate = new UriTemplate("applications/{id}");

        UriTemplateMatch matchResults = idTemplate.Match(baseAddress, new Uri(request.Url.AbsoluteUri));

        string apiKey = Utils.isAuthenticated(request);

        if (!String.IsNullOrEmpty(apiKey))
        {
            if (matchResults != null) //must have an ID
            {
                try
                {
                    int applicationId = int.Parse(matchResults.BoundVariables.GetValues(0).First().ToString());

                    if (applicationId > 0)
                    {
                        switch (request.HttpMethod.ToLower())
                        {
                            case "get":
                                //get individual
                                get(_context, applicationId);
                                break;
                            case "put":
                                //update individual
                                update(_context, applicationId);
                                break;
                            case "delete":
                                //delete individual
                                delete(_context, applicationId);
                                break;
                            default:
                                _context.Response.StatusCode = 405;
                                break;
                        }
                    }
                }
                catch
                {
                    _context.Response.StatusCode = 404;
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

    /// <summary>
    /// Coordinates retrieval of all applications from DB and writes JSON 
    /// to output stream
    /// </summary>
    /// <param name="_context">HTTPContext of the request</param>
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

    /// <summary>
    /// Coordinates retrieval of a single application from DB and writes
    /// JSON to output steam
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_id">ID of the </param>
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
            _context.Response.StatusCode = 404;
        }
    }

    /// <summary>
    /// Coordinates insertion of a new application into the DB
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_apiKey">API key, to identiy user associated with application</param>
    private void insert(HttpContext _context, string _apiKey)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JobApplication));

        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);

        if (job.isPostcodeValid())
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
                _context.Response.StatusCode = 400;
            }
        }
        else
        {
            _context.Response.StatusCode = 422;
        }
    }

    /// <summary>
    /// Coordinates updation of application
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_id">ID of application to update</param>
    private void update(HttpContext _context, int _id)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JobApplication));
        JobApplication job = (JobApplication)json.ReadObject(_context.Request.InputStream);
        job.Id = _id;
        if (job.isPostcodeValid())
        {
            bool success = JobApplicationDB.update(job, _id);

            if (success)
            {
                Utils.outputJson(_context, job, json);
                _context.Response.StatusCode = 200;
            }
            else
            {
                _context.Response.StatusCode = 400;
            }
        }
        else
        {
            _context.Response.StatusCode = 422;
        }
    }

    /// <summary>
    /// Coordinates deletion of a job application
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_id">ID of application to delete</param>
    private void delete(HttpContext _context, int _id)
    {
        bool success = JobApplicationDB.delete(_id);

        if (success)
        {
            _context.Response.StatusCode = 204;
        }
        else
        {
            _context.Response.StatusCode = 400;
        }
    }
}