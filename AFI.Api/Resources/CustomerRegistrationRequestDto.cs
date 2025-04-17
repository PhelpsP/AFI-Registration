namespace AFI.Api.Resources
{
    public class CustomerRegistrationRequestDto
    {
        public string PolicyholderFirstName { get; set; } = string.Empty;

        public string PolicyholderSurname { get; set; } = string.Empty;

        public string PolicyReferenceNumber { get; set; } = string.Empty;

        public DateTime? PolicyholderDOB { get; set; }

        public string? PolicyholderEmail { get; set; }
    }
}
