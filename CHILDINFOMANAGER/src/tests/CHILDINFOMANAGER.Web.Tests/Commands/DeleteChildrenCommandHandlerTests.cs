using Moq;
using CHILDINFOMANAGER.Web.Commands;
using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;

namespace CHILDINFOMANAGER.Web.Tests.Commands;

[TestFixture]
public class DeleteChildrenCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private DeleteChildrenCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteChildrenCommandHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldReturnDeletedGuid()
    {
        // Arrange
        var command = new DeleteChildrenCommand { Id = Guid.NewGuid() };
        var existingChild = new Children { Id = command.Id };

        _unitOfWorkMock.Setup(uow => uow.Childrens.GetByIdAsync(command.Id)).ReturnsAsync(existingChild);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command);

        // Assert
        Assert.That(result, Is.EqualTo(command.Id));
        _unitOfWorkMock.Verify(uow => uow.Childrens.Remove(existingChild), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    //[Test]
    //public async Task Handle_ChildNotFound_ShouldReturnEmptyGuid()
    //{
    //    // Arrange
    //    var command = new DeleteChildrenCommand { Id = Guid.NewGuid() };

    //    _unitOfWorkMock.Setup(uow => uow.Childrens.GetByIdAsync(command.Id)).ReturnsAsync((Children)null);

    //    // Act
    //    var result = await _handler.Handle(command);

    //    // Assert
    //    Assert.That(result, Is.EqualTo(command.Id));
    //    _unitOfWorkMock.Verify(uow => uow.Childrens.Remove(It.IsAny<Children>()), Times.Never);
    //    _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    //}
}
