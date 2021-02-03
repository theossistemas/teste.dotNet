using System.ComponentModel;

namespace JeffersonMello.Livraria.Common
{
    public class Enuns
    {
        #region Public Enums

        public enum NivelUsuario
        {
            [Description("Público")]
            Publico = 0,

            [Description("Administrador")]
            Administrador = 1,
        }

        #endregion Public Enums
    }
}