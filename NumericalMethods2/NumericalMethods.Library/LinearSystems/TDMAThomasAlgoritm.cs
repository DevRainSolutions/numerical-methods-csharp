namespace NumericalMethods.LinearSystems
{
    public class TDMAThomas
    {
        /* 
            http://en.wikipedia.org/wiki/Tridiagonal_matrix_algorithm
            In numerical linear algebra, the tridiagonal matrix algorithm (TDMA), 
            also known as the Thomas algorithm (named after Llewellyn Thomas), is 
            a simplified form of Gaussian elimination that can be used to solve 
            tridiagonal systems of equations. A tridiagonal system for n unknowns may be written as
            where  and . In matrix form, this system is written as
         
            a(i)*x(i-1)+b(i)*x(i)+c(i)*x(i+1)=d(i)
            where a(1)=0 c(n)=0
         
            [  b1  c1  0   0  ... ... ...    ]*[x1]=[d1]
            [  a2  b2  c2  0  ... ... ...    ] [x2] [d2]
            [  0   a3  b3  c3 ... ... ...    ] ...   ...
            [  ... ... ... ... ... ... ...   ] ...   ...
            [  ... ... ... ... an-1 bn-1 cn-1] ...   ...
            [  ... ... ... ... ... an  bn    ] [xn] [dn]
         
            For such systems, the solution can be obtained in O(n) operations instead 
            of O(n3) required by Gaussian elimination. A first sweep eliminates the ai's,
            and then an (abbreviated) backward substitution produces the solution.
        
            Example of such matrices commonly arise from the discretization of 1D
            Poisson equation (e.g., the 1D diffusion problem) and natural cubic spline interpolation.

            The first step consists of modifying the coefficients as follows, denoting 
            the new modified coefficients with primes:
         
            c'(i)=  {c(1)/b(1)                  ;i=1
                    {c(i)/(b(i)-c'(i-1)*a(i);   i=2,3,...,n
            and:

            d'(i)=  {d(1)/b(1)
                    {[d(i)-d'(i-1)*a(i)]/[b(i)-c'(i-1)*a(i)];  i=2,3,...,n

            This is the forward sweep. The solution is then obtained by back substitution:
          
            x(n)=d'(n)
            x(i)=d'(i)-c'(i)*x(i+1)            ;i=n-1,n-2,...1
         
         
         */
        private double[] result;
        readonly int _n;
        private double[] _a;
        private double[] _b;
        private double[] _c;
        public TDMAThomas(double[] a, double[] b, double[] c, int n)
        {
            _n = n;
            result = new double[_n];
            _a = a;
            _b = b;
            _c = c;
        }
        /* Fills solution into x. Warning: will modify c and d! */
        public double[] TridiagonalSolve(double[] d)
        {
            /* Modify the coefficients. */
            _c[0] /= _b[0];	/* Division by zero risk. */
            d[0] /= _b[0];	/* Division by zero would imply a singular matrix. */
            for (var i = 1; i < _n; i++)
            {
                var id = 1.0 / (_b[i] - _c[i - 1] * _a[i]);  /* Division by zero risk. */
                _c[i] *= id;	                         /* Last value calculated is redundant. */
                d[i] = (d[i] - d[i - 1] * _a[i]) * id;
            }
            /* back substitute. */
            result[_n - 1] = d[_n - 1];
            for (var i = _n - 2; i >= 0; i--)
                result[i] = d[i] - _c[i] * result[i + 1];

            return result;
        }
    }
}
