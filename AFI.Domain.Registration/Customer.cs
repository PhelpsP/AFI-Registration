using AFI.Domain.Registration.Interfaces;
using System.Text.RegularExpressions;

namespace AFI.Domain.Registration
{
    public class Customer : ICustomer
    {
        public static Customer CreateNew(
            string firstName,
            string surname,
            string policyReferenceNumber,
            DateTime? dateOfBirth,
            string? email)
        {
            return new Customer(
                firstName,
                surname,
                policyReferenceNumber,
                dateOfBirth,
                email);
        }

        public static Customer CreateExisting(
            int id,
            string firstName,
            string surname,
            string policyReferenceNumber,
            DateTime? dateOfBirth,
            string? email)
        {
            return new Customer(id,  firstName, surname, policyReferenceNumber, dateOfBirth, email);
        }

        private Customer(
            int id,
            string firstName,
            string surname,
            string policyReferenceNumber,
            DateTime? dateOfBirth,
            string? email)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"Id is invalid, must be greater than 0.");
            }
         
            CustomerId = id;
            FirstName = firstName;
            Surname = surname;
            PolicyReferenceNumber = policyReferenceNumber;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        private Customer(
            string firstName,
            string surname,
            string policyReferenceNumber,
            DateTime? dateOfBirth,
            string? email)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 3 || firstName.Length > 50)
            {
                throw new ArgumentException("First name must be between 3 and 50 characters long.");
            }

            if (string.IsNullOrWhiteSpace(surname) || surname.Length < 3 || surname.Length > 50)
            {
                throw new ArgumentException("Surname must be between 3 and 50 characters long.");
            }

            if (!Regex.IsMatch(policyReferenceNumber, RegexPatterns.PolicyReferencePattern))
            {
                throw new ArgumentException(
                    $"Policy Reference Number: {policyReferenceNumber} does not conform to the expected format.");
            }

            if (dateOfBirth == null && string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("Date of Birth or Email required.");
            }

            DateTime eighteenYearsFromToday = DateTime.Today.AddYears(-18);
            if (dateOfBirth != null && !IsOver18(dateOfBirth))
            {
                throw new ArgumentException("Policy holder must be 18 or older.");
            }

            if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, RegexPatterns.EmailAddressPattern))
            {
                throw new ArgumentException("Email does not match the expected email format.");
            }

            FirstName = firstName;
            Surname = surname;
            PolicyReferenceNumber = policyReferenceNumber;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        public int CustomerId { get; private set; }

        public string FirstName { get; }
        
        public string Surname { get; }
        
        public string PolicyReferenceNumber { get; }
        
        public DateTime? DateOfBirth { get; }
        
        public string? Email { get; }

        private bool IsOver18(DateTime? dateToCheck)
        {
            DateTime eighteenYearsAgoToday = DateTime.Today.AddYears(-18);

            return dateToCheck <= eighteenYearsAgoToday;
        }
    }
}
