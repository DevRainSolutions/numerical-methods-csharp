using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NumericalMethods;
using NumericalMethods.DifferentialEquations;
using NumericalMethods.Approximation;
using NumericalMethods.Integration;
using NumericalMethods.NonLinearEquations;
using NumericalMethods.SystemLinearEqualizations;
using NumericalMethods.Interpolation;
using NumericalMethods.MatrixAlgebra;
using NumericalMethods.Optimizing;
using NumericalMethods.Statistics;
//using MathParser;
using NumericalMethods_Silverlight;
using SilverlightMathParser;
namespace NumericalMethods_Silverlight.Code
{
    public class CalculateMethods
    {
        public int precision = 4;
        private string result = "";
        private static string TestFunction = "";
        public string GetSolution()
        {
            return result;
        }

        public void ExecuteMethod(
            string nameMethod,
            double param1,
            double param2,
            double param3,
            double param4,
            string testFunction,
            int rangeArray,
            double[] LinSysMasA,
            double[,]LinSysMatrixB,
            double[] massX,
            double[] massF,
            double[] massW,
            double pointInterpolation,
            double[,]  MatrixAlgebraA,
            double pointPercentile, 
            double pointGenerator)
        {
           TestFunction = testFunction;
            switch (nameMethod)
            {

                //*** Approximate decision of equalization f(x)=0 ***
                case "Bisection Method":
                    {

                        Bisection bisect = new Bisection(new FunctionOne(TestFunBisection), param1, param2, param3);
                        result = "\n Result of the program: \n" +
                            "    x= " + string.Format("{0:f" + precision + "}", bisect.Result);
                    }
                    break;
                case "Chord Method":
                    {
                        Сhord chord = new Сhord(new FunctionOne(TestFunNewton), new FunctionOne(TestFunNewton2), param1, param2, param3, param4);
                        result = "\n Result of the program: \n" +
                            "   x= " + string.Format("{0:f" + precision + "}", chord.GetSolution());
                    }
                    break;
                case "Iteration Method":
                    {
                        IterationMethod itermet = new IterationMethod(new FunctionOne(TestFunIteration), param1, param2, param3, param4);
                        result = "\n Result of the program: \n" +
                            "   x= " + string.Format("{0:f" + precision + "}", itermet.GetSolution());
                    }
                    break;
                case "Newton Method":
                    {
                        Newton newton = new Newton(new FunctionOne(TestFunNewton), new FunctionOne(TestFunNewton2), param1, param2, param3, param4);
                        result = "\n Result of the program: \n" +
                            "   x= " + string.Format("{0:f" + precision + "}", newton.GetSolution());
                    }
                    break;

                //*** Differential Equations ***
                case "Euler Simple":
                    {
                        EulerSimple eulersimpl = new EulerSimple(new Function(TestFunDifferEquations), param1, param2, param3, Convert.ToInt32(param4));
                        var result_eulersimpl = eulersimpl.Result;
                        for (int j = 0; j < Convert.ToInt32(param4); j++)
                        {
                            result = result + string.Format("{0:f" + precision + "}", result_eulersimpl[0, j])
                                + " : "
                             + string.Format("{0:f" + precision + "}", result_eulersimpl[1, j]) + "\n";

                        }
                    }
                    break;
                case "Euler Modified":
                    {
                        EulerModified eulerModif = new EulerModified(new Function(TestFunDifferEquations), param1, param2, param3, Convert.ToInt32(param4));
                        var result_eulerModif = eulerModif.Result;
                        for (int j = 0; j < Convert.ToInt32(param4); j++)
                        {
                            result = result + string.Format("{0:f" + precision + "}", result_eulerModif[0, j])
                                + " : " + string.Format("{0:f" + precision + "}", result_eulerModif[1, j]) + "\n";
                           // result = result + "fun=\n";
                           // result = result +Convert.ToString( TestFunDifferEquations);
                        }
                    }
                    break;
                case "Euler Corrected":
                    {
                        EulerCorrected eulerCorrect = new EulerCorrected(new Function(TestFunDifferEquations), param1, param2, param3, Convert.ToInt32(param4));
                        var result_eulerCorrect = eulerCorrect.Result;
                        for (int j = 0; j < Convert.ToInt32(param4); j++)
                        {
                            result = result + string.Format("{0:f" + precision + "}", result_eulerCorrect[0, j])
                                + " : " + string.Format("{0:f" + precision + "}", result_eulerCorrect[1, j]) + "\n";
                        }
                    }
                    break;
                case "Runge-Kutta4":
                    {
                        RungeKutta4 rungeKutta4 = new RungeKutta4(new Function(TestFunDifferEquations), param1, param2, param3, Convert.ToInt32(param4));
                        var result_rungeKutta4 = rungeKutta4.Result;
                        for (int j = 0; j < Convert.ToInt32(param4); j++)
                        {
                            result = result + string.Format("{0:f" + precision + "}", result_rungeKutta4[0, j])
                                + " : " + string.Format("{0:f" + precision + "}", result_rungeKutta4[1, j]) + "\n";
                        }

                    }
                    break;

                //*** Integration ***
                case "Chebishev":
                    Chebishev chebish = new Chebishev(new FunctionOne(TestFunInteger), param1, param2, Convert.ToInt32(param3));
                    var result_chebish = chebish.GetSolution();

                    for (int j = 0; j <= Convert.ToInt32(param3); j++)
                        result = result + "\n   h = " + string.Format("{0:f" + precision + "}", result_chebish[1, j]) + "   \t   integral = "
                            + string.Format("{0:f" + precision + "}", result_chebish[0, j]);
                    break;
                case "Simpson":
                    Simpson simps = new Simpson(new FunctionOne(TestFunInteger), param1, param2, Convert.ToInt32(param3));
                    var result_simps = simps.GetSolution();
                    for (int j = 0; j <= Convert.ToInt32(param3); j++)
                        result = result + "\n   h = " + string.Format("{0:f" + precision + "}", result_simps[1, j]) +
                             " \t   integral = " + string.Format("{0:f" + precision + "}", result_simps[0, j]);
                    break;
                case "Simpson2":
                    Simpson2 simps2 = new Simpson2(new FunctionOne(TestFunInteger), param1, param2, Convert.ToInt32(param3));
                    var result_simps2 = simps2.GetSolution();
                    result = "\n\n  integral = " + string.Format("{0:f" + precision + "}", result_simps2);
                    break;
                case "Trapezium":
                    Trapezium trapez = new Trapezium(new FunctionOne(TestFunInteger), param1, param2, Convert.ToInt32(param3));
                    var result_trapez = trapez.GetSolution();
                    for (int j = 0; j <= Convert.ToInt32(param3); j++)
                        result = result + "\n   h = " + string.Format("{0:f" + precision + "}", result_trapez[1, j])
                            + " \t   integral = " + string.Format("{0:f" + precision + "}", result_trapez[0, j]);
                    break;

                //*** Non Linear equalization ***
                case "Half Division":
                    HalfDivision halfdiv = new HalfDivision(new FunctionOne(TestFunNonLinearEquations), param1, param2, param3);
                    result = "\n    X = " + string.Format("{0:f" + precision + "}", halfdiv.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", halfdiv.GetSolution()[1, 0]);
                    break;
                case "Hord Metod":
                    HordMetod hormet = new HordMetod(new FunctionOne(TestFunNonLinearEquations), param1, param2,Convert.ToInt32( param3));
                    result = "\n    X = " + string.Format("{0:f" + precision + "}", hormet.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", hormet.GetSolution()[1, 0]);
                    break;

                case "Newton Metod":
                    NewtonMethod newt = new NewtonMethod(new FunctionOne(TestFunNonLinearEquations), param1, param2);
                    result = "\n     X = " + string.Format("{0:f" + precision + "}", newt.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", newt.GetSolution()[1, 0]);
                    break;
                case "Secant Metod":
                    Secant sec = new Secant(new FunctionOne(TestFunNonLinearEquations), param1, param2);
                    result = "\n     X = " + string.Format("{0:f" + precision + "}", sec.GetSolution()[0, 0])
                        + "       Iterations = " + string.Format("{0:f" + precision + "}", sec.GetSolution()[1, 0]);
                    break;

                default:
                    result = "";
                    break;

                //*** Linear Systems ***
                case "Gaus":
                    double[,] LinSysMatrix;
                    LinSysMatrix = new double[100, 100];
                    for (int l = 0; l < rangeArray; l++)
                    {
                        for (int j = 0; j < rangeArray; j++)
                        {
                            LinSysMatrix[l, j] = LinSysMatrixB[l, j];
                        }
                        LinSysMatrix[l, rangeArray] = LinSysMasA[l];
                    }
                    Gaus gaus = new Gaus(4, LinSysMatrix);
                    var result_gaus = gaus.GetSolution();
                    result = "";
                    for (int j = 0; j < result_gaus.Length; j++)
                        result = result + "\n         X" + j + "  =  "
                            + string.Format("{0:f" + precision + "}", result_gaus[j]) + ";";
                    break;
                case "Zeidel":
                    Zeidel zeidel = new Zeidel(4, LinSysMatrixB, LinSysMasA);
                    var result_zeidel = zeidel.GetSolution();
                    result = "";
                    for (int j = 0; j < result_zeidel.Length; j++)
                        result = result + "\n         X" + j + "  =  "
                            + string.Format("{0:f" + precision + "}", result_zeidel[j]) + ";";
                    break;
                //*** Interpolation ***
                case "Lagrange Interpolator":
 
                    LagrangeInterpolator lagran = new LagrangeInterpolator(massX, massF, 6, pointInterpolation);
                    result = "\n    A value interpolation polynomial is in the point of interpolation.";
                    result = result + "\n\n    P = " + string.Format("{0:f" + precision + "}", lagran.GetSolution());
                    break;
                case "Newton Interpolator":

                    NewtonInterpolator newinterpol = new NewtonInterpolator(massX, massF, 6, pointInterpolation);
                   result= "\n    A value interpolation polynomial is in the point of interpolation.";
                  result =result + "\n\n    P = " + string.Format("{0:f" + precision + "}", newinterpol.GetSolution());
                    break;
                case "Neville Interpolator":
                    
                    NevilleInterpolator newill = new NevilleInterpolator(massX, massF, 6, pointInterpolation);
                    result = "\n    A value interpolation polynomial is in the point of interpolation.";
                    result = result + "\n\n    P = " + string.Format("{0:f" + precision + "}", newill.GetSolution());
                    break;
                case "Spline Interpolator":
                    SplineInterpolator spline = new SplineInterpolator(massX, massF, 6, pointInterpolation);
                    result = "\n    A value interpolation polynomial is in the point of interpolation.";
                    result = result + "\n\n    P = " + string.Format("{0:f" + precision + "}", spline.GetSolution());
                    break;
                case "Barycentric Interpolator":

                    BarycentricInterpolation barycen = new BarycentricInterpolation(massX, massF, massW, 6, pointInterpolation);
                    result = "\n    A value interpolation polynomial is in the point of interpolation.";
                    result = result + "\n\n    P = " + string.Format("{0:f" + precision + "}", barycen.GetSolution());
                    break;
                //*** Matrix Algebra ***
                case "Matrix Determinant":
                    MatrixDeterminant matrdet = new MatrixDeterminant();
                    result = " \n\n\n  Determinant =  " + string.Format("{0:f" + precision + "}", matrdet.MatrixDet(MatrixAlgebraA, rangeArray)) + ";\n";
                    break;
                case "RMatrix LU":
                  
                    MatrixLU matrlu = new MatrixLU(MatrixAlgebraA, rangeArray, rangeArray);
                    var result_matrlu = matrlu.GetSolution();
                    var result_matrlu2 = matrlu.GetSolution2();
                    result = "A:\n";
                    for (int ii = 0; ii < rangeArray; ii++)
                    {
                        for (int j = 0; j < rangeArray; j++)
                            result = result + "  \t " + string.Format("{0:f" + precision + "}", result_matrlu[ii, j]);
                        result = result + " \n";
                    }
                    result = result + "\nL:\n";
                    for (int ii = 0; ii < rangeArray; ii++)
                        result = result + "      " + string.Format("{0:f" + precision + "}", result_matrlu2[ii]);
                    result = result + " \n ";
                    break;
               case "Matrix Inverse LU":
                    RMatrixLuInverse matrluinv = new RMatrixLuInverse();

                    MatrixLU matrlu2 = new MatrixLU(MatrixAlgebraA, rangeArray, rangeArray);
                    if (matrluinv.rmatrixluinverse(MatrixAlgebraA, rangeArray, matrlu2.GetSolution2()) == true)
                    {
                        result = "\n             An inverse matrix exists \n\n ";
                        var result_matrluinv = matrluinv.GetSolution();
                        for (int ii = 0; ii < rangeArray; ii++)
                        {
                            for (int j = 0; j < rangeArray; j++)
                            {
                                result = result + "    \t" + string.Format("{0:f" + precision + "}", result_matrluinv[ii, j]);
                            }
                            result = result + "\n\n";
                        }
                    }
                    else
                        result = "\n             An inverse matrix does not exist";

                    break;
               //*** Optimizing***
               case "Brentopt":
                    Brentopt brent = new Brentopt(new FunctionOne(TestFunOptimizing), param1, param2, param3);
                    result = "\n    Point of the found minimum :";
                    result = result + "\n\n    XMin = " + string.Format("{0:f" + precision + "}", brent.GetSolution());
                    result = result + "\n\n     A value of function is in the found minimum :";
                    result = result + "\n\n    F(XMin) = " + string.Format("{0:f" + precision + "}", brent.GetSolutionFunction());
                    break;
               case "Golden Section":
                    GoldenSection godsection = new GoldenSection(new FunctionOne(TestFunOptimizing), param1, param2,Convert.ToInt32( param3));
                    result = "\n    Scopes   of segment  which a decision of task is on .";
                    result = result + "\n\n    a = " + string.Format("{0:f" + precision + "}", godsection.GetSolutionA());
                    result = result + "\n\n    b = " + string.Format("{0:f" + precision + "}", godsection.GetSolutionB());
                    break;
               case "Pijavsky":
                    Pijavsky pijavsky = new Pijavsky(new FunctionOne(TestFunOptimizing),  param1, param2, param3, Convert.ToInt32(param4));
                    result = "\n    Abscissa of the best point from found..";
                    result = result + "\n\n    F = " + string.Format("{0:f" + precision + "}", pijavsky.GetSolution());
                    break;
               //*** Statistics ***
               case "Correlation Pearson":


                    CorrelationPearson corelperson = new CorrelationPearson(massX, massF, 6);
                    result = "\n    Pearson product-moment correlation coefficient.";
                    result = result+"\n\n    K = " + string.Format("{0:f" + precision + "}", corelperson.GetSolution());
                    break;
               case "Correlation Spearmans Rank":
                    CorrelationSpearmansRank corelspear = new CorrelationSpearmansRank(massX, massF, 6);
                    result = "\n    Pearson product-moment correlation coefficient.";
                    result = result + "\n\n    K = " + string.Format("{0:f" + precision + "}", corelspear.GetSolution());
                    break;
               case "Descriptive Statistics Median":
                    DescriptiveStatisticsADevMedian desceripM = new DescriptiveStatisticsADevMedian(massX, 6);
                    result = "\n    Output parameters:";
                    result = result + "\n\n    M = " + string.Format("{0:f" + precision + "}", desceripM.GetSolution());
                    break;
               case "Descriptive Statistics Moments":
                    DescriptiveStatisticsMoments desceripMo = new DescriptiveStatisticsMoments(massX, 6);
                    result = "\n    Output parameters:";
                    result = result + "\n\n    M = " + string.Format("{0:f" + precision + "}", desceripMo.GetSolution());
                    result = result + "\n\n    Variance = " + string.Format("{0:f" + precision + "}", desceripMo.variance);
                    result = result + "\n\n    Skewness = " + string.Format("{0:f" + precision + "}", desceripMo.skewness) + " (if variance<>0; zero otherwise)";
                    result = result + "\n\n    Kurtosis = " + string.Format("{0:f" + precision + "}", desceripMo.kurtosis) + " (if variance<>0; zero otherwise)";
                    break;
               case "Descriptive Statistics Percentile":
                    DescriptiveStatisticsPercentile desceripP = new DescriptiveStatisticsPercentile(massX, 6, pointPercentile);
                    result = "\n    Output parameters:";
                    result = result + "\n\n    V = " + string.Format("{0:f" + precision + "}", desceripP.GetSolution());
                    break;
               case "Generator 1":
                    RandomGeneratorsMethod1 random1 = new RandomGeneratorsMethod1();
                    result = "\n    Output parameters:";
                    result = result + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random1.GetSolution());
                    break;
               case "Generator 2":
                    RandomGeneratorsMethod2 random2 = new RandomGeneratorsMethod2(Convert.ToInt32( pointGenerator));
                    result = "\n    Output parameters:";
                    result = result + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random2.GetSolution());
                    break;
               case "Generator 3":
                    RandomGeneratorsMethod3 random3 = new RandomGeneratorsMethod3();
                    result = "\n    Output parameters:";
                   result = result+ "\n\n    Random = " + string.Format("{0:f" + precision + "}", random3.GetSolution());
                    break;
               case "Generator 4":
                    RandomGeneratorsMethod4 random4 = new RandomGeneratorsMethod4();
                    result = "\n    Output parameters:";
                    result = result + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random4.GetSolution());
                    break;
               case "Generator 5":
                    RandomGeneratorsMethod5 random5 = new RandomGeneratorsMethod5(pointGenerator);
                    result = "\n    Output parameters:";
                    result = result + "\n\n    Random = " + string.Format("{0:f" + precision + "}", random5.GetSolution());
                    break;
            }
        }

