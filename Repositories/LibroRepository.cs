using Microsoft.Data.SqlClient;
using System.Data;
using DSW1_T1_SalazarHerrera_JosueSantiago.Models;

namespace DSW1_T1_SalazarHerrera_JosueSantiago.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly string cadenaConexion;

        public LibroRepository(IConfiguration configuration)
        {
            cadenaConexion = configuration.GetConnectionString(
                "DefaultConnection")!;
        }

        private Libro MapearLibro(SqlDataReader reader)
        {
            return new Libro
            {
                Id = Convert.ToInt32(reader["Id"]),
                NombreLibro = reader["NombreLibro"].ToString()!,
                Autor = reader["Autor"].ToString()!,
                NombreEditorial = reader["NombreEditorial"].ToString()!,
                ISDN = reader["ISDN"].ToString()!
            };
        }

        public IEnumerable<Libro> ListarLibros()
        {
            List<Libro> libros = new List<Libro>();

            using (SqlConnection conexion =
                new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "usp_ListarLibros", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        libros.Add(MapearLibro(reader));
                    }
                }
            }

            return libros;
        }

        public Libro? ObtenerLibroPorId(int id)
        {
            Libro? libro = null;

            using (SqlConnection conexion =
                new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "usp_ObtenerLibroPorId", conexion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        libro = MapearLibro(reader);
                    }
                }
            }

            return libro;
        }

        public bool RegistrarLibro(Libro libro)
        {
            using (SqlConnection conexion =
                new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "usp_RegistrarLibro", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                AgregarParametrosLibro(cmd, libro);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public bool ActualizarLibro(Libro libro)
        {
            using (SqlConnection conexion =
                new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "usp_ActualizarLibro", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value =
                    libro.Id;

                AgregarParametrosLibro(cmd, libro);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public bool EliminarLibro(int id)
        {
            using (SqlConnection conexion =
                new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "usp_EliminarLibro", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                conexion.Open();
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
        {
            List<Libro> libros = new List<Libro>();

            using (SqlConnection conexion =
                new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "usp_ListarLibrosPorAutor", conexion);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Autor", SqlDbType.VarChar, 100)
                    .Value = autor;

                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        libros.Add(MapearLibro(reader));
                    }
                }
            }

            return libros;
        }

        private void AgregarParametrosLibro(
            SqlCommand cmd, Libro libro)
        {
            cmd.Parameters.Add("@NombreLibro",
                SqlDbType.VarChar, 120).Value = libro.NombreLibro;

            cmd.Parameters.Add("@Autor",
                SqlDbType.VarChar, 100).Value = libro.Autor;

            cmd.Parameters.Add("@NombreEditorial",
                SqlDbType.VarChar, 100).Value =
                libro.NombreEditorial;

            cmd.Parameters.Add("@ISDN",
                SqlDbType.VarChar, 20).Value = libro.ISDN;
        }
    }
}