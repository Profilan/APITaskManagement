namespace APITaskManagement.Logic.Common
{
    public enum Unit
    {
        Hours = 1,
        Minutes = 2,
        Seconds = 3
    }

    public enum HttpMethod
    {
        Get = 1,
        Post = 2,
        Put = 3,
        Patch = 4,
        Delete = 5
    }

    public enum ErrorType
    {
        EMERG = 0,  // Emergency: system is unusable
        ALERT = 1,  // Alert: action must be taken immediately
        CRIT = 2,   // Critical: critical conditions
        ERR = 3,    // Error: error conditions
        WARN = 4,   // Warning: warning conditions
        NOTICE = 5, // Notice: normal but significant condition
        INFO = 6,   // Informational: informational messages
        DEBUG = 7,   // Debug: debug messages
        NONE = 8
    }

    public enum AuthenticationType
    {
        None = 0,
        Basic = 1,
        OAuth2 = 2,
        Windows = 3,
        ApiKey = 4,
        Header = 5
    }

    public enum TaskType
    {
        API = 1,
        FTP = 2,
        FILE = 3,
        MAIL = 4
    }

    public enum FtpSyncMethod
    {
        Upload = 1,
        Download = 2,
        Synchonize = 3
    }

    public enum FtpConnectionType
    {
        FTP = 1,
        SFTP = 2
    }

    public enum ContentFormat
    {
        JSON = 1,
        XML = 2,
        TXT = 3
    }
}
