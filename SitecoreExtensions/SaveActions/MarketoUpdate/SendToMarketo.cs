namespace SitecoreExtensions.SaveActions.MarketoUpdate
{
    using System;
    using System.Collections.Generic;

    using RestSharp;

    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Form.Core.Client.Data.Submit;
    using Sitecore.Form.Submit;

    public class SendToMarketo : ISaveAction
    {
        public void Execute(ID formid, AdaptedResultList fields, params object[] data)
        {
            Log.Info("Sending to marketo", this);
            try
            {
                var firstName = fields.GetEntryByName("First name").Value;
                var email = fields.GetEntryByName("Email").Value;
                var postcode = fields.GetEntryByName("Postcode").Value;

                this.CreateUpdateLead(firstName, email, postcode);
                Log.Info("Sending to marketo succeeded", this);
            }
            catch (Exception ex)
            {
                Log.Error("Marketo Update failed", ex, this);
            }
        }

        private const string MarketoBaseUrl = "https://143-AAV-221.mktorest.com";
        private const string ClientId = "5b744e91-5d7e-404a-b108-7d0d74ab6fac";
        private const string ClientSecret = "dImLbFBcOZCEY973qcIVK9PIBUgVrlJF";

        private RestClient GetMarketoRestClient()
        {
            return new RestClient(MarketoBaseUrl);
        }

        public CreateUpdateResponseData CreateUpdateLead(string firstName, string email, string postcode)
        {
            var data = this.GetMarketoAccessTokenData();

            var client = this.GetMarketoRestClient();

            var request = new RestRequest("rest/v1/leads.json?access_token=" + data.AccessToken, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };


            var createData = this.CreateUpdateData(firstName, email, postcode);
            request.AddJsonBody(createData);

            var response = client.Execute<CreateUpdateResponseData>(request);

            return response.Data;
        }

        private MarketoAccessTokenData GetMarketoAccessTokenData()
        {
            var client = this.GetMarketoRestClient();

            var request = new RestRequest("identity/oauth/token", Method.GET);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecret);

            var response = client.Execute<MarketoAccessTokenData>(request);
            return response.Data;
        }

        private CreateUpdateData CreateUpdateData(string firstName, string email, string postcode)
        {
            if (string.IsNullOrWhiteSpace(firstName)) { throw new ArgumentException("firstName is required"); }
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentException("email is required"); }
            if (string.IsNullOrWhiteSpace(postcode)) { throw new ArgumentException("postcode is required"); }

            var createData = new CreateUpdateData()
            {
                action = "createOrUpdate",
                input = new List<Input>()
                {
                    new Input()
                    {
                        email = email,
                        firstName = firstName,
                        postalCode = postcode
                    }
                }
            };
            return createData;
        }
    }
}