namespace DynamicAPI.Business.Helper
{
    public class ObjectHelper
    {
        public ObjectHelper()
        {
        }
        public Dictionary<string, string> ConvertValuesToStrings(Dictionary<string, object> data)
        {
            var stringValues = new Dictionary<string, string>();

            foreach (var kvp in data)
            {
                stringValues[kvp.Key] = kvp.Value?.ToString() ?? string.Empty;
            }

            return stringValues;
        }
    }
}
