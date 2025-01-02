using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Commands;

public class CreateChildrenCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string HomeAddress { get; set; }
}

public class CreateChildrenCommandHandler : IRequestHandler<CreateChildrenCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateChildrenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateChildrenCommand request, CancellationToken cancellationToken)
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

        await _unitOfWork.Childrens.AddAsync(children);
        await _unitOfWork.SaveChangesAsync();

        return children.Id;
    }
}


