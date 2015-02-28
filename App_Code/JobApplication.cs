using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for JobApplication
/// </summary>

[DataContract]
public class JobApplication
{
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
    private string _companySector;
    [DataMember]
    public string CompanySector
    {
        get { return _companySector; }
        set { _companySector = value; }
    }

    //Job Advert URL
    private string _jobAdURL;
    [DataMember]
    public string JobAdURL
    {
        get { return _jobAdURL; }
        set { _jobAdURL = value; }
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
    private int _salary;
    [DataMember]
    public int Salary
    {
        get { return _salary; }
        set { _salary = value; }
    }

    //Recruiter Name
    private string _recruiterName;
    [DataMember]
    public string RecruiterName
    {
        get { return _recruiterName; }
        set { _recruiterName = value; }
    }

    //Recruiter Contact
    private string _recruiterContact;
    [DataMember]
    public string RecruiterContact
    {
        get { return _recruiterContact; }
        set { _recruiterContact = value; }
    }


    public JobApplication()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}