using System.Text.Json;

namespace Application.Common.Helpers
{
    public static class SerializeHelper
    {
        public static string SerializeObjectToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
