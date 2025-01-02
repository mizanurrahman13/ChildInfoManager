using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Queries;

public class GetChildrenByIdQuery : IRequest<Children>
{
    public Guid Id { get; set; }
}

public class GetChildrenByIdQueryHandler : IRequestHandler<GetChildrenByIdQuery, Children>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetChildrenByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Children> Handle(GetChildrenByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Childrens.GetByIdAsync(request.Id);
    }
}
