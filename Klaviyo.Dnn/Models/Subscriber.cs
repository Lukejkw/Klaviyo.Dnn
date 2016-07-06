namespace Klaviyo.Dnn.Models
{
    public class Subscriber
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }

        public string ToPropertiesString()
        {
            return $"{{ '$first_name\' : '{First}', '$last_name' : '{Last}' }}";
        }
    }
}