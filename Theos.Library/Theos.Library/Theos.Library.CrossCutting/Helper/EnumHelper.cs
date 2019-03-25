using System;
using System.ComponentModel;

namespace Theos.Library.CrossCutting.Helper
{
    public static class EnumHelper<T> where T : struct, IConvertible
    {
        public static string GetEnumDescription(T enumerator)
        {
            var field = enumerator.GetType().GetField(enumerator.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return enumerator.ToString();
        }

        public static string GetEnumDescription(int value)
        {
            var enumerator = (T)System.Enum.ToObject(typeof(T), value);
            return GetEnumDescription(enumerator);
        }

        public static T GetValueFromDescription(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", "Description");
        }
    }
}