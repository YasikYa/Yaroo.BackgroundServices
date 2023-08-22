namespace Yaroo.BackgroundServices.Utility
{
    internal class TypeNameHelper
    {
        public static string GetTypeName<T>()
        {
            return GetTypeName(typeof(T));
        }

        public static string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                string genericArguments = type.GetGenericArguments()
                                    .Select(x => GetTypeName(x))
                                    .Aggregate((x1, x2) => $"{x1}, {x2}");
                return $"{type.Name.Substring(0, type.Name.IndexOf("`"))}"
                     + $"<{genericArguments}>";
            }
            return type.Name;
        }
    }
}
