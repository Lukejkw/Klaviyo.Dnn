using DotNetNuke.Common;
using Klaviyo.Dnn.Models;
using RestSharp;

namespace Klaviyo.Dnn
{
    /// <summary>
    /// C# Wrapper for calling the Klaviyo List API
    /// </summary>
    public class KlaviyoListApi
    {
        #region Private Members

        private const string LIST_MEMBER_URL = "api/v1/list/{LIST_ID}/members";

        private RestClient _client;

        private RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    InitializeClient();
                }
                return _client;
            }
        }

        private string ApiKey { get; set; }

        private string ListToken { get; set; }

        #endregion

        #region Constructors

        public KlaviyoListApi(string privateApiKey, string listToken)
        {
            Requires.NotNullOrEmpty("privateApiKey", privateApiKey);
            Requires.NotNullOrEmpty("listToken", listToken);

            ApiKey = privateApiKey;
            ListToken = listToken;
        }

        #endregion

        #region Private Methods

        private void InitializeClient()
        {
            _client = new RestClient("https://a.klaviyo.com/");
            _client.AddDefaultHeader("Accepts", "application/json");
            _client.AddDefaultHeader("Content-Type", "application/json");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a person as a subscriber to the list
        /// </summary>
        /// <param name="first">First Name</param>
        /// <param name="last">Last Name</param>
        /// <param name="email">Email</param>
        /// <returns>Api Response</returns>
        public ListMemberApiResponse Subscribe(string first, string last, string email)
        {
            return Subscribe(new Subscriber()
            {
                First = first,
                Last = last,
                Email = email
            });
        }

        /// <summary>
        /// Adds a user to the subscriber list
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns>Api Response</returns>
        public ListMemberApiResponse Subscribe(Subscriber subscriber)
        {
            Requires.NotNull("subscriber", subscriber);
            Requires.NotNullOrEmpty("subscriber.Email", subscriber.Email);

            // Init request
            var request = new RestRequest(LIST_MEMBER_URL, Method.POST);

            // Add param
            request.AddUrlSegment("LIST_ID", ListToken);
            request.AddParameter("api_key", ApiKey);
            request.AddParameter("email", subscriber.Email);
            request.AddParameter("properties", subscriber.ToPropertiesString());
            request.AddParameter("confirm_optin", "false");

            // Execute
            var response = Client.Execute<ListMemberApiResponse>(request);
            return response.Data;
        }

        public ListMemberApiResponse Unsubscribe(string email)
        {
            // Init request
            var request = new RestRequest(LIST_MEMBER_URL + "exclude", Method.POST);

            // Add param
            request.AddUrlSegment("LIST_ID", ListToken);
            request.AddParameter("api_key", ApiKey);
            request.AddParameter("email", email);

            // Execute
            var response = Client.Execute<ListMemberApiResponse>(request);
            return response.Data;
        }

        public ListMemberApiResponse UsersInList(string[] emails)
        {
            // Init request
            var request = new RestRequest("api/v1/list/{LIST_ID}/members", Method.GET);
            request.AddUrlSegment("LIST_ID", ListToken);
            request.AddParameter("api_key", ApiKey);
            string emailStr = string.Empty;
            foreach (string email in emails)
                emailStr += email;
            request.AddParameter("email", emailStr);

            // Execute
            var response = Client.Execute<ListMemberApiResponse>(request);
            return response.Data;
        }

        public ListInfoApiResponse GetListInfo()
        {
            // Init request
            var request = new RestRequest("api/v1/list/{LIST_ID}", Method.GET);
            request.AddUrlSegment("LIST_ID", ListToken);
            request.AddParameter("api_key", ApiKey);
            // Execute
            var response = Client.Execute<ListInfoApiResponse>(request);
            return response.Data;
        }

        #endregion
    }
}