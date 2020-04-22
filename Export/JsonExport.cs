using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Export
{
    public class JsonExport : IExport
    {
        public string ContentType => "application/json";
        public string Extenstion => ".json";

        public MemoryStream Convert<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var buffer = Encoding.UTF8.GetBytes(json);
            return new MemoryStream(buffer);
        }
    }
}