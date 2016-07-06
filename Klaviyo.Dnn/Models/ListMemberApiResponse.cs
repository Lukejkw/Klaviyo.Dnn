using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klaviyo.Dnn.Models
{
    public class ListMemberApiResponse
    {
        public Person person { get; set; }
        public List list { get; set; }
        public bool already_member { get; set; }
    }

    public class Person
    {
        public string @object { get; set; }
        public string id { get; set; }
        public string email { get; set; }
    }

    public class List
    {
        public string @object { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
}