using Xunit;
using Moq;
using CalculadoraDataAPI.Controllers;
using CalculadoraDataAPI.DTOs;
using CalculadoraDataAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using CalculadoraDataAPI.Services;

namespace CalculadoraDataAPI.Tests
{
    public class CalcularDataUnitTestController
    {
        private readonly Mock<IDataCalculationService> _dataCalculationServiceMock;
        private readonly CalcularDataController _controller;

        public CalcularDataUnitTestController()
        {
            // Mock do serviço de cálculo de data
            _dataCalculationServiceMock = new Mock<IDataCalculationService>();

            // Mock do serviço de conversão de calendário
            var conversorCalendarioMock = new Mock<ConversorCalendarioService>();

            // Inicializa o controlador com os serviços mockados
            _controller = new CalcularDataController(_dataCalculationServiceMock.Object, conversorCalendarioMock.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithValidData()
        {
            // Arrange
            var request = new DateRequestDto
            {
                Data = "14/03/2021 19:12",
                Operacao = "+",
                Valor = 3000
            };
            _dataCalculationServiceMock
                .Setup(service => service.CalculateDate(request))
                .Returns("16/03/2021 21:11");

            // Act
            var result = _controller.Get(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("16/03/2021 21:11", okResult.Value);
        }

        [Fact]
        public void Get_ReturnsBadRequest_WhenInvalidData()
        {
            // Arrange
            var request = new DateRequestDto
            {
                Data = "invalid date",
                Operacao = "+",
                Valor = 3000
            };
            _dataCalculationServiceMock
                .Setup(service => service.CalculateDate(request))
                .Throws(new ArgumentException("Dados inválidos!"));

            // Act
            var result = _controller.Get(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dados inválidos!", badRequestResult.Value);
        }

        [Fact]
        public void Get_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var request = new DateRequestDto
            {
                Data = "14/03/2021 19:12",
                Operacao = "+",
                Valor = 3000
            };
            _dataCalculationServiceMock
                .Setup(service => service.CalculateDate(request))
                .Throws(new Exception("Erro interno"));

            // Act
            var result = _controller.Get(request);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Ocorreu um erro na sua solicitação. Por favor, tente mais tarde!", internalServerErrorResult.Value);
        }
    }
}
