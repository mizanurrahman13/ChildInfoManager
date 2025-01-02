using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Commands;
public class CreateChildrenByStoredProcedureCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string HomeAddress { get; set; }
}

public class CreateChildrenByStoredProcedureCommandHandler : IRequestHandler<CreateChildrenByStoredProcedureCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateChildrenByStoredProcedureCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateChildrenByStoredProcedureCommand request, CancellationToken cancellationToken)
    {
        var children = new Children
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            HomeAddress = request.HomeAddress
        };

        await _unitOfWork.Childrens.AddByStoredProcedureAsync(children);
        await _unitOfWork.SaveChangesAsync();

        return children.Id;
    }
}
