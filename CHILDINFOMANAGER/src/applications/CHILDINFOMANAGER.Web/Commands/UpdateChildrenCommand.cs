using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Commands;

public class UpdateChildrenCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string HomeAddress { get; set; }
}

public class UpdateChildrenCommandHandler : IRequestHandler<UpdateChildrenCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateChildrenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateChildrenCommand request, CancellationToken cancellationToken)
    {
        var children = await _unitOfWork.Childrens.GetByIdAsync(request.Id);

        if (children == null)
        {
            throw new Exception("Child not found");
        }

        children.FirstName = request.FirstName;
        children.LastName = request.LastName;
        children.DateOfBirth = request.DateOfBirth;
        children.PhoneNumber = request.PhoneNumber;
        children.HomeAddress = request.HomeAddress;

        _unitOfWork.Childrens.Update(children);
        await _unitOfWork.SaveChangesAsync();

        return children.Id;
    }
}
