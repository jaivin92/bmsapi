using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace bmslib
{
    public static class Extensions
    {
        public static bool IsEmpty(this Guid guid)
        {
            return (guid == Guid.Empty);
        }

        public static T GetModel<T>(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(str);
            }
            return default(T);
        }

        public static T GetModel<T>(this Dictionary<string, object> obj)
        {
            if (obj != null)
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(System.Text.Json.JsonSerializer.Serialize(obj));
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static bool IsNotNull<T>(this List<T> source)
        {
            return (source != null && source.Count > 0);
        }

        public static bool IsNull<T>(this List<T> source)
        {
            return (source == null || source.Count == 0);
        }

        public static string RemoveSpecialCharacters(this string str, string replace = "")
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in str.Trim())
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
                else if (!char.IsWhiteSpace(c) && !string.IsNullOrWhiteSpace(replace))
                {
                    sb.Append(replace);
                }
            }
            return sb.ToString();
        }

        public static string ToPascalCase(this string the_string)
        {
            TextInfo info = Thread.CurrentThread.CurrentCulture.TextInfo;
            return info.ToTitleCase(the_string.ToLower());
        }

        public static T GetResult<T>(this IEnumerable<dynamic> source)
        {
            if (source == null || source.Count() == 0)
            {
                return default(T);
            }
            var _ds = source.FirstOrDefault();
            foreach (var pair in _ds)
            {
                var vl = pair.Value;
                return System.Text.Json.JsonSerializer.Deserialize<T>(vl.ToString());
            }
            return default(T);
        }

        public static string ToSerialize(this object obj)
        {
            if (obj != null)
            {
                return System.Text.Json.JsonSerializer.Serialize(obj);
            }
            return null;
        }

        public static T ToDeserialize<T>(this string obj)
        {
            if (!string.IsNullOrWhiteSpace(obj))
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(obj);
            }
            return default(T);
        }

        public static bool IsNull(this string val)
        {
            return (string.IsNullOrWhiteSpace(val) || val == "null");
        }

        public static string MySqlDateString(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd");
        }

        public static string MySqlDateString(this DateOnly val)
        {
            return val.ToString("yyyy-MM-dd");
        }

        public static string SubstringLib(this string value, int maxLength, string addSuffixAfterSubstring = "")
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength) + addSuffixAfterSubstring;
        }

        public static string ToSEOUrl(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value == "/")
                return value;

            value = Regex.Replace(value, @"[^a-zA-Z0-9]+", "-");
            return value.Trim('-');
        }

    }
}
