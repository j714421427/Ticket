using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ticket.Core
{
    public static class Extensions
    {
        public static string SerializeToQueryString(this object obj, string separator = "&", bool encode = false)
        {
            var result = new List<string>();

            foreach (var property in obj.GetType().GetProperties())
            {
                if (!property.CustomAttributes.Any(
                    a => a.AttributeType == typeof(JsonIgnoreAttribute)))
                {
                    var attr = property.GetCustomAttributes(false).FirstOrDefault(
                        a => a.GetType() == typeof(JsonPropertyAttribute))
                        as JsonPropertyAttribute;

                    result.Add(string.Format("{0}={1}",
                        attr == null ? property.Name : attr.PropertyName,
                        encode ? WebUtility.UrlEncode(property.GetValue(obj).ToString()) : property.GetValue(obj)));
                }
            }

            return string.Join(separator, result);
        }
    }
}
