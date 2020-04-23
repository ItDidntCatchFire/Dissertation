namespace Repository
{
    public interface IUserRepository<T, U> : IRepository<T, U> 
        where T : class 
    {
        
    }
}