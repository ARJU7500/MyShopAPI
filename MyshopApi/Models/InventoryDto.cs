namespace MyshopApi.Models
{
    public class InventoryDto
    {
        #region properties
        public int InventoryId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public int StockAvaible { get; set; }
        public int ReOrderStock { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        #endregion
    }
}
