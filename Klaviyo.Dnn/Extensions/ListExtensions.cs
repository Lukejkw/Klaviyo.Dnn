using DotNetNuke.Entities.Users;
using Klaviyo.Dnn.Models;

namespace Klaviyo.Dnn.Extensions
{
    public static class ListExtensions
    {
        public static bool AddToKlaviyoList(this UserInfo user, string privateApiKey, string listToken)
        {
            var service = new KlaviyoListApi(privateApiKey, listToken);
            ListMemberApiResponse response = service.Subscribe(new Subscriber()
            {
                First = user.FirstName,
                Last = user.LastName,
                Email = user.Email
            });
            return response != null;
        }

        public static bool IsInKlaviyoList(this UserInfo user, string privateApiKey, string listToken)
        {
            var service = new KlaviyoListApi(privateApiKey, listToken);
            var response = service.UsersInList(new string[] { user.Email });
            return response != null;
        }
    }
}