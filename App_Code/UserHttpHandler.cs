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

   ///<summary>
    /// Process request method, called by web.config if /users is hit.
    /// Coordinates routes through the code, and distributes to following methods
    /// </summary>
    /// <param name="_context">HTTPContext of the request</param>
    public void ProcessRequest(HttpContext _context)
    {
        HttpRequest request = _context.Request;

        //get folder bit of path
        Uri uri = new Uri(request.Url.AbsoluteUri);

        Uri baseAddress = new Uri("http://xserve.uopnet.plymouth.ac.uk/Modules/SOFT338/alea/");
        UriTemplate idTemplate = new UriTemplate("users/{id}");

        UriTemplateMatch matchResults = idTemplate.Match(baseAddress, uri);


        if (!String.IsNullOrEmpty(Utils.isAuthenticated(request)))
        {
            if (matchResults != null) //must have an ID
            {
                try
                {
                    int id = int.Parse(matchResults.BoundVariables.GetValues(0).First().ToString());

                    if (id > 0)
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
                        insert(_context);
                        break;
                    default:
                        _context.Response.StatusCode = 405;
                        break;
                }
            }
        }
        else if(request.HttpMethod.ToLower() == "post")
        {
            insert(_context);
        }
        else
        {
            _context.Response.StatusCode = 401;
        }
    }

    ///<summary>
    /// Coordinates retrieval of all users from DB and writes JSON 
    /// to output stream
    /// </summary>
    /// <param name="_context">HTTPContext of the request</param>
    private void getAll(HttpContext _context)
    {
        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        IEnumerable<User> userList = UserDB.getList();

        if (userList.Count() > 0)
        {
            Utils.outputJson(_context, 
                userList, 
                new DataContractJsonSerializer(typeof(IEnumerable<User>)));
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 204;
        }
    }

   /// <summary>
    /// Coordinates retrieval of a single user from DB and writes
    /// JSON to output steam
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_id">ID of the user</param>
    private void get(HttpContext _context, int _id)
    {
        //Get a list of Logs - note we need it as an IEnumerable object otherwise the serializer can't cope.
        User u = UserDB.get(_id);

        if (u != null)
        {
            Utils.outputJson(_context, u, new DataContractJsonSerializer(typeof(User)));
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 404;
        }
    }

///<summary>
    /// Coordinates insertion of a new user into the DB
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_apiKey">API key, to identiy user associated with user</param>
    private void insert(HttpContext _context)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));

        User user = (User)json.ReadObject(_context.Request.InputStream);
        user.Key = Guid.NewGuid().ToString();

        user = UserDB.insert(user);

        if (user != null)
        {            
            Utils.outputJson(_context, user, json);
            _context.Response.StatusCode = 201;
        }
        else
        {
            _context.Response.StatusCode = 400;
        }
    }

    /// <summary>
    /// Coordinates updation of user
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_id">ID of user to update</param>
    private void update(HttpContext _context, int _id)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));

        User user = (User)json.ReadObject(_context.Request.InputStream);

        bool success = UserDB.update(user, _id);
        user.Id = _id;
        if (success)
        {
            Utils.outputJson(_context, user, json);
            _context.Response.StatusCode = 200;
        }
        else
        {
            _context.Response.StatusCode = 400;
        }
    }

    /// <summary>
    /// Coordinates deletion of a user
    /// </summary>
    /// <param name="_context">HTTPContext of the API request</param>
    /// <param name="_id">ID of user to delete</param>
    private void delete(HttpContext _context, int _id)
    {
        bool success = UserDB.delete(_id);

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