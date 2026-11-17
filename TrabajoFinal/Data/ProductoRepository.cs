using Microsoft.Data.SqlClient;
using TrabajoFinal.Models;
using System.Data;

namespace TrabajoFinal.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            var productos = new List<Producto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Nombre, Precio, Stock FROM Productos";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(new Producto
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Stock = Convert.ToInt32(reader["Stock"])
                            });
                        }
                    }
                }
            }

            return productos;
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            Producto? producto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Nombre, Precio, Stock FROM Productos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    // Usamos parámetros para evitar SQL Injection (Actividad 13 del TP)
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            producto = new Producto
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Stock = Convert.ToInt32(reader["Stock"])
                            };
                        }
                    }
                }
            }

            return producto;
        }
    }
}