using Moq;
using CHILDINFOMANAGER.Web.Commands;
using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;

namespace CHILDINFOMANAGER.Web.Tests.Commands;

[TestFixture]
public class UpdateChildrenCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private UpdateChildrenCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateChildrenCommandHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldReturnUpdatedGuid()
    {
        // Arrange
        var command = new UpdateChildrenCommand
        {
            Id = Guid.NewGuid(),
            FirstName = "Zunayed",
            LastName = "Shahriar",
            DateOfBirth = DateTimeOffset.Now,
            PhoneNumber = "01344444444",
            HomeAddress = "123 Main St"
        };

        var existingChild = new Children
        {
            Id = command.Id,
            FirstName = "Zunayed",
            LastName = "Shahriar",
            DateOfBirth = DateTimeOffset.Now,
            PhoneNumber = "01344444444",
            HomeAddress = "123 Main St"
        };

        _unitOfWorkMock.Setup(uow => uow.Childrens.GetByIdAsync(command.Id)).ReturnsAsync(existingChild);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(command.Id));
        Assert.That(existingChild.FirstName, Is.EqualTo(command.FirstName));
        Assert.That(existingChild.LastName, Is.EqualTo(command.LastName));
        Assert.That(existingChild.DateOfBirth, Is.EqualTo(command.DateOfBirth));
        Assert.That(existingChild.PhoneNumber, Is.EqualTo(command.PhoneNumber));
        Assert.That(existingChild.HomeAddress, Is.EqualTo(command.HomeAddress));
        _unitOfWorkMock.Verify(uow => uow.Childrens.Update(existingChild), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void Handle_ChildNotFound_ShouldThrowException()
    {
        // Arrange
        var command = new UpdateChildrenCommand
        {
            Id = Guid.NewGuid(),
            FirstName = "Zunayed",
            LastName = "Shahriar",
            DateOfBirth = DateTimeOffset.Now,
            PhoneNumber = "01344444444",
            HomeAddress = "123 Main St"
        };

        _unitOfWorkMock.Setup(uow => uow.Childrens.GetByIdAsync(command.Id)).ReturnsAsync((Children)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Child not found"));
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
