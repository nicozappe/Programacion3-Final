using TrabajoFinal.Models;

namespace TrabajoFinal.Data
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
    }
}