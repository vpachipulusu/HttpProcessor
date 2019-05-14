using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Runpath.Common.HttpProcessor
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(JsonConvert.SerializeObject(value, Formatting.Indented), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object value, string mediaType)
            : base(JsonConvert.SerializeObject(value, Formatting.Indented), Encoding.UTF8, mediaType)
        {
        }
    }
}