        private static double CalculateFunctionX(string InputExpression, double x)
        {
            Parser MathParser = new Parser();
            //string InputExpression = "x^2 - y+5";
            string newStr = "";
            for (int i = 0; i < InputExpression.Length; i++)
            {
                switch (InputExpression[i])
                {
                    case 'x': newStr += x.ToString();
                        break;
                    case 'X': newStr += x.ToString();
                        break;
                    default:
                        newStr += InputExpression[i];
                        break;
                }
            }
            MathParser.Evaluate(newStr);
            return MathParser.Result;
        }
        private static double CalculateFunctionXY(string InputExpression, double x, double y)
        {
            Parser MathParser = new Parser();
            //string InputExpression = "x^2 - y+5";
            string newStr = "";
            for (int i = 0; i < InputExpression.Length; i++)
            {
                switch (InputExpression[i])
                {
                    case 'x': newStr += x.ToString();
                        break;
                    case 'X': newStr += x.ToString();
                        break;
                    case 'y': newStr += y.ToString();
                        break;
                    case 'Y': newStr += y.ToString();
                        break;
                    default:
                        newStr += InputExpression[i];
                        break;
                }
            }
            MathParser.Evaluate(newStr);
            return MathParser.Result;
        }
        public double TestFunDifferEquations(double x, double y)
        {
            return CalculateFunctionXY(TestFunction, x, y);//x * x * x - x * y + 8;
            /* Parser p = new Parser();
             p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
             p.Grammar.AddNamedConstant("y", Convert.ToDouble(y));
             p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
             p.Grammar.AddNamedConstant("Y", Convert.ToDouble(y));
             return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());*/
        }
        public static double TestFunNewton(double x)
        {
            return CalculateFunctionX(TestFunction, x); //(2.113f * x * x * x - 6.44f * x * x - 3.19f * x + 15.13f);
            /* Parser p = new Parser();
             p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
             p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
             return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());*/
        }
        public static double TestFunIteration(double x)
        {
            return CalculateFunctionX(TestFunction, x); //CalculateFunctionX("0,1697*x^3-0,5693*x^2-1,6*x+3,7300", x);
            //return (0.1697f * x * x * x - 0.5693f * x * x - 1.6000f * x + 3.7300f);
            /* Parser p = new Parser();
             p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
             p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
             return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());*/
        }
        public static double TestFunBisection(double x)
        {
            return CalculateFunctionX(TestFunction, x);
            // return (Math.Tan(0.9464f * x) - 1.3825f * x);
        }
        public static double TestFunNewton2(double x)
        {
            return (3 * 2.113f * x * x + 2 * (-6.44f) * x - 3.19f);
        }
        public static double TestFunInteger(double x)
        {
            return CalculateFunctionX(TestFunction, x); //Math.Sqrt(x) * Math.Sin(x);
            /* Parser p = new Parser();
             p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
             p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
             return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());*/
        }
        public static double TestFunNonLinearEquations(double x)
        {
            return CalculateFunctionX(TestFunction, x); //Math.Cos(x) - x * x + 1;
            /* Parser p = new Parser();
             p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
             p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
             return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());*/
        }
        public static double TestFunOptimizing(double x)
        {
            return CalculateFunctionX(TestFunction, x); //Math.Cos(x) - x * x * x + 9;
            /*Parser p = new Parser();
            p.Grammar.AddNamedConstant("x", Convert.ToDouble(x));
            p.Grammar.AddNamedConstant("X", Convert.ToDouble(x));
            return Convert.ToDouble(p.Parse(TestFunnction).Tree.ToPolishInversedNotationString());*/
        }
    }
}
