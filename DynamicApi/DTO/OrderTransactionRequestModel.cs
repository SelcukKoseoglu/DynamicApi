namespace DynamicAPI.DTO
{
    public class OrderTransactionRequestModel
    {
        public Dictionary<string, object> OrderData { get; set; } 
        public List<Dictionary<string, object>> Products { get; set; }
    }
}
