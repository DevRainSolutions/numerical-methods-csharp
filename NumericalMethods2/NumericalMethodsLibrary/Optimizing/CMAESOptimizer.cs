using System;
using System.Diagnostics;
using System.Threading;
using NumericalMethods.MatrixAlgebra;

namespace NumericalMethods.Optimization
{
 
    public class CMAESOptimizer : MultidimensionalOptimizer
    {
        #region props and fields
        private double bestEnergy = Double.MaxValue;
        public double BestEnergy { get { return bestEnergy; } private set { bestEnergy = value; } }
        private double[] bestSolution = null;
        public double[] BestSolution { get { return bestSolution; } private set { bestSolution = value; } }
        private readonly const double defaultlb = -3;
        private readonly const double defaultub = 3;
        private double[] lb = null;
        private double[] ub = null;
        private double[] s = null;
        private int populationSize = 10;
        public int PopulationSize { get { return populationSize; } private set { populationSize = value; } }
        private int d = 1;
        private Random rgen = new Random();
        #endregion

        #region ctors
        public CMAESOptimizer(Func<double[],double> fitnessFunction, int problemDimension, double[] seed = null,
            double[] lowerBound = null, double[] upperBound = null, int particlePopulationSize = 10, double precision = 1e-8)
        {
            if (fitnessFunction == null)
                throw new ArgumentNullException("fitnessFunction", "The fitnessFunction needs to be defined a priori.");
            f = fitnessFunction;

            if (problemDimension <= 0)
                throw new ArgumentOutOfRangeException("problemDimension", String.Format("problemDimension={0}", problemDimension));
            d = problemDimension;

            if (lowerBound != null && lowerBound.Length != d)
                throw new ArgumentException("Argument needs to have the same declared problemDimension", "lowerBound");
            lb = lowerBound;
            
            if (upperBound != null && upperBound.Length != d)
                throw new ArgumentException("Argument needs to have the same declared problemDimension", "upperBound");
            ub = upperBound;
            
            if (seed != null && seed.Length != d)
                throw new ArgumentException("Argument needs to have the same declared problemDimension", "seed");
            s = seed;

            if (particlePopulationSize < 4)
                throw new ArgumentOutOfRangeException("particlePopulationSize", "Particle population size must be greater thatn 4.");
            populationSize = particlePopulationSize;

        }

        #endregion

        #region methods
        private void init(out int lambda, out int mu, out Matrix omega, out Matrix pc, out Matrix ps, 
            out Matrix B, out Matrix D, out Matrix C, out double mueff, out double sigma, 
            out double csigma, out double dsigma, out double cc, out double c1,
            out double cmu, out double chiN )
        {
            sigma = 0.5;
            lambda = 10 + populationSize + (int)(3 * Math.Log(1.0 * d ));
            mu = (int)(lambda / 2.0);

            double sum=0;

            for(int i = 0 ; i < mu ; ++i)
                sum += Math.Log(lambda/2 + .5) - Math.Log(i+1.0);

            omega = new Matrix( mu, mu );
            omega = omega * 0.0;

            double sum2 = 0;
            for (int i = 0; i < mu; i++)
            {
                omega[i,i] = (Math.Log(lambda / 2 + 0.5) - Math.Log(i + 1.0)) / sum;
                sum2 += omega[i, i] * omega[i, i];
            }

            mueff = 1.0 / sum2;

            csigma = (mueff + 2) / (d + mueff + 5);

            double temp = Math.Sqrt((mueff - 1) / (d + 1)) - 1;
            temp = Math.Max(0.0, temp);

            dsigma = 1 + csigma + 2 * temp;

            cc = (4.0 + mueff / d) / (d + 4.0 + 2.0 * mueff / d);
            c1 = 2.0 / ((d + 1.3) * (d + 1.3) + mueff);

            temp = 2.0 * (mueff - 2.0 + 1.0 / mueff) / ((d + 2.0) * (d + 2.0) + mueff);

            cmu = Math.Min(1.0 - c1, temp);

            pc = new Matrix(d, 1);
            ps = new Matrix(d, 1);

            B = new Matrix(d, d);
            D = new Matrix(d, d);
            C = new Matrix(d, d);

            pc.setTo(0.0);
            ps.setTo(0.0);

            B.setToIdentity();
            D.setToIdentity();

            Console.WriteLine( B.ToString() );
            Console.ReadLine( );

            C = B * D.TransposedProduct( B * D );

            chiN = Math.Sqrt(d) * (1.0 - 1.0 / (4.0 * d) + 1.0 / (21.0 * d * d));

            bestEnergy = double.MaxValue;

        }

        public override void optimize( uint max_no_generations )
        {
            
            int lambda, mu;
            Matrix omega, B, D, C, pc, ps;
            double mueff, sigma, csigma, dsigma, cc, c1, cmu, chiN;
            
            init(out lambda, out mu, out omega, out pc, out ps, out B, out D, out C,
                out mueff, out sigma, out csigma, out dsigma,
                out cc, out c1, out cmu, out chiN);

            uint generation = 0, evalCount = 0, maxEvalCount = uint.MaxValue;

            double[,] arx, arz, xsel, zsel;
            double[] xmean,zmean;

            arx = new double[d, lambda];
            arz = new double[d, lambda];
            xsel = new double[d, mu];
            zsel = new double[d, mu];
            xmean = new double[d];
            zmean = new double[d];


            if (s == null) 
            {
                for (int i = 0; i < d; i++)
                    xmean[i] = rgen.NextDouble();
			}else{
                s.CopyTo( xmean, index:0 );
			}

            xmean.CopyTo( bestSolution, index:0 );

            bestEnergy = f(bestSolution);

            do //Main Loop
            {
                for (int i = 0; i < d; i++)
                    for (int j = 0; j < d; j++)
                        arz[i, j] = rgen.NextDouble();


                {

                }
            } while (generation < max_no_generations);
        }

       
        #endregion
    }
}
