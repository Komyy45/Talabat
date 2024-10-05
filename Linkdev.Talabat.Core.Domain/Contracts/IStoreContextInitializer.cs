namespace Linkdev.Talabat.Core.Domain.Contracts
{
    public interface IStoreContextInitializer
    {
        Task InitalizeAsync();

        Task SeedAsync();
    }
}
