namespace Livraria.Util.ExtensionMethods
{
    public static class IntExtensions
    {
        public static int TryToInt(this object value)
        {
            int.TryParse(value.ToString(), out int valorConvertido);

            return valorConvertido;
        }
    }
}
