using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest
{
    public class CarsControllerUnitTest
    {
        [Fact]
        public async void Create_ReturnsOkResult_WhenRequestIsValid()
        {
            //Arrenge - tanımlamaların yapıldığı alan
            var mediatrMock = new Mock<IMediator>();
            var createCarCommand = new CreateCarCommand("toyota","Corolla",5000);
            MessageResponse response = new("Araç başarıyla kayıt edildi");
            CancellationToken cancellationToken = new();

            mediatrMock.Setup(x=> x.Send(createCarCommand,cancellationToken))
                .ReturnsAsync(response);

            CarsController carsController = new(mediatrMock.Object);
            //Act - eylem parçası işlem yapılır
            var result = await carsController.Create(createCarCommand, cancellationToken);

            //Assert - kontrolün yapıldığı yerdir
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            Assert.Equal(response, returnValue);
            mediatrMock.Verify(x => x.Send(createCarCommand, cancellationToken), Times.Once);
        }
    }
}