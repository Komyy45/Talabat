namespace Linkdev.Talabat.Core.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
        : base($"{name} with Id:{key} is not found!") {
            
        }
    }
}
