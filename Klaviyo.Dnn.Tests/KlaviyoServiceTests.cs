using DotNetNuke.Entities.Users;
using Klaviyo.Dnn.Models;
using NUnit.Framework;
using System;
using System.Configuration;

namespace Klaviyo.Dnn.Tests
{
    [TestFixture]
    public class KlaviyoServiceTests
    {
        private string _privateApiKey;
        private string _listToken;
        private UserInfo _user;
        private KlaviyoListApi _api;

        [SetUp]
        public void Setup()
        {
            _user = new UserInfo();
            _user.Profile = new UserProfile()
            {
                FirstName = ConfigurationManager.AppSettings["first"],
                LastName = ConfigurationManager.AppSettings["last"]
            };
            _user.Email = ConfigurationManager.AppSettings["email"];
            _user.FirstName = ConfigurationManager.AppSettings["first"];
            _user.LastName = ConfigurationManager.AppSettings["last"];

            _privateApiKey = ConfigurationManager.AppSettings["private_api_key"];
            _listToken = ConfigurationManager.AppSettings["list_token"];

            _api = new KlaviyoListApi(_privateApiKey, _listToken);
        }

        [Test]
        public void Cannot_Init_Listapi_With_Empty_ListToken_Or_PrivateKey()
        {
            // Invalid ctor calls
            Assert.That(() => new KlaviyoListApi(string.Empty, _listToken), Throws.Exception.TypeOf<ArgumentException>());
            Assert.That(() => new KlaviyoListApi(_privateApiKey, string.Empty), Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void Can_Add_User_To_List()
        {
            var sub = new Subscriber()
            {
                First = _user.FirstName,
                Last = _user.LastName,
                Email = _user.Email
            };

            var apiResponse = _api.Subscribe(sub);

            Assert.NotNull(apiResponse);
        }

        [Test]
        public void Can_Get_List_Info()
        {
            var apiResponse = _api.GetListInfo();

            Assert.NotNull(apiResponse);
        }

        [Test]
        public void Can_Check_If_User_In_List()
        {
            var apiResponse = _api.UsersInList(new string[] { ConfigurationManager.AppSettings["email"] });

            Assert.NotNull(apiResponse);
        }
    }
}