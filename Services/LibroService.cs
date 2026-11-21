using DSW1_T1_SalazarHerrera_JosueSantiago.Models;
using DSW1_T1_SalazarHerrera_JosueSantiago.Repositories;

namespace DSW1_T1_SalazarHerrera_JosueSantiago.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            this.libroRepository = libroRepository;
        }

        public IEnumerable<Libro> ListarLibros()
        {
            return libroRepository.ListarLibros();
        }

        public Libro? ObtenerLibroPorId(int id)
        {
            if (id <= 0)
                return null;

            return libroRepository.ObtenerLibroPorId(id);
        }

        public bool RegistrarLibro(Libro libro)
        {
            if (string.IsNullOrWhiteSpace(libro.NombreLibro) ||
                string.IsNullOrWhiteSpace(libro.Autor) ||
                string.IsNullOrWhiteSpace(libro.NombreEditorial) ||
                string.IsNullOrWhiteSpace(libro.ISDN))
            {
                return false;
            }

            return libroRepository.RegistrarLibro(libro);
        }

        public bool ActualizarLibro(Libro libro)
        {
            if (libro.Id <= 0 ||
                string.IsNullOrWhiteSpace(libro.NombreLibro) ||
                string.IsNullOrWhiteSpace(libro.Autor) ||
                string.IsNullOrWhiteSpace(libro.NombreEditorial) ||
                string.IsNullOrWhiteSpace(libro.ISDN))
            {
                return false;
            }

            return libroRepository.ActualizarLibro(libro);
        }

        public bool EliminarLibro(int id)
        {
            if (id <= 0)
                return false;

            return libroRepository.EliminarLibro(id);
        }

        public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor))
                return libroRepository.ListarLibros();

            return libroRepository.ListarLibrosPorAutor(autor.Trim());
        }
    }
}