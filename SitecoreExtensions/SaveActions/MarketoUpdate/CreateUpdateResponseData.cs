namespace SitecoreExtensions.SaveActions.MarketoUpdate
{
    using System.Collections.Generic;

    public class CreateUpdateResponseData
    {
        public string requestId { get; set; }
        public List<Result> result { get; set; }
        public List<Error> errors { get; set; }
        public bool success { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string status { get; set; }

        public List<Reason> reasons { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Reason
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}