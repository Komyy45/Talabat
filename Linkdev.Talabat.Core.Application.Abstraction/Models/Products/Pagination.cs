namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Products
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public required IEnumerable<T> Data { get; set; }
    }
}
