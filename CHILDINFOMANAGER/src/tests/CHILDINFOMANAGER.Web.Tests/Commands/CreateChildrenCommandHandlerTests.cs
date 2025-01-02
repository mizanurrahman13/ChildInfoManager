using Moq;
using CHILDINFOMANAGER.Web.Commands;
using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;

namespace CHILDINFOMANAGER.Web.Tests.Commands;

[TestFixture]
public class CreateChildrenCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private CreateChildrenCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateChildrenCommandHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldReturnNewGuid()
    {
        // Arrange
        var command = new CreateChildrenCommand
        {
            FirstName = "Zunayed",
            LastName = "Shahriar",
            DateOfBirth = DateTimeOffset.Now,
            PhoneNumber = "01344444444",
            HomeAddress = "123 Main St"
        };

        _unitOfWorkMock.Setup(uow => uow.Childrens.AddAsync(It.IsAny<Children>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1); // Return an int for SaveChangesAsync

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.TypeOf<Guid>(), "The result should be of Guid type.");
        Assert.That(result, Is.Not.EqualTo(Guid.Empty), "The result should not be an empty GUID.");
        _unitOfWorkMock.Verify(uow => uow.Childrens.AddAsync(It.IsAny<Children>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void Handle_UnitOfWorkThrowsException_ShouldThrowException()
    {
        // Arrange
        var command = new CreateChildrenCommand
        {
            FirstName = "Zunayed",
            LastName = "Shahriar",
            DateOfBirth = DateTimeOffset.Now,
            PhoneNumber = "01344444444",
            HomeAddress = "123 Main St"
        };

        _unitOfWorkMock.Setup(uow => uow.Childrens.AddAsync(It.IsAny<Children>())).ThrowsAsync(new Exception());
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ThrowsAsync(new Exception());

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
    }
}
