using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

/// <summary>
/// Summary description for UserHttpHandler
/// </summary>
public class UserHttpHandler : IHttpHandler
{
    public bool IsReusable { get { return true; } }

    public void ProcessRequest(HttpContext _context)
    {
        HttpRequest request = _context.Request;

        //get folder bit of path
        Uri uri = new Uri(request.Url.AbsoluteUri);

        Uri baseAddress = new Uri("http://xserve.uopnet.plymouth.ac.uk/Modules/SOFT338/alea/");
        UriTemplate idTemplate = new UriTemplate("users/{id}");
        UriTemplate defaultTemplate = new UriTemplate("users");

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
        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        IEnumerable<User> userList = UserDB.getList();

        if (userList.Count() > 0)
        {
            outputJson(_context, userList, new DataContractJsonSerializer(typeof(IEnumerable<User>)));
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 204;
            _context.Response.StatusDescription = "No data";
        }
    }

    private void outputJson(HttpContext _context, object _d, DataContractJsonSerializer _jsonData)
    {
        var _data = _d;

        Stream outputStream = _context.Response.OutputStream;

        // Notify caller that the response resource is in JSON.
        _context.Response.ContentType = "application/json";

        _jsonData.WriteObject(outputStream, _data);
    }

    private void get(HttpContext _context, int _id)
    {
        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        User u = UserDB.get(_id);

        if (u != null)
        {
            outputJson(_context, u, new DataContractJsonSerializer(typeof(User)));
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
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));

        User user = (User)json.ReadObject(_context.Request.InputStream);
        user.Key = Guid.NewGuid().ToString();

        int id = UserDB.insert(user);

        if (id != -1)
        {
            _context.Response.StatusDescription = "http://xserve.uopnet.plymouth.ac.uk/modules/soft338/alea/users/" + id;
            outputJson(_context, user, json);
            _context.Response.StatusCode = 201;
        }
        else
        {
            _context.Response.StatusCode = 422;
            _context.Response.StatusDescription = JobApplicationDB.ErrorMessage;
        }
    }

    private void update(HttpContext _context, int _id)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));

        User user = (User)json.ReadObject(_context.Request.InputStream);

        bool success = UserDB.update(user, _id);

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
        bool success = UserDB.delete(_id);

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