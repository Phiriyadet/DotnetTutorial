using Domain;

namespace Core;

public interface IProvinceRepository : IBaseRepository<Province>
{
    Task<IEnumerable<Province>> GetAllAsync();
    public Task<Province> GetByIdWithPointOfInterestAsync(Guid id);
}