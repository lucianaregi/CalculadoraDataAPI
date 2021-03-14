using CalculadoraDataAPI.Controllers;
using CalculadoraDataAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace CalcularDataUnitTestController
{
    public class CalcularDataUnitTestController
    {
        private readonly ConversorCalendarioService _conversorCalendario;

        [Fact]
        public void TestFailedDate()
        {
            // arrange
            var controller = new CalcularDataController(_conversorCalendario);

            // act
            var data = controller.Get("01/03/2021", "+", 2000);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal("Dados inválidos!", result.Value);
        }

        [Fact]
        public void TestFailedValue()
        {
            // arrange
            var controller = new CalcularDataController(_conversorCalendario);

            // act
            var data = controller.Get("01/03/2021 23:00", "+", 0);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal("Dados inválidos!", result.Value);
        }

        [Fact]
        public void TestOkDate()
        {
            // arrange
            var mockService = new Mock<ConversorCalendarioService>();
                  
            var controller = new CalcularDataController(mockService.Object);
            // act
            var data = controller.Get("01/03/2021 23:01", "+", 4000);

            // Assert
            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal("04/03/2021 17:40", result.Value);
        }

        [Fact]
        public void TestsOkDate()
        {
            // arrange
            var mockService = new Mock<ConversorCalendarioService>();

            var controller = new CalcularDataController(mockService.Object);
            // act
            var data = controller.Get("01/12/2021 23:01", "+", 4000);

            // Assert
            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal("04/12/2021 17:40", result.Value);
        }

        [Fact]
        public void TestsOkDateMinus()
        {
            // arrange
            var mockService = new Mock<ConversorCalendarioService>();

            var controller = new CalcularDataController(mockService.Object);
            // act
            var data = controller.Get("01/12/2021 23:00", "-", 4000);

            // Assert
            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal("29/11/2021 4:19", result.Value);
        }
    }
}
