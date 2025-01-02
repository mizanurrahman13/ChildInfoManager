using CHILDINFOMANAGER.Web.Repositories;
using MediatR;

namespace CHILDINFOMANAGER.Web.Commands;

public class DeleteChildrenCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteChildrenCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteChildrenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteChildrenCommand command)
    {
        var child = await _unitOfWork.Childrens.GetByIdAsync(command.Id);

        if (child == null) return child.Id;

        _unitOfWork.Childrens.Remove(child);
        _unitOfWork.SaveChangesAsync();

        return child.Id;
    }
}


