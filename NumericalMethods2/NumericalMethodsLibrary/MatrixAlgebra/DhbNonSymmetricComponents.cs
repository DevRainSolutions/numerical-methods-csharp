#region Using directives

using System;

#endregion

namespace NumericalMethods.MatrixAlgebra
{
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class DhbNonSymmetricComponents : ApplicationException
    {

        /// DhbNonSymmetricComponents constructor comment.
        public DhbNonSymmetricComponents() : base()
        {
        }

        /// DhbNonSymmetricComponents constructor comment.
        /// @param s string
        public DhbNonSymmetricComponents(string s) : base(s)
        {
        }
    }
}
