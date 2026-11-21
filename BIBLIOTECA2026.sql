IF DB_ID(N'BDBIBLIOTECA2026') IS NULL
BEGIN
    CREATE DATABASE BDBIBLIOTECA2026;
END;
GO

USE BDBIBLIOTECA2026;
GO

IF OBJECT_ID(N'dbo.Libros', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Libros
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        NombreLibro VARCHAR(120) NOT NULL,
        Autor VARCHAR(100) NOT NULL,
        NombreEditorial VARCHAR(100) NOT NULL,
        ISDN VARCHAR(20) NOT NULL
    );
END;
GO

SELECT * FROM dbo.Libros;


CREATE OR ALTER PROCEDURE dbo.usp_ListarLibros
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    FROM dbo.Libros
    ORDER BY Id;
END;
GO

EXEC dbo.usp_ListarLibros;
GO


CREATE OR ALTER PROCEDURE dbo.usp_RegistrarLibro
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Libros
    (
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    )
    VALUES
    (
        @NombreLibro,
        @Autor,
        @NombreEditorial,
        @ISDN
    );
END;
GO

EXEC dbo.usp_RegistrarLibro
    @NombreLibro = 'Cien años de soledad',
    @Autor = 'Gabriel Garcia Marquez',
    @NombreEditorial = 'Sudamericana',
    @ISDN = '9780307474728';
GO

EXEC dbo.usp_ListarLibros;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ActualizarLibro
    @Id INT,
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Libros
    SET
        NombreLibro = @NombreLibro,
        Autor = @Autor,
        NombreEditorial = @NombreEditorial,
        ISDN = @ISDN
    WHERE Id = @Id;
END;
GO

EXEC dbo.usp_ListarLibros;

EXEC dbo.usp_ActualizarLibro
    @Id = 1,
    @NombreLibro = 'Cien años de soledad',
    @Autor = 'Gabriel Garcia Marquez',
    @NombreEditorial = 'Editorial Sudamericana',
    @ISDN = '9780307474728';
GO

EXEC dbo.usp_ListarLibros;
GO


CREATE OR ALTER PROCEDURE dbo.usp_EliminarLibro
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Libros
    WHERE Id = @Id;
END;
GO

EXEC dbo.usp_EliminarLibro
    @Id = 9999;
GO

EXEC dbo.usp_ListarLibros;
GO


CREATE OR ALTER PROCEDURE dbo.usp_ListarLibrosPorAutor
    @Autor VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    FROM dbo.Libros
    WHERE Autor LIKE '%' + @Autor + '%'
    ORDER BY Id;
END;
GO


EXEC dbo.usp_ListarLibrosPorAutor
    @Autor = 'Gabriel';
GO

EXEC dbo.usp_ListarLibrosPorAutor
    @Autor = 'Marquez';
GO


CREATE OR ALTER PROCEDURE dbo.usp_ObtenerLibroPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        NombreLibro,
        Autor,
        NombreEditorial,
        ISDN
    FROM dbo.Libros
    WHERE Id = @Id;
END;
GO

EXEC dbo.usp_ObtenerLibroPorId @Id = 1;
GO
