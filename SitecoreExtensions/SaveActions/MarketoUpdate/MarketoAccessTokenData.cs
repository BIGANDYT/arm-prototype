namespace SitecoreExtensions.SaveActions.MarketoUpdate
{
    public class MarketoAccessTokenData
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string ExpiresIn { get; set; }
        public string Scope { get; set; }

        // {
        //    "accesstoken": "8f9ba030-9bd1-4641-9cbd-c3cad974e796:lon",
        //    "tokentype": "bearer",
        //    "expiresin": 3599,
        //    "scope": "robert.naicker.madev+api@arm.com"
        //}
    }
}