namespace CHILDINFOMANAGER.Web.Repositories;

public interface IUnitOfWork : IDisposable
{
    IChildrenRepository Childrens { get; }
    Task<int> SaveChangesAsync();
}
