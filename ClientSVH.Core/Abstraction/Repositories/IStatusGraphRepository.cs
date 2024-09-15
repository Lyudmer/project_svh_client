namespace ClientSVH.Core.Abstaction.Repositories
{
    public interface IStatusGraphRepository
    {
        Task Add(int oldst, int newst, string maskbit);
        Task Delete(int oldst, int newst);
        Task UpdateNew(int oldst, int newst, string maskbit);
        Task UpdateOld(int oldst, int newst, string maskbit);
    }
}