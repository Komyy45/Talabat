namespace Linkdev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IStoreContextInitializer
    {
        Task InitalizeAsync();

        Task SeedAsync();
    }
}
