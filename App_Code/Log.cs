using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>
/// 

[DataContract]
public class Log
{
    //ID
    //FROM_MAC
    private string _SenderMAC;
    [DataMember]
    public string SenderMAC
    {
        get { return _SenderMAC; }
        set { _SenderMAC = value; }
    }
    
    //TO_MAC
    private string _RecipientMAC;
    [DataMember]
    public string RecipientMAC
    {
        get { return _RecipientMAC; }
        set { _RecipientMAC = value; }
    }

    //TIMESTAMP
    private DateTime _Timestamp;
    [DataMember]
    public DateTime Timestamp
    {
        get { return _Timestamp; }
        set { _Timestamp = value; }
    }

    //COMMAND_TYPE
    private string _CmdType;
    [DataMember]
    public string CmdType
    {
        get { return _CmdType; }
        set { _CmdType = value; }
    }

    //COMMAND
    private string _Cmd;
    [DataMember]
    public string Cmd
    {
        get { return _Cmd; }
        set { _Cmd = value; }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_senderMAC">MAC Address of message sender</param>
    /// <param name="_recipientMAC">MAC Address of message recipient</param>
    /// <param name="_cmdType">Type of command</param>
    /// <param name="_cmd">The command itself</param>
	public Log(string _senderMAC, string _recipientMAC, string _cmdType, string _cmd)
	{
        this.SenderMAC = _senderMAC;
        this.RecipientMAC = _recipientMAC;
        this.Timestamp = DateTime.Now;
        this.CmdType = _cmdType;
        this.Cmd = _cmd;
	}
}