namespace Browsio.Amazon
{
    using System.Collections.Generic;

    internal class ParamComparer : IComparer<string>
    {
        #region IComparer<string> Members

        public int Compare(string p1, string p2)
        {
            return string.CompareOrdinal(p1, p2);
        }

        #endregion
    }
}