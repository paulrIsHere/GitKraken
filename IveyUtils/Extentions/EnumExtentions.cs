using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Ivey.Utils
{
    public static partial class Extentions
    {

        private static Dictionary<string, string> _enumTypeDescriptionCache = new Dictionary<string, string>();
        private static object _enumTypeDescriptionCacheLock = new object();

        public static string ToDescriptionString(this Enum value)
        {

            string cacheKey;
            string description;

            cacheKey = string.Format("{0}:{1}", value.GetType().FullName, value.ToString());
            description = string.Empty;

            if (_enumTypeDescriptionCache.ContainsKey(cacheKey) == false)
            {
                lock (_enumTypeDescriptionCacheLock)
                {
                    if (_enumTypeDescriptionCache.ContainsKey(cacheKey) == false)
                    {

                        MemberInfo[] memberInfo;
                        memberInfo = value.GetType().GetMember(value.ToString());

                        if (memberInfo != null && memberInfo.Length > 0)
                        {
                            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                            if (null != attributes && attributes.Length > 0)
                            {
                                description = ((DescriptionAttribute)attributes[0]).Description;
                            }
                        }
                    }
                    else
                    {
                        description = _enumTypeDescriptionCache[cacheKey];
                    }
                }
            }
            else
            {
                description = _enumTypeDescriptionCache[cacheKey];
            }

            return description;

        }
    }
}
