using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Tienda.Models;
using Tienda.Models.ViewModels;

namespace Tienda.Controllers
{
    public class HomeController : Controller
    {
        private readonly TiendaContext _dbcontext;

        public HomeController(TiendaContext context )
        {
            _dbcontext = context;
        }

        public IActionResult ResumenVenta()
        {
            DateTime FechaInicio = DateTime.Now;
            FechaInicio = FechaInicio.AddDays(-5);

            List<GraficaVentas> Lista = (from Venta in _dbcontext.Ventas where Venta.FechaVenta.Value.Date >= FechaInicio.Date group Venta by Venta.FechaVenta.Value.Date into grupo select new GraficaVentas { Fecha = grupo.Key.ToString("dd/MM/yyyy"), Cantidad = grupo.Count()} ).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult ResumenProducto()
        {
            List<GraficaProductos> Lista = (from DetalleVenta in _dbcontext.DetalleVentas
                                            join Producto in _dbcontext.Productos on DetalleVenta.ProductoId equals Producto.ProductoId
                                            group DetalleVenta by DetalleVenta.ProductoId into grupo
                                            orderby grupo.Count() descending
                                            select new GraficaProductos
                                            {
                                                Producto = _dbcontext.Productos.FirstOrDefault(p => p.ProductoId == grupo.Key) != null ? _dbcontext.Productos.FirstOrDefault(p => p.ProductoId == grupo.Key).NombreProducto : "",
                                                Cantidad = grupo.Count()
                                            }).Take(4).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }







        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}