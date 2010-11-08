using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System.NumericalMethods
{

    /// <summary>
    /// Not tested
    /// </summary>

    class GeneticAlgorithm
    {
        public class Cconst
        {
            public const double Pc = 0.8;
            public const double Pm = 0.08;

        }

        public struct individual
        {

            public int[] chrom;
            public double value;
            public double fitness;

        }

        public class Realize
        {

            static void Select(int start, int stop, ref individual[] population, ref individual[] newpopulation, int POPSIZE)
            {
                int i, index;
                double sum = 0.0;

                double[] cfitness = new double[POPSIZE];
                double[] p = new double[stop];
                Random randObj = new Random();


                for (i = start; i < stop; i++)
                {
                    sum += population[i].fitness;
                }
                for (i = start; i < stop; i++)
                {
                    cfitness[i] = population[i].fitness / sum;
                }
                for (i = start + 1; i < stop; i++)
                {
                    cfitness[i] = cfitness[i - 1] + cfitness[i];
                }
                int k = 0;
                for (i = start; i < stop; i++)
                {

                    p[i] = (randObj.Next(1000 - k)) / 1000;
                    k++;
                    index = 0;
                    while (p[i] > cfitness[index])
                    {
                        index++;
                    }

                    newpopulation[i] = population[index];
                }
                for (i = start; i < stop; i++)
                    population[i] = newpopulation[i];

            }
            static void SelectionOperator(ref individual[] population, ref individual[] newpopulation, int PopSiz_e, int POPSIZE)
            {

                int start, stop;
                start = 0;
                stop = (int)(PopSiz_e / 3);
                Select(start, stop, ref population, ref newpopulation, POPSIZE);
                start = stop + 1;
                stop = (int)(2 * PopSiz_e / 3);
                Select(start, stop, ref population, ref newpopulation, POPSIZE);
                start = stop + 1;
                stop = PopSiz_e;
                Select(start, stop, ref population, ref newpopulation, POPSIZE);
            }

            static void CrossoverOperator(ref individual[] population, int POPSIZE, int PopSiz_e)
            {
                int i, j;
                int CHROMLENGTH = (int)Math.Sqrt(PopSiz_e);
                int[] index = new int[POPSIZE];
                int temp;
                double[] p = new double[PopSiz_e];
                Random randObj = new Random();
                int[] point = new int[PopSiz_e];

                int CityNo;

                for (i = 0; i < PopSiz_e; i++)
                    index[i] = i;

                for (i = 0; i < PopSiz_e; i++)
                {
                    point[i] = randObj.Next(PopSiz_e - i);
                    temp = index[i];
                    index[i] = index[point[i] + i];
                    index[point[i] + i] = temp;
                }

                for (i = 0; i < PopSiz_e - 1; i += 2)
                {

                    p[i] = (randObj.Next(1000 - i)) / 1000;
                    if (p[i] < Cconst.Pc)
                    {
                        point[i] = randObj.Next(CHROMLENGTH - 1) + 1;
                        for (j = point[i]; j < CHROMLENGTH; j++)
                        {
                            CityNo = population[index[i]].chrom[j];
                            population[index[i]].chrom[j] = population[index[i + 1]].chrom[j];
                            population[index[i + 1]].chrom[j] = CityNo;
                        }
                    }
                }
            }


            static void MutationOperator(ref individual[] population, int PopSiz_e)
            {

                int CHROMLENGTH = (int)Math.Sqrt(PopSiz_e);
                int CITYCOUNT = CHROMLENGTH;
                int i, j;
                double[] p = new double[CHROMLENGTH];

                Random randObj = new Random();


                for (i = 0; i < PopSiz_e; i++)
                {
                    for (j = 1; j < CHROMLENGTH; j++)
                    {

                        p[j] = (randObj.Next(1000 - j)) / 1000;
                        if (p[j] < Cconst.Pm)
                        {
                            population[i].chrom[j] = randObj.Next(CITYCOUNT - j) + 1;
                        }
                    }
                }
            }


            static void GenerateInitialPopulation(ref int[] CitySet, ref individual[] population, int PopSiz_e)
            {
                int i, j;
                Random randObj = new Random();
                int CITYCOUNT = (int)Math.Sqrt(PopSiz_e);

                for (i = 0; i < CITYCOUNT; i++)
                {
                    CitySet[i] = i + 1;
                }

                for (i = 0; i < PopSiz_e; i++)
                {
                    population[i].chrom[0] = 1;
                    for (j = 1; j < CITYCOUNT; j++)
                        population[i].chrom[j] = randObj.Next(CITYCOUNT - j) + 1;
                }
            }


            static void GenerateNextPopulation(ref individual[] population, ref individual[] newpopulation, int PopSiz_e, int POPSIZE)
            {
                SelectionOperator(ref population, ref newpopulation, PopSiz_e, POPSIZE);
                CrossoverOperator(ref population, POPSIZE, PopSiz_e);
                MutationOperator(ref population, PopSiz_e);
            }

            static void DeleteOneCity(ref int[] workset, int pos, int n)
            {
                int i;
                for (i = pos - 1; i < n - 1; i++)
                    workset[i] = workset[i + 1];
            }

            static void DecodeChromosome(ref int[] chrom1, ref int[] Result, ref int[] WorkSet, ref int[] CitySet, int CHROMLENGTH)
            {
                int CITYCOUNT = CHROMLENGTH;
                int[] pp = new int[CHROMLENGTH];


                int i, n;


                for (i = 0; i < CHROMLENGTH; i++)
                {
                    pp[i] = chrom1[i];

                }


                for (i = 0; i < CITYCOUNT; i++) WorkSet[i] = CitySet[i];
                n = CITYCOUNT;

                for (i = 0; i < CITYCOUNT; i++)
                {

                    Result[i] = WorkSet[pp[i] - 1];
                    DeleteOneCity(ref WorkSet, pp[i], n);
                    n--;
                }
            }



            static void OutputTextReport(ref individual currentbest, ref int generation, ref int[] TempCitySet, ref int[] WorkSet, ref int[] CitySet, int CHROMLENGTH, double[,] temp)
            {
                int i, j;


                TextWriter str = Console.Out;
                StreamWriter fileOut = new StreamWriter(new FileStream("graph_opt.txt",
                                                      FileMode.Create,
                                                      FileAccess.Write));
                Console.SetOut(fileOut);

                for (i = 0; i < CHROMLENGTH; i++)
                {
                    for (j = 0; j < CHROMLENGTH; j++)
                        Console.Write("{0:N}  ", temp[i, j]);

                    Console.WriteLine();
                }

                Console.SetOut(str);

                DecodeChromosome(ref currentbest.chrom, ref TempCitySet, ref WorkSet, ref CitySet, CHROMLENGTH);
                Console.SetOut(fileOut);
                Console.Write("\nНайкраще значення = {0:N}  при шляху ==>>  ", currentbest.value);

                for (i = 0; i < CHROMLENGTH; i++)
                    Console.Write("<{0}>", TempCitySet[i]);
                Console.WriteLine("\n");

                fileOut.Close();
                Console.SetOut(str);
            }



            static void CalculateFitness(ref individual[] population, ref int[] TempCitySet, ref double[,] CityDistance, ref int[] WorkSet, ref int[] CitySet, int PopSiz_e, int CITYCOUNT)
            {
                int i, j;
                double TotalDistance = 0.0;

                for (i = 0; i < PopSiz_e; i++)
                {

                    DecodeChromosome(ref population[i].chrom, ref TempCitySet, ref WorkSet, ref CitySet, CITYCOUNT);
                    for (j = 1; j < CITYCOUNT; j++)
                        TotalDistance = TotalDistance + CityDistance[TempCitySet[j - 1] - 1, TempCitySet[j] - 1];
                    TotalDistance = TotalDistance + CityDistance[TempCitySet[CITYCOUNT - 1] - 1, 0];
                    population[i].value = TotalDistance;
                    TotalDistance = 0.0;
                }

                for (i = 0; i < PopSiz_e; i++)
                {
                    population[i].fitness = CITYCOUNT / population[i].value;
                }
            }


            static void FindBestIndividual(ref individual[] population, ref individual bestindividual, ref individual currentbest, int generation, ref int[] TempCitySet, ref int[] WorkSet, ref int[] CitySet, int PopSiz_e, double[,] temp)
            {
                int i;
                bestindividual = population[0];

                for (i = 1; i < PopSiz_e; i++)

                    if (population[i].fitness > bestindividual.fitness)
                        bestindividual = population[i];

                if (generation == 0)
                {
                    currentbest = bestindividual;
                }
                else
                {
                    if (bestindividual.fitness > currentbest.fitness)
                    {
                        currentbest = bestindividual;
                        OutputTextReport(ref currentbest, ref generation, ref TempCitySet, ref WorkSet, ref CitySet, (int)Math.Sqrt(PopSiz_e), temp);
                    }
                }
            }




            public static void m3_Main(double[,] temp, int size)
            {
                int POPSIZE = (int)Math.Pow(size, 2) + size;
                int CITYCOUNT = size;
                int CHROMLENGTH = CITYCOUNT;
                int PopSiz_e = (int)Math.Pow(size, 2);
                int MaxGeneration = POPSIZE;

                individual bestindividual = new individual();
                individual currentbest = new individual();
                individual[] population = new individual[POPSIZE];
                individual[] newpopulation = new individual[POPSIZE];



                bestindividual.chrom = new int[CHROMLENGTH];
                currentbest.chrom = new int[CHROMLENGTH];
                for (int i = 0; i < POPSIZE; i++)
                {
                    population[i].chrom = new int[CHROMLENGTH];
                    newpopulation[i].chrom = new int[CHROMLENGTH];
                }

                int[] CitySet = new int[CITYCOUNT];
                int[] WorkSet = new int[CITYCOUNT];
                int[] TempCitySet = new int[CITYCOUNT];

                double[,] CityDistance = new double[CITYCOUNT, CITYCOUNT];
                CityDistance = temp;

                int generation = 0;

                GenerateInitialPopulation(ref CitySet, ref population, PopSiz_e);
                CalculateFitness(ref population, ref TempCitySet, ref CityDistance, ref WorkSet, ref CitySet, PopSiz_e, CITYCOUNT);
                FindBestIndividual(ref population, ref bestindividual, ref currentbest, generation, ref TempCitySet, ref WorkSet, ref CitySet, PopSiz_e, temp);
                OutputTextReport(ref currentbest, ref generation, ref TempCitySet, ref WorkSet, ref CitySet, CHROMLENGTH, temp);

                while (generation < MaxGeneration)
                {
                    generation++;
                    GenerateNextPopulation(ref population, ref newpopulation, PopSiz_e, POPSIZE);
                    CalculateFitness(ref population, ref TempCitySet, ref CityDistance, ref WorkSet, ref CitySet, PopSiz_e, CITYCOUNT);
                    FindBestIndividual(ref population, ref bestindividual, ref currentbest, generation, ref TempCitySet, ref WorkSet, ref CitySet, PopSiz_e, temp);
                }

            }
        }

    }
}
