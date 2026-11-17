using Microsoft.AspNetCore.Mvc;
using TrabajoFinal.Services;

namespace TrabajoFinal.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosAsync();
                return Ok(productos); 
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var producto = await _productoService.ObtenerPorIdAsync(id);

                if (producto == null)
                {
                    return NotFound(new { mensaje = $"El producto con ID {id} no fue encontrado." }); // Devuelve 404
                }

                return Ok(producto); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno en el servidor.");
            }
        }
    }
}