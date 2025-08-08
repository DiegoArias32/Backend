using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IBaseController<T> where T : class
    {
        Task<ActionResult<IEnumerable<T>>> GetAllAsync();
        Task<ActionResult<T>> GetByIdAsync(int id);
        Task<ActionResult<T>> CreateAsync(T entity);
        Task<ActionResult<T>> UpdateAsync(T entity);
        Task<ActionResult<bool>> DeleteAsync(int id);
        Task<ActionResult<bool>> DeleteLogicalAsync(int id);
    }
}