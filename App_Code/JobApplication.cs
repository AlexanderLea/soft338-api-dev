using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Net.Http;

/// <summary>
/// Summary description for JobApplication
/// </summary>

[DataContract]
public class JobApplication
{
    //Job Id
    private int _id;
    [DataMember]
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    //Job Title
    private string _jobTitle;
    [DataMember]
    public string JobTitle
    {
        get { return _jobTitle; }
        set { _jobTitle = value; }
    }

    //Description
    private string _jobDescription;
    [DataMember]
    public string JobDescription
    {
        get { return _jobDescription; }
        set { _jobDescription = value; }
    }

    //Company name
    private string _companyName;
    [DataMember]
    public string CompanyName
    {
        get { return _companyName; }
        set { _companyName = value; }
    }

    //Company business Sector
    private string _businessSector;
    [DataMember]
    public string BusinessSector
    {
        get { return _businessSector; }
        set { _businessSector = value; }
    }

    //Postcode
    private string _jobPostcode;
    [DataMember]
    public string JobPostcode
    {
        get { return _jobPostcode; }
        set { _jobPostcode = value; }
    }

    //Town
    private string _jobTown;
    [DataMember]
    public string JobTown
    {
        get { return _jobTown; }
        set { _jobTown = value; }
    }

    //County
    private string _jobCounty;
    [DataMember]
    public string JobCounty
    {
        get { return _jobCounty; }
        set { _jobCounty = value; }
    }

    //Salary
    //private int _salary;
    //[DataMember]
    //public int Salary
    //{
    //    get { return _salary; }
    //    set { _salary = value; }
    //}

    //Recruiter Name
    private string _recruiterName;
    [DataMember]
    public string RecruiterName
    {
        get { return _recruiterName; }
        set { _recruiterName = value; }
    }

    //Recruiter Contact
    private string _recruiterNumber;
    [DataMember]
    public string RecruiterNumber
    {
        get { return _recruiterNumber; }
        set { _recruiterNumber = value; }
    }

    //Recruiter Email
    private string _recruiterEmail;
    [DataMember]
    public string RecruiterEmail
    {
        get { return _recruiterEmail; }
        set { _recruiterEmail = value; }
    }

    //Application Notes
    private string _applicationNotes;
    [DataMember]
    public string ApplicationNotes
    {
        get { return _applicationNotes; }
        set { _applicationNotes = value; }
    }

    public JobApplication()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Constructor without town info, for creating applications from user input
    /// </summary>
    /// <param name="_jobTitle">Title of job</param>
    /// <param name="_companyName">Name of Company</param>
    /// <param name="_businessSector">Business Sector in which the company operates</param>
    /// <param name="_postcode">Job location postcode</param>
    /// <param name="_recruiterName">Name of recruiting peron</param>
    /// <param name="_recruiterNumber">Phont number of recruiter</param>
    /// <param name="_recruiterEmail">Email address of recruiter</param>
    /// <param name="_applicationNotes">Notes on the job application</param>
    public JobApplication(int _id, string _jobTitle, string _jobDesc, string _companyName, string _businessSector,
         string _postcode, string _town, string _county, string _recruiterName, string _recruiterNumber,
             string _recruiterEmail, string _applicationNotes)
    {
        this.Id = _id;
        this.JobTitle = _jobTitle;
        this.JobDescription = _jobDesc;
        this.CompanyName = _companyName;
        this.BusinessSector = _businessSector;
        this.JobPostcode = _postcode;
        this.JobTown = _town;
        this.JobCounty = _county;
        this.RecruiterName = _recruiterName;
        this.RecruiterNumber = _recruiterNumber;
        this.RecruiterEmail = _recruiterEmail;
        this.ApplicationNotes = _applicationNotes;
    }

    /// <summary>
    /// Constructor with all params, for pulling info from database
    /// </summary>
    /// <param name="_jobTitle">Title of job</param>
    /// <param name="_companyName">Name of Company</param>
    /// <param name="_businessSector">Business Sector in which the company operates</param>
    /// <param name="_postcode">Job location postcode</param>
    /// <param name="_town">Town in which the job is based</param>
    /// <param name="_county">County in which the job is based</param>
    /// <param name="_recruiterName">Name of recruiting peron</param>
    /// <param name="_recruiterNumber">Phont number of recruiter</param>
    /// <param name="_recruiterEmail">Email address of recruiter</param>
    /// <param name="_applicationNotes">Notes on the job application</param>
    public JobApplication(string _jobTitle, string _jobDesc, string _companyName, string _businessSector,
        string _postcode, string _town, string _county, string _recruiterName, string _recruiterNumber,
            string _recruiterEmail, string _applicationNotes)
    {
        this.JobTitle = _jobTitle;
        this.JobDescription = _jobDesc;
        this.CompanyName = _companyName;
        this.BusinessSector = _businessSector;
        this.JobPostcode = _postcode;
        this.JobTown = _town;
        this.JobCounty = _county;
        this.RecruiterName = _recruiterName;
        this.RecruiterNumber = _recruiterNumber;
        this.RecruiterEmail = _recruiterEmail;
        this.ApplicationNotes = _applicationNotes;
    }

    public void getLocationData()
    {
        //string URL = "https://sub.domain.com/objects.json";
        //string urlParameters = "?api_key=123";

        //HttpClient client = new HttpClient();
        //client.BaseAddress = new Uri(URL);

        //// Add an Accept header for JSON format.
        //client.DefaultRequestHeaders.Accept.Add(
        //new MediaTypeWithQualityHeaderValue("application/json"));

        //// List data response.
        //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
        //if (response.IsSuccessStatusCode)
        //{
        //    // Parse the response body. Blocking!
        //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
        //    foreach (var d in dataObjects)
        //    {
        //        Console.WriteLine("{0}", d.Name);
        //    }
        //}
        //else
        //{
        //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        //}  
    }
}