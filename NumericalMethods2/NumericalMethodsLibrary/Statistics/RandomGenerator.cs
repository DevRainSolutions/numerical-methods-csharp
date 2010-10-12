#region Using directives

using System;

#endregion

namespace NumericalMethods.Statistics
{
    /// A random generator that can be seeded and can also generate gaussian distributed random values
    /// @author edgar.sanchez@logicstudio.net
    public class RandomGenerator
    {
        private bool _haveNextNextGaussian;
        private double _nextNextGaussian;

        private Random _random;

        public RandomGenerator()
        {
            this._random = new Random();
            this._haveNextNextGaussian = false;

        }

        public RandomGenerator(int seed)
        {
            this._random = new Random(seed);
            this._haveNextNextGaussian = false;
        }

        public int Seed
        {
            set
            {
                this._haveNextNextGaussian = false;
                this._random = new Random(value);
            }
        }

        public double NextDouble()
        {
            return this._random.NextDouble();
        }

        /// <summary>
        /// Based on the Java Random.NextGaussian() method
        /// </summary>
        /// <returns>the next gaussian distributed random value</returns>
        public double NextGaussian()
        {
            lock (_random)
            {
                if (_haveNextNextGaussian)
                {
                    _haveNextNextGaussian = false;
                    return _nextNextGaussian;
                }
                else
                {
                    double v1, v2, s;
                    do
                    {
                        v1 = 2 * _random.NextDouble() - 1;   // between -1.0 and 1.0
                        v2 = 2 * _random.NextDouble() - 1;   // between -1.0 and 1.0
                        s = v1 * v1 + v2 * v2;
                    } while (s >= 1 || s == 0);
                    double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                    _nextNextGaussian = v2 * multiplier;
                    _haveNextNextGaussian = true;
                    return v1 * multiplier;
                }
            }
        }
    }
}
