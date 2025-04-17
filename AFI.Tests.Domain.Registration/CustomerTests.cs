using AFI.Domain.Registration;
using System.ComponentModel.DataAnnotations;

namespace AFI.Tests.Domain.Registration
{
    [TestClass]
    public sealed class CustomerTests
    {
        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenFirstNameIsToShort()
        {
            string firstName = "Di";
            string surname = "Valid";
            string referenceNumber = "XX-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenFirstNameIsToLong()
        {
            string firstName = new string('a', 51);
            string surname = "Valid";
            string referenceNumber = "XX-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenSurnameIsToShort()
        {
            string firstName = "Valid";
            string surname = "No";
            string referenceNumber = "XX-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenSurnameIsToLong()
        {
            string firstName = "Valid";
            string surname = new string('b', 51);
            string referenceNumber = "XX-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenPolicyReferenceNumberIsInvalidFormat_Not2AlphaCharacters()
        {
            string firstName = new string('a', 15);
            string surname = "Valid";
            string referenceNumber = "X2-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenPolicyReferenceNumberIsInvalidFormat_1AlphaCharacter()
        {
            string firstName = new string('a', 15);
            string surname = "Valid";
            string referenceNumber = "X-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenPolicyReferenceNumberIsInvalidFormat_LowerCaseAlphaCharacters()
        {
            string firstName = new string('a', 15);
            string surname = "Valid";
            string referenceNumber = "ab-999999";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenPolicyReferenceNumberIsInvalidFormat_Not6NumericCharacters()
        {
            string firstName = new string('a', 15);
            string surname = "Valid";
            string referenceNumber = "AB-12345";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenPolicyReferenceNumberIsInvalidFormat_Last6WithAlphaChar()
        {
            string firstName = new string('a', 15);
            string surname = "Valid";
            string referenceNumber = "CC-999Z99";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_WhenPolicyReferenceNumberIsInvalidFormat_NoHyphen()
        {
            string firstName = new string('a', 15);
            string surname = "Valid";
            string referenceNumber = "AB123456";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, referenceNumber, dateOfBirth, string.Empty));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentNullException_WhereNoEmailOrDateOfBirthProvided()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = null;
            string email = string.Empty;

            Assert.ThrowsException<ArgumentNullException>(
                () => Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_ForSeventeenYearOldToday()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = DateTime.Today.AddDays(1).AddYears(-18); 
            string email = string.Empty;

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateNew_ReturnsCustomer_ForEighteenYearOldToday()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-18);
            string email = string.Empty;

            var actual = Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email);

            Assert.IsNotNull(actual);
            Assert.AreEqual(firstName, actual.FirstName);
            Assert.AreEqual(surname, actual.Surname);
            Assert.AreEqual(policyRefNum, actual.PolicyReferenceNumber);
            Assert.AreEqual(dateOfBirth, actual.DateOfBirth);
            Assert.AreEqual(email, actual.Email);
        }

        [TestMethod]
        public void CreateNew_ReturnsCustomer_ForEighteenYearOldTodayWithTimeIncluded()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = DateTime.Now.AddYears(-18);
            DateTime? expectedDateOfBirth = dateOfBirth?.Date;
            string email = string.Empty;

            var actual = Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email);

            Assert.IsNotNull(actual);
            Assert.AreEqual(firstName, actual.FirstName);
            Assert.AreEqual(surname, actual.Surname);
            Assert.AreEqual(policyRefNum, actual.PolicyReferenceNumber);
            Assert.AreEqual(expectedDateOfBirth, actual.DateOfBirth);
            Assert.AreEqual(email, actual.Email);
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_ForLessThan4AlphaNumbericCharsBeforeEmailAtSymbol()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = null;
            string email = "tes@test.co.uk";

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_ForNoAtSymbol()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = null;
            string email = "testtest.co.uk";

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_ForLessThan2AlphaNumbericCharsAfterEmailAtSymbol()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = null;
            string email = "test@t.co.uk";

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateNew_ThrowsArgumentException_ForEndingNotComOrCoDotyUk()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = null;
            string email = "tes@test.net";

            Assert.ThrowsException<ArgumentException>(
                () => Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateNew_ReturnsCustomer_ForValidInputs()
        {
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);
            string email = string.Empty;

            var actual = Customer.CreateNew(firstName, surname, policyRefNum, dateOfBirth, email);

            Assert.IsNotNull(actual);
            Assert.AreEqual(firstName, actual.FirstName);
            Assert.AreEqual(surname, actual.Surname);
            Assert.AreEqual(policyRefNum, actual.PolicyReferenceNumber);
            Assert.AreEqual(dateOfBirth, actual.DateOfBirth);
            Assert.AreEqual(email, actual.Email);
        }

        [TestMethod]
        public void CreateExisting_ThrowsArgumentException_ForInvalidId()
        {
            int id = 0;
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);
            string email = string.Empty;

            Assert.ThrowsException<ArgumentException>(() => Customer.CreateExisting(id, firstName, surname, policyRefNum, dateOfBirth, email));
        }

        [TestMethod]
        public void CreateExisting_ReturnsExistingCustomer()
        {
            int id = 101;
            string firstName = "Barry";
            string surname = "Valid";
            string policyRefNum = "AA-123456";
            DateTime? dateOfBirth = DateTime.Today.AddYears(-19);
            string email = string.Empty;

            var actual = Customer.CreateExisting(id, firstName, surname, policyRefNum, dateOfBirth, email);

            Assert.IsNotNull(actual);
            Assert.AreEqual(id, actual.CustomerId);
            Assert.AreEqual(firstName, actual.FirstName);
            Assert.AreEqual(surname, actual.Surname);
            Assert.AreEqual(policyRefNum, actual.PolicyReferenceNumber);
            Assert.AreEqual(dateOfBirth, actual.DateOfBirth);
            Assert.AreEqual(email, actual.Email);
        }
    }
}
