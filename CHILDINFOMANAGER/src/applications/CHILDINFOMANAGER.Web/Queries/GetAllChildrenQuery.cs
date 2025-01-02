using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Queries;

public class GetAllChildrenQuery : IRequest<IEnumerable<Children>> { }

public class GetAllChildrenQueryHandler : IRequestHandler<GetAllChildrenQuery, IEnumerable<Children>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllChildrenQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Children>> Handle(GetAllChildrenQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Childrens.GetAllAsync();
    }
}

