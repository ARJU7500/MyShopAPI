namespace MyshopApi.Models
{
    public class CustomerMaster
    {
        #region properties
        public int CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? EmailId { get; set; }
        public string? Contact { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        #endregion
    }
}
