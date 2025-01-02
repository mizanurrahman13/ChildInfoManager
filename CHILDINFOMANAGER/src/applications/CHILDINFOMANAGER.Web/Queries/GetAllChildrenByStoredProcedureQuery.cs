using CHILDINFOMANAGER.Web.Entities;
using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Queries;
public class GetAllChildrenByStoredProcedureQuery : IRequest<IEnumerable<Children>> { }

public class GetAllChildrenByStoredProcedureQueryHandler : IRequestHandler<GetAllChildrenByStoredProcedureQuery, IEnumerable<Children>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllChildrenByStoredProcedureQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Children>> Handle(GetAllChildrenByStoredProcedureQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Childrens.GetAllByStoredProcedureAsync();
    }
}
