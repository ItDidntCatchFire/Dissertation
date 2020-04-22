using Newtonsoft.Json;

namespace Import
{
    public class JsonImport : IImport
    {
        public T Read<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}