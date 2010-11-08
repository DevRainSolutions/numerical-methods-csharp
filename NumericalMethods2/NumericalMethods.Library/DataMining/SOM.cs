using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace System.Data.Clustering
{
    public class SOM
    {
        private Neuron[,] outputs;  // Collection of weights.
        private int iteration;      // Current iteration.
        private int length;        // Side length of output grid.
        private int dimensions;    // Number of input dimensions.
        private Random rnd = new Random();

        private List<string> labels = new List<string>();
        private List<double[]> patterns = new List<double[]>();

        public SOM(int dimensions, int length, string file)
        {
            this.length = length;
            this.dimensions = dimensions;
            Initialise();
            LoadData(file);
            NormalisePatterns();
            Train(0.0000001);
            DumpCoordinates();
        }

        private void Initialise()
        {
            outputs = new Neuron[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    outputs[i, j] = new Neuron(i, j, length);
                    outputs[i, j].Weights = new double[dimensions];

                    for (int k = 0; k < dimensions; k++)
                    {
                        outputs[i, j].Weights[k] = rnd.NextDouble();
                    }
                }
            }
        }

        private void LoadData(string file)
        {
            StreamReader reader = File.OpenText(file);
            reader.ReadLine(); // Ignore first line.

            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                labels.Add(line[0]);
                double[] inputs = new double[dimensions];

                for (int i = 0; i < dimensions; i++)
                {
                    inputs[i] = double.Parse(line[i + 1], CultureInfo.InvariantCulture);
                }
                patterns.Add(inputs);
            }
            reader.Close();
        }

        private void NormalisePatterns()
        {
            for (int j = 0; j < dimensions; j++)
            {
                double sum = 0;
                for (int i = 0; i < patterns.Count; i++)
                {
                    sum += patterns[i][j];
                }

                double average = sum / patterns.Count;

                for (int i = 0; i < patterns.Count; i++)
                {
                    patterns[i][j] = patterns[i][j] / average;
                }
            }
        }

        private void Train(double maxError)
        {
            double currentError = double.MaxValue;

            while (currentError > maxError)
            {
                currentError = 0;
                List<double[]> TrainingSet = new List<double[]>();
                foreach (double[] pattern in patterns)
                {
                    TrainingSet.Add(pattern);
                }

                for (int i = 0; i < patterns.Count; i++)
                {
                    double[] pattern = TrainingSet[rnd.Next(patterns.Count - i)];
                    currentError += TrainPattern(pattern);
                    TrainingSet.Remove(pattern);
                }
                Console.WriteLine(currentError.ToString("0.0000000"));
            }
        }

        private double TrainPattern(double[] pattern)
        {
            double error = 0;
            Neuron winner = Winner(pattern);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    error += outputs[i, j].UpdateWeights(pattern, winner, iteration);
                }
            }
            iteration++;
            return Math.Abs(error / (length * length));
        }

        private void DumpCoordinates()
        {
            for (int i = 0; i < patterns.Count; i++)
            {
                Neuron n = Winner(patterns[i]);
                Console.WriteLine("{0},{1},{2}", labels[i], n.X, n.Y);
            }
        }

        private Neuron Winner(double[] pattern)
        {
            Neuron winner = null;
            double min = double.MaxValue;

            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    double d = Distance(pattern, outputs[i, j].Weights);
                    if (d < min)
                    {
                        min = d;
                        winner = outputs[i, j];
                    }
                }
            return winner;
        }

        private double Distance(double[] vector1, double[] vector2)
        {
            double value = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                value += Math.Pow((vector1[i] - vector2[i]), 2);
            }
            return Math.Sqrt(value);
        }
    }

    public class Neuron
    {
        public double[] Weights;
        public int X;
        public int Y;
        private int length;
        private double nf;

        public Neuron(int x, int y, int length)
        {
            X = x;
            Y = y;
            this.length = length;
            nf = 1000 / Math.Log(length);
        }

        private double Gauss(Neuron win, int it)
        {
            double distance = Math.Sqrt(Math.Pow(win.X - X, 2) + Math.Pow(win.Y - Y, 2));
            return Math.Exp(-Math.Pow(distance, 2) / (Math.Pow(Strength(it), 2)));
        }

        private double LearningRate(int it)
        {
            return Math.Exp(-it / 1000) * 0.1;
        }

        private double Strength(int it)
        {
            return Math.Exp(-it / nf) * length;
        }

        public double UpdateWeights(double[] pattern, Neuron winner, int it)
        {
            double sum = 0;

            for (int i = 0; i < Weights.Length; i++)
            {
                double delta = LearningRate(it) * Gauss(winner, it) * (pattern[i] - Weights[i]);
                Weights[i] += delta;
                sum += delta;
            }
            return sum / Weights.Length;
        }
    }
}
