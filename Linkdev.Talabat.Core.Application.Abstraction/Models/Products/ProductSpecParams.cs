namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Products
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        private string? search;

        public string? Search { get => search; set => search = value?.ToUpper(); }

        private int pageSize = 5;

        private const int maximumPageSize = 10; 

        public int PageSize 
        {
            get
            {
                return pageSize;
            }
            set
            {
                
                pageSize = value > maximumPageSize ? maximumPageSize : value;
            }
        }

        public int PageIndex { get; set; } = 1;  

    }
}
