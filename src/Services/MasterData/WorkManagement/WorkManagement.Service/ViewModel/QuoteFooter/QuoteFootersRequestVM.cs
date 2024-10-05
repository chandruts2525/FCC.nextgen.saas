namespace WorkManagement.Service.ViewModel.QuoteFooter
{
    public class QuoteFootersRequestVM
    {
        public QuoteFootersRequestVM() { }

        public int? QuoteFootersID { get; set; }
        public string? Name { get; set; }
        public string? Companies { get; set; }
        public string? Modules { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
