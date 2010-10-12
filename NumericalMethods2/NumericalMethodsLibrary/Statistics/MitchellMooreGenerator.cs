#region Using directives

using System;

#endregion

namespace NumericalMethods.Statistics
{
    /// MitchellMoore random number generator
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public class MitchellMooreGenerator
    {
	    /// List of previously generated numbers
        private double[] _randoms;
	    /// Index of last generated number
        int _highIndex;
	    /// Index of number to add to last number
        int _lowIndex;

        /// Default constructor.
        public MitchellMooreGenerator() : this(55, 24)
        {
        }

        /// Constructor method.
        /// @param seeds double[]
        /// @param index int
        public MitchellMooreGenerator(double[] seeds, int index)
        {
            _highIndex = seeds.Length;
            _randoms = new double[_highIndex];
            Array.Copy(seeds, _randoms, --_highIndex);
            _lowIndex = index - 1;
        }

        /// Constructor method.
        /// @param indexH int	high index
        /// @param indexL int	low index
        public MitchellMooreGenerator(int indexH, int indexL)
        {
            Random generator = new Random();
            _randoms = new double[indexH];
            for (int i = 0; i < indexH; i++)
                _randoms[i] = generator.NextDouble();
            _highIndex = indexH - 1;
            _lowIndex = indexL - 1;
        }

        /// @return double	the next random number
        public double NextDouble()
        {
            double x = _randoms[_highIndex--] + _randoms[_lowIndex--];
            if (_highIndex < 0)
                _highIndex = _randoms.Length - 1;
            if (_lowIndex < 0)
                _lowIndex = _randoms.Length - 1;
            return (_randoms[_highIndex] = x < 1.0 ? x : x - 1);
        }

        /// @return long	returns a long integer between 0 and n-1
        /// @param n long
        public long NextInteger(long n)
        {
            return (long)(n * NextDouble());
        }
    }
}
