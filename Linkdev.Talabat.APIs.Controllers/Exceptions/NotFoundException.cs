namespace Linkdev.Talabat.APIs.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
        : base("NotFound") {
            
        }
    }
}
