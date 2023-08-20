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
            return type.Name;
        }
    }
}
