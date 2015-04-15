namespace SitecoreExtensions.SaveActions.MarketoUpdate
{
    using System.Collections.Generic;

    public class CreateUpdateData
    {
        public string action { get; set; }
        public string lookupField { get; set; }
        public string partitionName { get; set; }
        public List<Input> input { get; set; }
    }

    public class Input
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string postalCode { get; set; }
    }
}