using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace RazorPage
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
        public static bool checkPermission(this ISession session, string role)
        {
            string rolecheck = null;
            rolecheck = SessionHelper.GetObjectFromJson<string>(session, "role");
            if (rolecheck != null)
            {
                if (role.Equals(rolecheck)) return true;
                else return false;
            }
            else { return false; }

        }
    }
}
