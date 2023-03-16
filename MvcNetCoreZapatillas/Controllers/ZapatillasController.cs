using Microsoft.AspNetCore.Mvc;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Home()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillas();
            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int idproducto)
        {
            Zapatilla zapatilla = await this.repo.GetZapatilla(idproducto);
            return View(zapatilla);
        }

        public async Task<IActionResult> _PaginacionZapatillas(int? posicion, int idproducto)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            List<ImagenZapatilla> imagenes =
                    await this.repo.GetImagenesZapatillas(posicion.Value, idproducto);
            ViewData["REGISTROS"] = this.repo.GetNumeroZapartillas(idproducto);
            ViewData["IDPRODUCTO"] = idproducto;
            return PartialView("_PaginacionZapatillas", imagenes);
        }


        public async Task<IActionResult> _DetalleImagen(int? posicion, int idproducto)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            List<ImagenZapatilla> imagenes =
                    await this.repo.GetImagenesZapatillas(posicion.Value, idproducto);
            ViewData["REGISTROS"] = this.repo.GetNumeroZapartillas(idproducto);
            ViewData["IDPRODUCTO"] = idproducto;
            return PartialView("_PaginacionZapatillas", imagenes);
        }
    }
}
