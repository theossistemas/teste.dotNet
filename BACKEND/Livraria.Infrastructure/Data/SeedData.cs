using Livraria.Domain;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(DbContext context)
    {
        if (!context.Set<Livro>().Any())
        {
            context.Set<Livro>().AddRange(
                new Livro { Titulo = "Dom Casmurro", Autor = "Machado de Assis", ISBN = "978-85-359-0756-2", AnoPublicacao = 1899, Quantidade = 50, Preco = 29.90m },
                new Livro { Titulo = "O Pequeno Príncipe", Autor = "Antoine de Saint-Exupéry", ISBN = "978-85-7827-062-8", AnoPublicacao = 1943, Quantidade = 120, Preco = 19.50m },
                new Livro { Titulo = "Capitães da Areia", Autor = "Jorge Amado", ISBN = "978-85-359-0756-2", AnoPublicacao = 1937, Quantidade = 75, Preco = 35.00m },
                new Livro { Titulo = "1984", Autor = "George Orwell", ISBN = "978-85-359-0756-2", AnoPublicacao = 1949, Quantidade = 90, Preco = 45.90m },
                new Livro { Titulo = "Orgulho e Preconceito", Autor = "Jane Austen", ISBN = "978-85-359-0756-2", AnoPublicacao = 1813, Quantidade = 65, Preco = 32.80m },
                new Livro { Titulo = "Cem Anos de Solidão", Autor = "Gabriel García Márquez", ISBN = "978-85-359-0756-2", AnoPublicacao = 1967, Quantidade = 80, Preco = 55.00m },
                new Livro { Titulo = "O Senhor dos Anéis", Autor = "J.R.R. Tolkien", ISBN = "978-85-359-0756-2", AnoPublicacao = 1954, Quantidade = 110, Preco = 89.90m },
                new Livro { Titulo = "Harry Potter e a Pedra Filosofal", Autor = "J.K. Rowling", ISBN = "978-85-359-0756-2", AnoPublicacao = 1997, Quantidade = 150, Preco = 49.90m },
                new Livro { Titulo = "O Apanhador no Campo de Centeio", Autor = "J.D. Salinger", ISBN = "978-85-359-0756-2", AnoPublicacao = 1951, Quantidade = 55, Preco = 38.50m },
                new Livro { Titulo = "Crime e Castigo", Autor = "Fiódor Dostoiévski", ISBN = "978-85-359-0756-2", AnoPublicacao = 1866, Quantidade = 40, Preco = 62.00m },
                new Livro { Titulo = "A Metamorfose", Autor = "Franz Kafka", ISBN = "978-85-359-0756-2", AnoPublicacao = 1915, Quantidade = 70, Preco = 25.00m },
                new Livro { Titulo = "O Alquimista", Autor = "Paulo Coelho", ISBN = "978-85-359-0756-2", AnoPublicacao = 1988, Quantidade = 100, Preco = 30.00m },
                new Livro { Titulo = "Memórias Póstumas de Brás Cubas", Autor = "Machado de Assis", ISBN = "978-85-359-0756-2", AnoPublicacao = 1881, Quantidade = 60, Preco = 40.50m },
                new Livro { Titulo = "O Sol é para Todos", Autor = "Harper Lee", ISBN = "978-85-359-0756-2", AnoPublicacao = 1960, Quantidade = 85, Preco = 42.00m },
                new Livro { Titulo = "Frankenstein", Autor = "Mary Shelley", ISBN = "978-85-359-0756-2", AnoPublicacao = 1818, Quantidade = 45, Preco = 31.90m },
                new Livro { Titulo = "Drácula", Autor = "Bram Stoker", ISBN = "978-85-359-0756-2", AnoPublicacao = 1897, Quantidade = 55, Preco = 39.90m },
                new Livro { Titulo = "O Grande Gatsby", Autor = "F. Scott Fitzgerald", ISBN = "978-85-359-0756-2", AnoPublicacao = 1925, Quantidade = 70, Preco = 36.50m },
                new Livro { Titulo = "As Crônicas de Nárnia", Autor = "C.S. Lewis", ISBN = "978-85-359-0756-2", AnoPublicacao = 1950, Quantidade = 95, Preco = 65.00m },
                new Livro { Titulo = "O Morro dos Ventos Uivantes", Autor = "Emily Brontë", ISBN = "978-85-359-0756-2", AnoPublicacao = 1847, Quantidade = 50, Preco = 44.00m },
                new Livro { Titulo = "A Revolução dos Bichos", Autor = "George Orwell", ISBN = "978-85-359-0756-2", AnoPublicacao = 1945, Quantidade = 80, Preco = 28.00m },
                new Livro { Titulo = "O Diário de Anne Frank", Autor = "Anne Frank", ISBN = "978-85-359-0756-2", AnoPublicacao = 1947, Quantidade = 130, Preco = 24.50m },
                new Livro { Titulo = "Viagem ao Centro da Terra", Autor = "Júlio Verne", ISBN = "978-85-359-0756-2", AnoPublicacao = 1864, Quantidade = 60, Preco = 33.00m },
                new Livro { Titulo = "Fahrenheit 451", Autor = "Ray Bradbury", ISBN = "978-85-359-0756-2", AnoPublicacao = 1953, Quantidade = 75, Preco = 37.00m },
                new Livro { Titulo = "Admirável Mundo Novo", Autor = "Aldous Huxley", ISBN = "978-85-359-0756-2", AnoPublicacao = 1932, Quantidade = 65, Preco = 41.50m },
                new Livro { Titulo = "O Conde de Monte Cristo", Autor = "Alexandre Dumas", ISBN = "978-85-359-0756-2", AnoPublicacao = 1844, Quantidade = 50, Preco = 58.00m }
            );
            context.SaveChanges();
        }
    }
}