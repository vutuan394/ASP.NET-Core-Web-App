using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_QLNT.Functions
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Set<T>(string key, T value)
        {
            var jsonString = JsonSerializer.Serialize(value);
            _httpContextAccessor.HttpContext.Session.SetString(key, jsonString);
        }

        public T Get<T>(string key)
        {
            var jsonString = _httpContextAccessor.HttpContext.Session.GetString(key);
            return jsonString == null ? default(T) : JsonSerializer.Deserialize<T>(jsonString);
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }

        public void Clear()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }
    }
}