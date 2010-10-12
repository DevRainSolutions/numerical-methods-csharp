﻿namespace AP
{
    /********************************************************************
    Class defining a complex number with double precision.
    ********************************************************************/
    public struct Complex
    {
        public double x;
        public double y;

        public Complex(double _x)
        {
            x = _x;
            y = 0;
        }
        public Complex(double _x, double _y)
        {
            x = _x;
            y = _y;
        }
        public static implicit operator Complex(double _x)
        {
            return new Complex(_x);
        }
        public static bool operator ==(Complex lhs, Complex rhs)
        {
            return (lhs.x == rhs.x) & (lhs.y == rhs.y);
        }
        public static bool operator !=(Complex lhs, Complex rhs)
        {
            return (lhs.x != rhs.x) | (lhs.y != rhs.y);
        }
        public static Complex operator +(Complex lhs)
        {
            return lhs;
        }
        public static Complex operator -(Complex lhs)
        {
            return new Complex(-lhs.x, -lhs.y);
        }
        public static Complex operator +(Complex lhs, Complex rhs)
        {
            return new Complex(lhs.x + rhs.x, lhs.y + rhs.y);
        }
        public static Complex operator -(Complex lhs, Complex rhs)
        {
            return new Complex(lhs.x - rhs.x, lhs.y - rhs.y);
        }
        public static Complex operator *(Complex lhs, Complex rhs)
        {
            return new Complex(lhs.x * rhs.x - lhs.y * rhs.y, lhs.x * rhs.y + lhs.y * rhs.x);
        }
        public static Complex operator /(Complex lhs, Complex rhs)
        {
            Complex result;
            double e;
            double f;
            if (System.Math.Abs(rhs.y) < System.Math.Abs(rhs.x))
            {
                e = rhs.y / rhs.x;
                f = rhs.x + rhs.y * e;
                result.x = (lhs.x + lhs.y * e) / f;
                result.y = (lhs.y - lhs.x * e) / f;
            }
            else
            {
                e = rhs.x / rhs.y;
                f = rhs.y + rhs.x * e;
                result.x = (lhs.y + lhs.x * e) / f;
                result.y = (-lhs.x + lhs.y * e) / f;
            }
            return result;
        }
    }

    /********************************************************************
    AP math namespace
    ********************************************************************/
    public struct rcommstate
    {
        public int stage;
        public int[] ia;
        public bool[] ba;
        public double[] ra;
        public AP.Complex[] ca;
    };

    /********************************************************************
    AP math namespace
    ********************************************************************/
    public class Math
    {
        public static System.Random RndObject = new System.Random(System.DateTime.Now.Millisecond);

        public const double MachineEpsilon = 5E-16;
        public const double MaxRealNumber = 1E300;
        public const double MinRealNumber = 1E-300;

        public static double RandomReal()
        {
            double r = 0;
            lock (RndObject) { r = RndObject.NextDouble(); }
            return r;
        }
        public static int RandomInteger(int N)
        {
            int r = 0;
            lock (RndObject) { r = RndObject.Next(N); }
            return r;
        }
        public static double Sqr(double X)
        {
            return X * X;
        }
        public static double AbsComplex(Complex z)
        {
            double w;
            double xabs;
            double yabs;
            double v;

            xabs = System.Math.Abs(z.x);
            yabs = System.Math.Abs(z.y);
            w = xabs > yabs ? xabs : yabs;
            v = xabs < yabs ? xabs : yabs;
            if (v == 0)
                return w;
            else
            {
                double t = v / w;
                return w * System.Math.Sqrt(1 + t * t);
            }
        }
        public static Complex Conj(Complex z)
        {
            return new Complex(z.x, -z.y);
        }
        public static Complex CSqr(Complex z)
        {
            return new Complex(z.x * z.x - z.y * z.y, 2 * z.x * z.y);
        }

    }
}
