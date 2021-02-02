using System.Reflection;

namespace JeffersonMello.Livraria.Common.Extension
{
    public static class ObjectExtension
    {
        #region Public Methods

        public static bool IsValid(this object ob)
        {
            bool valid = true;

            if (ob == null)
                return false;

            return valid;
        }

        public static bool IsValid(this PropertyInfo ob)
        {
            bool valid = true;

            if (ob == null)
                return false;

            return valid;
        }

        #endregion Public Methods
    }
}