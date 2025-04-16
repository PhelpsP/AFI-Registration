namespace AFI.Domain.Registration.Interfaces
{
    public interface ICustomerRegistrationService
    {
        Task<int> RegisterCustomerAsync();
    }
}
