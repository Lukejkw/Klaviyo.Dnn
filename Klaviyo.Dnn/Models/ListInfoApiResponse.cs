using RestSharp.Deserializers;

namespace Klaviyo.Dnn.Models
{
    public class ListInfoApiResponse
    {
        [DeserializeAs(Name = "id")]
        public string ListToken { get; set; }

        [DeserializeAs(Name = "updated")]
        public string Updated { get; set; }

        [DeserializeAs(Name = "person_count")]
        public int PersonCount { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "created")]
        public string Created { get; set; }

        [DeserializeAs(Name = "@object")]
        public string Object { get; set; }

        [DeserializeAs(Name = "list_type")]
        public string ListType { get; set; }

        [DeserializeAs(Name = "folder")]
        public string FolderName { get; set; }
    }
}