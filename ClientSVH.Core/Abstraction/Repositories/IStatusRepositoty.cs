namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface IStatusRepositoty
    {
        Task Add(int Id, string StatusName, bool RunWf, bool MkRes);
        Task Delete(int Id);
        Task Update(int Id, string StatusName, bool RunWf, bool MkRes);
    }
}