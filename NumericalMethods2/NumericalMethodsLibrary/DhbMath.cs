#region Using directives

using System;

#endregion

namespace NumericalMethods.DhbFunctionEvaluation
{
    /// This class implements additional mathematical functions
    /// and determines the parameters of the floating point representation.
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public sealed class DhbMath
    {
        /// Typical meaningful precision for numerical calculations.
        private static double _defaultNumericalPrecision = 0;
        /// Typical meaningful small number for numerical calculations.
        private static double _smallNumber = 0;
        /// Radix used by floating-point numbers.
        private static int _radix = 0;
        /// Largest positive value which, when added to 1.0, yields 0.
        private static double _machinePrecision = 0;
        /// Largest positive value which, when subtracted to 1.0, yields 0.
        private static double _negativeMachinePrecision = 0;
        /// Smallest number different from zero.
        private static double _smallestNumber = 0;
        /// Largest possible number
        private static double _largestNumber = 0;
        /// Largest argument for the exponential
        private static double _largestExponentialArgument = 0;
        /// Values used to compute human readable scales.
        private static double[] _scales = { 1.25, 2, 2.5, 4, 5, 7.5, 8, 10 };
        private static double[] _semiIntegerScales = { 2, 2.5, 4, 5, 7.5, 8, 10 };
        private static double[] _integerScales = { 2, 4, 5, 8, 10 };

        private static void ComputeLargestNumber()
        {
            double floatingRadix = Radix;
            double fullMantissaNumber = 1.0d -
                                floatingRadix * NegativeMachinePrecision;
            while (!Double.IsInfinity(fullMantissaNumber))
            {
                _largestNumber = fullMantissaNumber;
                fullMantissaNumber *= floatingRadix;
            }
        }

        private static void ComputeMachinePrecision()
        {
            double floatingRadix = Radix;
            double inverseRadix = 1.0d / floatingRadix;
            _machinePrecision = 1.0d;
            double tmp = 1.0d + _machinePrecision;
            while (tmp - 1.0d != 0.0d)
            {
                _machinePrecision *= inverseRadix;
                tmp = 1.0d + _machinePrecision;
            }
        }

        private static void ComputeNegativeMachinePrecision()
        {
            double floatingRadix = Radix;
            double inverseRadix = 1.0d / floatingRadix;
            _negativeMachinePrecision = 1.0d;
            double tmp = 1.0d - _negativeMachinePrecision;
            while (tmp - 1.0d != 0.0d)
            {
                _negativeMachinePrecision *= inverseRadix;
                tmp = 1.0d - _negativeMachinePrecision;
            }
        }

        private static void ComputeRadix()
        {
            double a = 1.0d;
            double tmp1, tmp2;
            do
            {
                a += a;
                tmp1 = a + 1.0d;
                tmp2 = tmp1 - a;
            } while (tmp2 - 1.0d != 0.0d);
            double b = 1.0d;
            while (_radix == 0)
            {
                b += b;
                tmp1 = a + b;
                _radix = (int)(tmp1 - a);
            }
        }

        private static void ComputeSmallestNumber()
        {
            double floatingRadix = Radix;
            double inverseRadix = 1.0d / floatingRadix;
            double fullMantissaNumber = 1.0d - floatingRadix * NegativeMachinePrecision;
            while (fullMantissaNumber != 0.0d)
            {
                _smallestNumber = fullMantissaNumber;
                fullMantissaNumber *= inverseRadix;
            }
        }

        public static double DefaultNumericalPrecision
        {
            get
            {
                if (_defaultNumericalPrecision == 0)
                    _defaultNumericalPrecision = Math.Sqrt(MachinePrecision);
                return _defaultNumericalPrecision;
            }
        }

        /// @return boolean	true if the difference between a and b is
        /// less than the default numerical precision
        /// @param a double
        /// @param b double
        public static bool Equal(double a, double b)
        {
            return Equal(a, b, DefaultNumericalPrecision);
        }

        /// @return boolean	true if the relative difference between a and b
        /// is less than precision
        /// @param a double
        /// @param b double
        /// @param precision double
        public static bool Equal(double a, double b, double precision)
        {
            double norm = Math.Max(Math.Abs(a), Math.Abs(b));
            return norm < precision || Math.Abs(a - b) < precision * norm;
        }

        public static double LargestExponentialArgument
        {
            get
            {
                if (_largestExponentialArgument == 0)
                    _largestExponentialArgument = Math.Log(LargestNumber);
                return _largestExponentialArgument;
            }
        }

        /// (c) Copyrights Didier BESSET, 1999, all rights reserved.
        public static double LargestNumber
        {
            get
            {
//                if (_largestNumber == 0)
//                    ComputeLargestNumber();
//                return _largestNumber;
                // replaced by edgar.sanchez@logicstudio.net
                return double.MaxValue;
            }
        }

        public static double MachinePrecision
        {
            get
            {
                if (_machinePrecision == 0)
                    ComputeMachinePrecision();
                return _machinePrecision;
            }
        }

        public static double NegativeMachinePrecision
        {
            get
            {
                if (_negativeMachinePrecision == 0)
                    ComputeNegativeMachinePrecision();
                return _negativeMachinePrecision;
            }
        }

        public static int Radix
        {
            get
            {
                if (_radix == 0)
                    ComputeRadix();
                return _radix;
            }
        }

        public static double SmallestNumber
        {
            get
            {
//                if (_smallestNumber == 0)
//                    ComputeSmallestNumber();
//                return _smallestNumber;
                // replaced by edgar.sanchez@logicstudio.net
                return double.Epsilon;
            }
        }

// Moved to DhbWindowsTesting.FormDhbTesting to reduce footprint of this assembly
//        public static void PrintParameters(System.IO.TextWriter printStream)
//        {
//            printStream.WriteLine("Floating-point machine parameters");
//            printStream.WriteLine("---------------------------------");
//            printStream.WriteLine(" ");
//            printStream.WriteLine("radix = " + Radix);
//            printStream.WriteLine("Machine precision = "
//                                                    + MachinePrecision);
//            printStream.WriteLine("Negative machine precision = "
//                                            + NegativeMachinePrecision);
//            printStream.WriteLine("Smallest number = " + SmallestNumber);
//            printStream.WriteLine("Largest number = " + LargestNumber);
//        }

        public static void Reset()
        {
            _defaultNumericalPrecision = 0;
            _smallNumber = 0;
            _radix = 0;
            _machinePrecision = 0;
            _negativeMachinePrecision = 0;
            _smallestNumber = 0;
            _largestNumber = 0;
        }

        /// This method returns the specified value rounded to
        /// the nearest integer multiple of the specified scale.
        ///
        /// @param value number to be rounded
        /// @param scale defining the rounding scale
        /// @return rounded value
        public static double RoundTo(double value, double scale)
        {
            return Math.Round(value / scale) * scale;
        }

        /// Round the specified value upward to the next scale value.
        /// @param the value to be rounded.
        /// @param a fag specified whether integer scale are used, otherwise double scale is used.
        /// @return a number rounded upward to the next scale value.
        public static double RoundToScale(double value, bool integerValued)
        {
            double[] scaleValues;
            int orderOfMagnitude = (int)Math.Floor(Math.Log(value) / Math.Log(10.0));
            if (integerValued)
            {
                orderOfMagnitude = Math.Max(1, orderOfMagnitude);
                if (orderOfMagnitude == 1)
                    scaleValues = _integerScales;
                else if (orderOfMagnitude == 2)
                    scaleValues = _semiIntegerScales;
                else
                    scaleValues = _scales;
            }
            else
                scaleValues = _scales;
            double exponent = Math.Pow(10.0, orderOfMagnitude);
            double rValue = value / exponent;
//            for (int n = 0; n < scaleValues.Length; n++)
//            {
//                if (rValue <= scaleValues[n])
//                    return scaleValues[n] * exponent;
//            }
            // replaced by edgar.sanchez@logicstudio.net
            foreach (double scaleValue in scaleValues)
            {
                if (rValue <= scaleValue)
                    return scaleValue * exponent;
            }
            return exponent;	// Should never reach here
        }

        /// (c) Copyrights Didier BESSET, 1999, all rights reserved.
        /// @return double
        public static double SmallNumber
        {
            get
            {
                if (_smallNumber == 0)
                    _smallNumber = Math.Sqrt(SmallestNumber);
                return _smallNumber;
            }
        }
    }
}
