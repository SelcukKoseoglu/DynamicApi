namespace DynamicAPI.DTO
{
    public class ObjectsRequestModel
    {
        public string ObjectType { get; set; }
        public Dictionary<string, object> Data { get; set; }

    }
}
