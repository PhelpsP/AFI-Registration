using AFI.Api.Controllers;
using AFI.Api.Models;
using AFI.Api.Resources;
using AFI.Domain.Registration.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace AFI.Tests.Api
{
    [TestClass]
    public sealed class CustomerRegistrationTests
    {
        [TestMethod]
        public void ConstructorThrowsArgumentNullExceptionForNullCustomerRegistrationService()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CustomerController(null));
        }

        [TestMethod]
        public async Task ValidPostRequestReturns200StatusCodeAndCustomerId()
        {
            var input = new Customer
            {
                PolicyholderFirstName = "First",
                PolicyholderSurname = "Last",
                PolicyReferenceNumber = "XX-999999",
                PolicyholderEmail = "test@test.co.uk"
            };

            var customerRegistrationServiceMock = GetExampleCustomerRegistrationServiceMock();

            var target = new CustomerController(customerRegistrationServiceMock);
            var result = await target.PostAsync(input);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);

            Assert.IsInstanceOfType(actual.Value, typeof(RegistrationResponse));
            var actualResponse = actual.Value as RegistrationResponse;

            Assert.IsNotNull(actualResponse);
            Assert.IsTrue(actualResponse.CustomerId > 0);
        }

        [TestMethod]
        public async Task Post_InvalidRequest_Returns400BadRequestResponse()
        {
            var customerRegistrationServiceMock = GetExampleCustomerRegistrationServiceMock();

            var target = new CustomerController(customerRegistrationServiceMock);
            var result = await target.PostAsync(null);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var actual = result as BadRequestObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(400, actual.StatusCode);
        }

        [TestMethod]
        public async Task Post_KnownException_Returns400BadRequest()
        {
            var input = new Customer
            {
                PolicyholderFirstName = "First",
                PolicyholderSurname = "Last",
                PolicyReferenceNumber = "XX-999999",
                PolicyholderEmail = "test@test.org"
            };

            var mockCustomerRegistrationService = new Mock<ICustomerRegistrationService>();
            mockCustomerRegistrationService.Setup(x => x.RegisterCustomerAsync()).ThrowsAsync(new ValidationException());

            var target = new CustomerController(mockCustomerRegistrationService.Object);
            var result = await target.PostAsync(input);


            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var actual = result as BadRequestObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(400, actual.StatusCode);
        }

        [TestMethod]
        public async Task Post_UnexpectedException_Returns500()
        {
            var input = new Customer
            {
                PolicyholderFirstName = "First",
                PolicyholderSurname = "Last",
                PolicyReferenceNumber = "XX-999999",
                PolicyholderEmail = "test@test.co.uk"
            };

            var mockCustomerRegistrationService = new Mock<ICustomerRegistrationService>();
            mockCustomerRegistrationService.Setup(x => x.RegisterCustomerAsync()).ThrowsAsync(new Exception());

            var target = new CustomerController(mockCustomerRegistrationService.Object);
            var result = await target.PostAsync(input);


            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var actual = result as ObjectResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(500, actual.StatusCode);
        }

        private ICustomerRegistrationService GetExampleCustomerRegistrationServiceMock()
        {
            var customerRegistrationServiceMock = new Mock<ICustomerRegistrationService>();
            customerRegistrationServiceMock.Setup(x => x.RegisterCustomerAsync()).ReturnsAsync(1);

            return customerRegistrationServiceMock.Object;
        }
    }
}
