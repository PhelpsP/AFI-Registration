namespace AFI.Application
{
    public interface ICustomerRegistrationService
    {
        Task<int> RegisterCustomerAsync(CustomerDetailsDto customerDetails);
    }
}
