namespace Husa.Extensions.Common.Tests
{
    using System;
    using Husa.Extensions.Common.Classes;
    using Husa.Extensions.Common.Tests.Providers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class ControllerExtensionsTests
    {
        private readonly Mock<HttpContext> httpContextMock = new();
        private readonly TestController sut;

        public ControllerExtensionsTests()
        {
            var controllerContext = new ControllerContext()
            {
                HttpContext = this.httpContextMock.Object,
            };

            this.sut = new TestController()
            {
                ControllerContext = controllerContext,
            };
        }

        [Fact]
        public void ToActionResult_CommandResult_ErrorResponse_HasErrors()
        {
            // Arrange
            var errors = new string[] { "error1", "error2" };
            var commandResult = CommandResult<string>.Error(errors);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            var value = Assert.IsAssignableFrom<string[]>(objectResult.Value);
            Assert.Equal(errors, value);
        }

        [Fact]
        public void ToActionResult_CommandResult_ErrorResponse_Message()
        {
            // Arrange
            var errorMessage = "error message";
            var commandResult = CommandResult<string>.Error(errorMessage);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            var value = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(errorMessage, value);
        }

        [Fact]
        public void ToActionResult_CommandResult_SuccessResponse_Message()
        {
            // Arrange
            var message = "Success message";
            var commandResult = CommandResult<string>.Success(message);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(message, value);
        }

        [Fact]
        public void ToActionResult_CommandResult_SuccessResponse_HasResults()
        {
            // Arrange
            var messages = new string[] { "Success1", "Success2" };
            var commandResult = CommandResult<string>.Success(messages);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<string[]>(objectResult.Value);
            Assert.Equal(messages, value);
        }

        [Fact]
        public void ToActionResult_CommandSingleResult_SuccessMessage()
        {
            // Arrange
            var message = "Success message";
            var commandResult = CommandSingleResult<string, string>.Success(message);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(message, value);
        }

        [Fact]
        public void ToActionResult_CommandSingleResult_SuccessResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var commandResult = CommandSingleResult<Guid, string>.Success(id);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<Guid>(objectResult.Value);
            Assert.Equal(id, value);
        }

        [Fact]
        public void ToActionResult_CommandSingleResult_ErrorResponse_HasErrors()
        {
            // Arrange
            var errors = new string[] { "error1", "error2" };
            var commandResult = CommandSingleResult<string, string>.Error(errors);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            var value = Assert.IsAssignableFrom<string[]>(objectResult.Value);
            Assert.Equal(errors, value);
        }

        [Fact]
        public void ToActionResult_CommandSingleResult_ErrorResponse_Message()
        {
            // Arrange
            var errorMessage = "error message";
            var commandResult = CommandSingleResult<string, string>.Error(errorMessage);

            // Act
            var result = this.sut.ToActionResult(commandResult);

            // Assert
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            var value = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(errorMessage, value);
        }
    }
}
