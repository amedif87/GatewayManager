namespace GatewayManager.Domain.IServices.Shared
{
    public interface ICrudService<T1> where T1 : class, new()
    {
        Task<T1?> Create(T1 entity);
        Task Update(T1 entity);
        Task Delete(long idEntity);
        Task<T1> GetById(long idEntity);      
        Task<IEnumerable<T1>> GetAll();       
    }
}
