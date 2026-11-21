using DSW1_T1_SalazarHerrera_JosueSantiago.Models;

namespace DSW1_T1_SalazarHerrera_JosueSantiago.Repositories
{
    public interface ILibroRepository
    {
        IEnumerable<Libro> ListarLibros();

        Libro? ObtenerLibroPorId(int id);

        bool RegistrarLibro(Libro libro);

        bool ActualizarLibro(Libro libro);

        bool EliminarLibro(int id);

        IEnumerable<Libro> ListarLibrosPorAutor(string autor);
    }
}
