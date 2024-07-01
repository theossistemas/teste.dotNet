namespace LivrariaJc.Domain.Input
{
    public class LivroFilterInput
    {
        public string Titulo { get; set; }
        public int NumeroPagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
        public int PaginaAtual { get; set; }
    }
}
