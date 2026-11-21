using Microsoft.AspNetCore.Mvc;
using DSW1_T1_SalazarHerrera_JosueSantiago.Models;
using DSW1_T1_SalazarHerrera_JosueSantiago.Services;

namespace DSW1_T1_SalazarHerrera_JosueSantiago.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService libroService;

        public LibroController(ILibroService libroService)
        {
            this.libroService = libroService;
        }

        [HttpGet]
        public IActionResult Index(string? autor)
        {
            IEnumerable<Libro> libros;

            if (string.IsNullOrWhiteSpace(autor))
            {
                libros = libroService.ListarLibros();
            }
            else
            {
                libros = libroService.ListarLibrosPorAutor(autor);
            }

            ViewBag.Autor = autor;

            return View(libros);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            bool registrado = libroService.RegistrarLibro(libro);

            if (registrado)
            {
                TempData["Mensaje"] = "Libro registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "No se pudo registrar el libro.");
            return View(libro);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Libro? libro = libroService.ObtenerLibroPorId(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            bool actualizado = libroService.ActualizarLibro(libro);

            if (actualizado)
            {
                TempData["Mensaje"] = "Libro actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "No se pudo actualizar el libro.");
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            Libro? libro = libroService.ObtenerLibroPorId(id);

            if (libro == null)
            {
                TempData["Error"] = "El libro no existe.";
                return RedirectToAction(nameof(Index));
            }

            bool eliminado = libroService.EliminarLibro(id);

            if (eliminado)
            {
                TempData["Mensaje"] = "Libro eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar el libro.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}