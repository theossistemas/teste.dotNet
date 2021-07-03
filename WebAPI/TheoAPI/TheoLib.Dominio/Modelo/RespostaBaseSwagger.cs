namespace TheoLib.Dominio.Modelo
{
    public class RespostaBaseSwagger<T>: RespostaBase
    {
        public RespostaBaseSwagger(T t) => Conteudo = t;

        public T Conteudo { get; set; }
    }
}
