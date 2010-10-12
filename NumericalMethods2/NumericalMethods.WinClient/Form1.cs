using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalMethods.DifferentialEquations;
using NumericalMethods.Approximation;
using NumericalMethods.Integration;
using NumericalMethods.SystemLinearEqualizations;
using NumericalMethods.MatrixAlgebra;
using NumericalMethods.NonLinearEquations;
using NumericalMethods.Optimizing;
using NumericalMethods.Interpolation;
using NumericalMethods.Statistics;
using NumericalMethods;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }





        private void Form1_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// For richTexBox2
            /// </summary>
            // Add the keywords to the list.
           
            CodeTextBox.Settings.Keywords.Add("namespace");
            CodeTextBox.Settings.Keywords.Add("class");
            CodeTextBox.Settings.Keywords.Add("using");
            CodeTextBox.Settings.Keywords.Add("public");
            CodeTextBox.Settings.Keywords.Add("private");
            CodeTextBox.Settings.Keywords.Add("if");
            CodeTextBox.Settings.Keywords.Add("then");
            CodeTextBox.Settings.Keywords.Add("else");
            CodeTextBox.Settings.Keywords.Add("while");
            CodeTextBox.Settings.Keywords.Add("{");
            CodeTextBox.Settings.Keywords.Add("}");
            CodeTextBox.Settings.Keywords.Add("true");
            CodeTextBox.Settings.Keywords.Add("false");
            CodeTextBox.Settings.Keywords.Add("double");
            CodeTextBox.Settings.Keywords.Add("bool");
            CodeTextBox.Settings.Keywords.Add("break");
            CodeTextBox.Settings.Keywords.Add("int");
            // Set the comment identifier. For Lua this is two minus-signs after each other (--). 
            // For C++ we would set this property to "//".
            CodeTextBox.Settings.Comment = "//";
            // Set the colors that will be used.
            CodeTextBox.Settings.KeywordColor = Color.Blue;
            CodeTextBox.Settings.CommentColor = Color.Green;
            CodeTextBox.Settings.StringColor = Color.Gray;
            CodeTextBox.Settings.IntegerColor = Color.Red;
            // Let's not process strings and integers.
            CodeTextBox.Settings.EnableStrings = false;
            CodeTextBox.Settings.EnableIntegers = false;
            // Let's make the settings we just set valid by compiling
            // the keywords to a regular expression.
            CodeTextBox.CompileKeywords();
            // Load a file and update the syntax highlighting.
            //richTextBox1.LoadFile("../script.lua", RichTextBoxStreamType.PlainText);
           // richTextBox1.ProcessAllLines();


            this.groupBox1.Size = new System.Drawing.Size(755, 167);

            TreeNode root = new TreeNode("Numerical Methods");

            TreeNode aproxdeci = new TreeNode("Approximate decision of equalization f(x)=0");
            aproxdeci.Nodes.Add("bm", "Bisection Method");
            aproxdeci.Nodes.Add("cm", "Chord Method");
            aproxdeci.Nodes.Add("im", "Iteration Method");
            aproxdeci.Nodes.Add("nm", "Newton Method");
            TreeNode diff = new TreeNode("Differential Equations");
            diff.Nodes.Add("es", "Euler Simple");
            diff.Nodes.Add("em", "Euler Modified");
            diff.Nodes.Add("ec", "Euler Corrected");
            diff.Nodes.Add("rk", "Runge-Kutta");
            TreeNode integr = new TreeNode("Integration");
            integr.Nodes.Add("che", "Chebishev");
            integr.Nodes.Add("sim", "Simpson");
            integr.Nodes.Add("sim2", "Simpson 2");
            integr.Nodes.Add("tra", "Trapezium");
            TreeNode interpo = new TreeNode("Interpolation");
            interpo.Nodes.Add("li","Lagrange Interpolator");
            interpo.Nodes.Add("ni","Newton Interpolator");
            interpo.Nodes.Add("nin","Neville Interpolator");
            interpo.Nodes.Add("si","Spline Interpolator");
            interpo.Nodes.Add("bi","Barycentric Interpolation");
            TreeNode linsys = new TreeNode("Linear Systems");
            linsys.Nodes.Add("gau", "Gaus");
            linsys.Nodes.Add("zei", "Zeidel");
            TreeNode nonlin = new TreeNode("Non Linear equalization");
            nonlin.Nodes.Add("half", "Half Division");
            nonlin.Nodes.Add("hord", "Hord Metod");
            nonlin.Nodes.Add("newt", "Newton Metod");
            nonlin.Nodes.Add("seca", "Secant Metod");
            TreeNode matralger = new TreeNode("Matrix Algebra");
            matralger.Nodes.Add("mad", "Matrix Determinant");
            matralger.Nodes.Add("decmat","Decomposition of matrix LU");
            matralger.Nodes.Add("matinv", "Matrix Inverse LU ");
            TreeNode optim = new TreeNode("Optimizing");
            optim.Nodes.Add("brent","Brentopt");
            optim.Nodes.Add("golsec","Golden Section");
            optim.Nodes.Add("pija","Pijavsky");
            TreeNode regres = new TreeNode("Regression");
            regres.Nodes.Add("lir","Linear Regression");
            regres.Nodes.Add("estpol","Estimated Polynomial");
            regres.Nodes.Add("Polynomial Function");
            regres.Nodes.Add("Polynomial Least Square Fit");
            regres.Nodes.Add("Weighted Point");

            TreeNode statist = new TreeNode("Statistics");
            statist.Nodes.Add("cp","Correlation Pearson");
            statist.Nodes.Add("csr","Correlation Spearmans Rank");
            statist.Nodes.Add("dsad","Descriptive Statistics A Dev");
            statist.Nodes.Add("dsm","Descriptive Statistics Median");
            statist.Nodes.Add("dsmo","Descriptive Statistics Moments");
            statist.Nodes.Add("dsp","Descriptive Statistics Percentile");
            TreeNode randomgener = new TreeNode("Random Generator");
            randomgener.Nodes.Add("m1","Method 1");
            randomgener.Nodes.Add("m2","Method 2");
            randomgener.Nodes.Add("m3","Method 3");
            randomgener.Nodes.Add("m4","Method 4");
            randomgener.Nodes.Add("m5","Method 5");
            statist.Nodes.Add(randomgener);

            root.Nodes.Add(aproxdeci);
            root.Nodes.Add(diff);
            root.Nodes.Add(integr);
            root.Nodes.Add(interpo);
            root.Nodes.Add(linsys);
            root.Nodes.Add(nonlin);
            root.Nodes.Add(matralger);
            root.Nodes.Add(optim);
            root.Nodes.Add(statist);
            //root.Nodes.Add(regres);
            treeView.Nodes.Add(root);
        }
      
   
        /// <summary>
        /// Поверненя стрічки з певною кількості знаків після коми
        /// </summary>
        /// <param name="digitStr"></param>
        /// <returns></returns>
        string ReturnLengthStringToGet(string digitStr)
        {
            int Number = 2;
            int lenStartDel = digitStr.Length;
            int Founde = 0;
            for (int i = 0; i < lenStartDel; i++)
            {
                if (digitStr[i] == ',')
                {
                    if (lenStartDel-i > 3)
                        Founde = i + Number+1;
                    else
                        Founde = i + Number;
                    break;
                }
            }
            return digitStr.Remove(Founde);
        }

        public void ExecuteProgram()
        {
            richTextBox2.Clear();
            
            switch (treeView.SelectedNode.Name.ToString())
            {
                /// <summary>
                /// Approximate decision of equalization f(x)=0
                /// </summary>
                case "nm":
                    Newton newton = new Newton(new FunctionOne(fNewton), new FunctionOne(dfNewton), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), double.Parse(tbPointsNumber.Text));
                
      


                    richTextBox2.Text = "\n\n     x= " + ReturnLengthStringToGet(newton.GetSolution().ToString());
                    break;
                case "im":
                    IterationMethod itermet = new IterationMethod(new FunctionOne(fNewton), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), double.Parse(tbPointsNumber.Text));
                    richTextBox2.Text = "\n\n     x= " +  ReturnLengthStringToGet(itermet.GetSolution().ToString());
                    break;
                case "cm":
                    Сhord chord = new Сhord(new FunctionOne(fNewton), new FunctionOne(dfNewton), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), double.Parse(tbPointsNumber.Text));
                    richTextBox2.Text = "\n\n     x= " +  ReturnLengthStringToGet(chord.GetSolution().ToString());
                    break;
                case "bm":
                    Bisection bisect = new Bisection(new FunctionOne(fNewton), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbPointsNumber.Text));
                    richTextBox2.Text = "\n\n     x= " +  ReturnLengthStringToGet(bisect.GetSolution().ToString());
                    break;
                /// <summary>
                /// Differential Equations
                /// </summary>
                case "rk":
                    RungeKutta4 rk4 = new RungeKutta4(new Function(f1), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), int.Parse(tbPointsNumber.Text));
                    var result1 = rk4.GetSolution();
                    chart1.Series.Clear();
                    Series s1 = new Series("Runge Kutta4");
                    for (int j = 0; j < int.Parse(tbPointsNumber.Text); j++)
                    {
                        s1.Points.Add(new DataPoint(result1[0, j], result1[1, j]));
                    }
                    chart1.Series.Add(s1);
                    break;

                case "es":
                    EulerSimple eulersimpl = new EulerSimple(new Function(f1), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), int.Parse(tbPointsNumber.Text));
                    var result2 = eulersimpl.GetSolution();
                    chart1.Series.Clear();
                    Series s2 = new Series("Euler Simple");
                    for (int j = 0; j < int.Parse(tbPointsNumber.Text); j++)
                    {
                        s2.Points.Add(new DataPoint(result2[0, j], result2[1, j]));
                    }
                    chart1.Series.Add(s2);

                    break;

                case "em":
                    EulerModified eulermod = new EulerModified(new Function(f1), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), int.Parse(tbPointsNumber.Text));
                    var result3 = eulermod.GetSolution();
                    chart1.Series.Clear();
                    Series s3 = new Series("Euler Modified");
                    for (int j = 0; j < int.Parse(tbPointsNumber.Text); j++)
                    {
                        s3.Points.Add(new DataPoint(result3[0, j], result3[1, j]));
                    }
                    chart1.Series.Add(s3);
                    break;
                case "ec":
                    EulerCorrected eulercor = new EulerCorrected(new Function(f1), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), int.Parse(tbPointsNumber.Text));
                    var result4 = eulercor.GetSolution();
                    chart1.Series.Clear();
                    Series s4 = new Series("Euler Corrected");
                    for (int j = 0; j < int.Parse(tbPointsNumber.Text); j++)
                    {
                        s4.Points.Add(new DataPoint(result4[0, j], result4[1, j]));
                    }
                    chart1.Series.Add(s4);
                    break;
                /// <summary>
                /// Integration
                /// </summary>
                 case "che":
                    Chebishev chebish = new Chebishev(new FunctionOne(fInteger), double.Parse(tbA.Text), double.Parse(tbB.Text), int.Parse(tbPointsNumber.Text));
                    var result5 = chebish.GetSolution();
                    for (int j = 0; j <= int.Parse(tbPointsNumber.Text); j++)
                        richTextBox2.Text = richTextBox2.Text + "\n   h = " + ReturnLengthStringToGet(result5[1, j].ToString()) + "                              \t   integral = " + ReturnLengthStringToGet(result5[0, j].ToString());
                    break;
                 case "sim":
                    Simpson simps = new Simpson(new FunctionOne(fInteger), double.Parse(tbA.Text), double.Parse(tbB.Text), int.Parse(tbPointsNumber.Text));
                    var result6 = simps.GetSolution();
                    for (int j = 0; j <= int.Parse(tbPointsNumber.Text); j++)
                        richTextBox2.Text = richTextBox2.Text + "\n   h = " + ReturnLengthStringToGet(result6[1, j].ToString()) + "                            \t   integral = " + result6[0, j];
                    break;
                 case "sim2":
                    Simpson2 simps2 = new Simpson2(new FunctionOne(fInteger), double.Parse(tbA.Text), double.Parse(tbB.Text), int.Parse(tbPointsNumber.Text));
                    var result7 = simps2.GetSolution();
                        richTextBox2.Text = "\n\n  integral = " + ReturnLengthStringToGet(result7.ToString());
                    break;
                 case "tra":
                    Trapezium trapez = new Trapezium(new FunctionOne(fInteger), double.Parse(tbA.Text), double.Parse(tbB.Text), int.Parse(tbPointsNumber.Text));
                    var result8 = trapez.GetSolution();
                    for (int j = 0; j <= int.Parse(tbPointsNumber.Text); j++)
                        richTextBox2.Text = richTextBox2.Text + "\n   h = " +ReturnLengthStringToGet( result8[1, j].ToString()) + "                             \t   integral = " + result8[0, j];
                    break;
                /// <summary>
                /// System Linear Equalizations
                /// </summary>
                 case "gau":
                    double[,] mas = new double[4, 5];
                    mas[0, 0] = double.Parse(textBox2.Text);
                    mas[0, 1] = double.Parse(textBox3.Text);
                    mas[0, 2] = double.Parse(textBox4.Text);
                    mas[0, 3] = double.Parse(textBox5.Text);
                    mas[0, 4] = double.Parse(textBox6.Text);
                    mas[1, 0] = double.Parse(textBox11.Text);
                    mas[1, 1] = double.Parse(textBox10.Text);
                    mas[1, 2] = double.Parse(textBox9.Text);
                    mas[1, 3] = double.Parse(textBox8.Text);
                    mas[1, 4] = double.Parse(textBox7.Text);
                    mas[2, 0] = double.Parse(textBox16.Text);
                    mas[2, 1] = double.Parse(textBox15.Text);
                    mas[2, 2] = double.Parse(textBox14.Text);
                    mas[2, 3] = double.Parse(textBox13.Text);
                    mas[2, 4] = double.Parse(textBox12.Text);
                    mas[3, 0] = double.Parse(textBox21.Text);
                    mas[3, 1] = double.Parse(textBox20.Text);
                    mas[3, 2] = double.Parse(textBox19.Text);
                    mas[3, 3] = double.Parse(textBox18.Text);
                    mas[3, 4] = double.Parse(textBox17.Text);
                    Gaus gaus = new Gaus(4,mas);
                    var result9 = gaus.GetSolution();
                    for (int j = 0; j <result9.Length; j++)
                        richTextBox2.Text = richTextBox2.Text + "\n         X" + j + "  =  " + result9[j] + ";";
                    break;
                 case "zei":
                    double[,] masA = new double[4, 5];
                    double[] masB = new double[4];
                    masA[0, 0] = double.Parse(textBox2.Text);
                    masA[0, 1] = double.Parse(textBox3.Text);
                    masA[0, 2] = double.Parse(textBox4.Text);
                    masA[0, 3] = double.Parse(textBox5.Text);
                    masA[1, 0] = double.Parse(textBox11.Text);
                    masA[1, 1] = double.Parse(textBox10.Text);
                    masA[1, 2] = double.Parse(textBox9.Text);
                    masA[1, 3] = double.Parse(textBox8.Text);
                    masA[2, 0] = double.Parse(textBox16.Text);
                    masA[2, 1] = double.Parse(textBox15.Text);
                    masA[2, 2] = double.Parse(textBox14.Text);
                    masA[2, 3] = double.Parse(textBox13.Text);
                    masA[3, 0] = double.Parse(textBox21.Text);
                    masA[3, 1] = double.Parse(textBox20.Text);
                    masA[3, 2] = double.Parse(textBox19.Text);
                    masA[3, 3] = double.Parse(textBox18.Text);
                    masB[0] = double.Parse(textBox6.Text);
                    masB[1] = double.Parse(textBox7.Text);
                    masB[2] = double.Parse(textBox12.Text);
                    masB[3] = double.Parse(textBox17.Text);
                    Zeidel zeidel = new Zeidel(4, masA, masB);
                    var result10 = zeidel.GetSolution();
                    for (int j = 0; j < result10.Length; j++)
                        richTextBox2.Text = richTextBox2.Text + "\n         X" + j + "  =  " + result10[j] +";";
                    break;
                 /// <summary>
                 /// Matrix Algebra
                 /// </summary>
                  case "mad":
                    double[,] mas2 = new double[3, 3];
                    mas2[0, 0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    mas2[0, 1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                    mas2[0, 2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                    mas2[1, 0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                    mas2[1, 1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                    mas2[1, 2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                    mas2[2, 0] = double.Parse(dataGridView1.Rows[2].Cells[0].Value.ToString());
                    mas2[2, 1] = double.Parse(dataGridView1.Rows[2].Cells[1].Value.ToString());
                    mas2[2, 2] = double.Parse(dataGridView1.Rows[2].Cells[2].Value.ToString());
                    MatrixDeterminant matrdet= new MatrixDeterminant();
                    richTextBox2.Text = " \n\n\n    Determinant =  " + matrdet.MatrixDet(mas2, 3) + ";";
                    break;
                 case "decmat":
                    double[,] mas3 = new double[3, 3];
                    mas3[0, 0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    mas3[0, 1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                    mas3[0, 2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                    mas3[1, 0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                    mas3[1, 1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                    mas3[1, 2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                    mas3[2, 0] = double.Parse(dataGridView1.Rows[2].Cells[0].Value.ToString());
                    mas3[2, 1] = double.Parse(dataGridView1.Rows[2].Cells[1].Value.ToString());
                    mas3[2, 2] = double.Parse(dataGridView1.Rows[2].Cells[2].Value.ToString());
                    MatrixLU matrlu = new MatrixLU(mas3,3,3);
                    var result_matrlu=matrlu.GetSolution();
                    var result_matrlu2 = matrlu.GetSolution2();
                    for (int i=0;i<3;i++)
                    {
                        for (int j=0;j<3;j++)
                            richTextBox2.Text = richTextBox2.Text+ "      \t "+ result_matrlu[i,j];
                        richTextBox2.Text = richTextBox2.Text+ " \n" ;
                    }
                    richTextBox2.Text =richTextBox2.Text+ " \n\n\n";
                    for (int i = 0; i < result_matrlu2.Length; i++)
                        richTextBox2.Text = richTextBox2.Text + "      " + result_matrlu2[i];
                    break;
                 case "matinv":
                    double[,] mas4 = new double[3, 3];
                    mas4[0, 0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    mas4[0, 1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                    mas4[0, 2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                    mas4[1, 0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                    mas4[1, 1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                    mas4[1, 2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                    mas4[2, 0] = double.Parse(dataGridView1.Rows[2].Cells[0].Value.ToString());
                    mas4[2, 1] = double.Parse(dataGridView1.Rows[2].Cells[1].Value.ToString());
                    mas4[2, 2] = double.Parse(dataGridView1.Rows[2].Cells[2].Value.ToString());
                    RMatrixLuInverse matrluinv = new RMatrixLuInverse();

                    MatrixLU matrlu2 = new MatrixLU(mas4,3,3);
                    if (matrluinv.rmatrixluinverse(mas4, 3,matrlu2.GetSolution2()) == true)
                    {
                        richTextBox2.Text =  "\n             An inverse matrix exists \n\n ";
                        var result_matrluinv = matrluinv.GetSolution();
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                richTextBox2.Text = richTextBox2.Text + "    \t" + result_matrluinv[i, j];
                            }
                            richTextBox2.Text = richTextBox2.Text + "\n\n";
                        }
                    }
                    else
                         richTextBox2.Text ="\n             An inverse matrix does not exist";
                    break;
                 /// <summary>
                 /// NonLinear Equations
                 /// </summary>
                 case "half":
                    HalfDivision halfdiv = new HalfDivision(new FunctionOne(fNonLinearEquations), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbPointsNumber.Text));
                    richTextBox2.Text = "\n\n     X = " + halfdiv.GetSolution()[0, 0] + "       Iterations = " + halfdiv.GetSolution()[1, 0];
                    break;
                 case "hord":
                    HordMetod hormet = new HordMetod(new FunctionOne(fNonLinearEquations), double.Parse(tbA.Text), double.Parse(tbB.Text), int.Parse(tbPointsNumber.Text));
                    richTextBox2.Text = "\n\n     X = " + hormet.GetSolution()[0, 0] + "       Iterations = " + hormet.GetSolution()[1, 0];
                    break;
                 case "newt":
                    NewtonMethod newt = new NewtonMethod(new FunctionOne(fNonLinearEquations), double.Parse(tbInitial.Text), double.Parse(tbPointsNumber.Text));
                    richTextBox2.Text = "\n\n     X = " + newt.GetSolution()[0, 0] + "       Iterations = " + newt.GetSolution()[1, 0];
                    break;
                case "seca":
                   Secant sec = new Secant(new FunctionOne(fNonLinearEquations), double.Parse(tbInitial.Text), double.Parse(tbPointsNumber.Text));
                   richTextBox2.Text = "\n\n     X = " + sec.GetSolution()[0, 0] + "       Iterations = " + sec.GetSolution()[1, 0];
                   break;
                /// <summary>
                /// Optimizing
                /// </summary>
                case "brent":
                   Brentopt brent = new Brentopt(new FunctionOne(fOptimizing), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbPointsNumber.Text));
                   richTextBox2.Text = "\n    Point of the found minimum :";
                   richTextBox2.Text =richTextBox2.Text+ "\n\n    XMin = " + brent.GetSolution() ;
                   richTextBox2.Text = richTextBox2.Text +"\n\n     A value of function is in the found minimum :";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    F(XMin) = " + brent.GetSolutionFunction();
                   break;
                case "golsec":
                   GoldenSection godsection = new GoldenSection(new FunctionOne(fOptimizing), double.Parse(tbA.Text), double.Parse(tbB.Text), int.Parse(tbPointsNumber.Text));
                   richTextBox2.Text = "\n    Scopes   of segment  which a decision of task is on .";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    a = " + godsection.GetSolutionA();
                   richTextBox2.Text = richTextBox2.Text + "\n\n    b = " + godsection.GetSolutionB();
                   break;
                case "pija":
                   Pijavsky pijavsky = new Pijavsky(new FunctionOne(fOptimizing), double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbInitial.Text), int.Parse(tbPointsNumber.Text));
                   richTextBox2.Text = "\n    Abscissa of the best point from found..";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    F = " + pijavsky.GetSolution();
                   break;
                /// <summary>
                /// Interpolation
                /// </summary>
                case "li":
                    double[] XLagr = new double[6];
                   XLagr[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XLagr[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XLagr[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XLagr[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XLagr[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XLagr[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                    double[] FLagr = new double[6];
                   FLagr[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   FLagr[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   FLagr[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   FLagr[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   FLagr[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   FLagr[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   LagrangeInterpolator lagran = new LagrangeInterpolator(XLagr, FLagr, int.Parse(textBox25.Text), double.Parse(textBox22.Text));
                   richTextBox2.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    P = " + lagran.GetSolution();
                   break;
                case "ni":
                   double[] XNew = new double[6];
                   XNew[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XNew[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XNew[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XNew[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XNew[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XNew[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   double[] FNew = new double[6];
                   FNew[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   FNew[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   FNew[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   FNew[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   FNew[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   FNew[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   NewtonInterpolator newinterpol = new NewtonInterpolator(XNew, FNew, int.Parse(textBox25.Text), double.Parse(textBox22.Text));
                   richTextBox2.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    P = " + newinterpol.GetSolution();
                   break;
                case "nin":
                   double[] XNewil = new double[6];
                   XNewil[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XNewil[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XNewil[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XNewil[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XNewil[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XNewil[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   double[] FNewil = new double[6];
                   FNewil[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   FNewil[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   FNewil[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   FNewil[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   FNewil[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   FNewil[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   NevilleInterpolator newill = new NevilleInterpolator(XNewil, FNewil, int.Parse(textBox25.Text), double.Parse(textBox22.Text));
                   richTextBox2.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    P = " + newill.GetSolution();
                   break;
                case "si":
                   double[] XSpline = new double[6];
                   XSpline[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XSpline[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XSpline[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XSpline[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XSpline[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XSpline[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   double[] FSpline = new double[6];
                   FSpline[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   FSpline[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   FSpline[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   FSpline[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   FSpline[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   FSpline[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   SplineInterpolator spline = new SplineInterpolator(XSpline, FSpline, int.Parse(textBox25.Text), double.Parse(textBox22.Text));
                   richTextBox2.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    P = " + spline.GetSolution();
                   break;
                case "bi":
                   double[] XBary = new double[6];
                   XBary[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XBary[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XBary[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XBary[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XBary[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XBary[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   double[] FBary = new double[6];
                   FBary[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   FBary[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   FBary[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   FBary[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   FBary[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   FBary[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());
                   double[] WBary = new double[6];
                   WBary[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   WBary[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   WBary[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   WBary[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   WBary[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   WBary[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   BarycentricInterpolation barycen = new BarycentricInterpolation(XBary, FBary,WBary, int.Parse(textBox25.Text), double.Parse(textBox22.Text));
                   richTextBox2.Text = "\n    A value interpolation polynomial is in the point of interpolation.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    P = " + barycen.GetSolution();
                   break;
                /// <summary>
                /// Statistics
                /// </summary>
                case "cp":
                   double[] XCorelP = new double[6];
                   XCorelP[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XCorelP[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XCorelP[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XCorelP[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XCorelP[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XCorelP[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   double[] YCorelP = new double[6];
                   YCorelP[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   YCorelP[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   YCorelP[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   YCorelP[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   YCorelP[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   YCorelP[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   CorrelationPearson corelperson = new CorrelationPearson(XCorelP, YCorelP, int.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Pearson product-moment correlation coefficient.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    K = " + corelperson.GetSolution();
                   break;
                case "csr":
                   double[] XCorelSR = new double[6];
                   XCorelSR[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XCorelSR[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XCorelSR[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XCorelSR[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XCorelSR[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XCorelSR[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   double[] YCorelSR = new double[6];
                   YCorelSR[0] = double.Parse(dataGridView1.Rows[1].Cells[0].Value.ToString());
                   YCorelSR[1] = double.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString());
                   YCorelSR[2] = double.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString());
                   YCorelSR[3] = double.Parse(dataGridView1.Rows[1].Cells[3].Value.ToString());
                   YCorelSR[4] = double.Parse(dataGridView1.Rows[1].Cells[4].Value.ToString());
                   YCorelSR[5] = double.Parse(dataGridView1.Rows[1].Cells[5].Value.ToString());

                   CorrelationSpearmansRank corelspear = new CorrelationSpearmansRank(XCorelSR, YCorelSR, int.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Pearson product-moment correlation coefficient.";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    K = " + corelspear.GetSolution();
                   break;
                case "dsad":
                   double[] XDecripSt = new double[6];
                   XDecripSt[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XDecripSt[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XDecripSt[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XDecripSt[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XDecripSt[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XDecripSt[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   DescriptiveStatisticsADev desceripSt = new DescriptiveStatisticsADev(XDecripSt, int.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    ADev = " + desceripSt.GetSolution();
                   break;
                case "dsm":
                   double[] XDecripM = new double[6];
                   XDecripM[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XDecripM[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XDecripM[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XDecripM[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XDecripM[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XDecripM[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   DescriptiveStatisticsADevMedian desceripM = new DescriptiveStatisticsADevMedian(XDecripM, int.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    M = " + desceripM.GetSolution();
                   break;
                case "dsmo":
                   double[] XDecripMo = new double[6];
                   XDecripMo[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XDecripMo[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XDecripMo[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XDecripMo[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XDecripMo[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XDecripMo[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   DescriptiveStatisticsMoments desceripMo = new DescriptiveStatisticsMoments(XDecripMo, int.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    M = " + desceripMo.GetSolution();
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Variance = " + desceripMo.variance;
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Skewness = " + desceripMo.skewness + " (if variance<>0; zero otherwise)";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Kurtosis = " + desceripMo.kurtosis + " (if variance<>0; zero otherwise)";
                   break;
                case "dsp":
                   double[] XDecripP = new double[6];
                   XDecripP[0] = double.Parse(dataGridView1.Rows[0].Cells[0].Value.ToString());
                   XDecripP[1] = double.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                   XDecripP[2] = double.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString());
                   XDecripP[3] = double.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                   XDecripP[4] = double.Parse(dataGridView1.Rows[0].Cells[4].Value.ToString());
                   XDecripP[5] = double.Parse(dataGridView1.Rows[0].Cells[5].Value.ToString());
                   DescriptiveStatisticsPercentile desceripP = new DescriptiveStatisticsPercentile(XDecripP, int.Parse(textBox25.Text), double.Parse(textBox22.Text));
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    V = " + desceripP.GetSolution();
                   break;
                case "m1":
                   RandomGeneratorsMethod1 random1 = new RandomGeneratorsMethod1();
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Random = " + random1.GetSolution();
                   break;
                case "m2":
                   RandomGeneratorsMethod2 random2 = new RandomGeneratorsMethod2(int.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Random = " + random2.GetSolution();
                   break;
                case "m3":
                   RandomGeneratorsMethod3 random3 = new RandomGeneratorsMethod3();
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Random1 = " + random3.GetSolution1();
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Random2 = " + random3.GetSolution2();
                   break;
                case "m4":
                   RandomGeneratorsMethod4 random4 = new RandomGeneratorsMethod4();
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Random = " + random4.GetSolution();
                   break;
                case "m5":
                   RandomGeneratorsMethod5 random5 = new RandomGeneratorsMethod5(double.Parse(textBox25.Text));
                   richTextBox2.Text = "\n    Output parameters:";
                   richTextBox2.Text = richTextBox2.Text + "\n\n    Random = " + random5.GetSolution();
                   break;


            }
        }
        public static double f1(double x, double y)
        {
            return (Math.Pow(x, 3) - x * y + 8);
           // return Math.Sin(x * y);
        }
        public static double fNewton(double x)
        {
            return (2.113f * x * x * x - 6.44f * x * x - 3.19f * x + 15.13f);
        }
        public static double dfNewton(double x)
        {
            return (3 * 2.113f * x * x + 2 * (-6.44f) * x - 3.19f);
        }

        public static double fInteger(double x)
        {
            return Math.Sqrt(x) * Math.Sin(x);
            //return (Math.Tan(0.9464f * x) - 1.3825f * x);
        }
        public static double fNonLinearEquations(double x)
        {
            return Math.Cos(x) - x * x + 1;
        }
        public static double fOptimizing(double x)
        {
            return Math.Cos(x) - x * x*x + 9;
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void btnGo_Click_1(object sender, EventArgs e)
        {
            ExecuteProgram();
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

       }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        void DiffeEquatVisible()
        {
            panel4.Visible = true;
            textBox1.Text = "Math.Sin(x * y)";
            label2.Text = "Beginning of interval:";
            tbA.Text = "0";
            label3.Text = "End of interval:";
            tbB.Text = "3,14";
            label4.Text = "Initial value:";
            tbInitial.Text = "1";
            tbInitial.Enabled = true;
            label5.Text = "Points number:";
            tbPointsNumber.Text = "40";
            tbInitial.Enabled = true;
            panel5.Visible = false;
            richTextBox2.Visible = false;
            panel2.Visible = true;
            panel2.Location = new System.Drawing.Point(18, 30);
            panel3.Visible = true;
            panel3.Location = new System.Drawing.Point(302, 30);
            label14.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label18.Visible = false;
            panel6.Visible = false;
            pictureBox1.Visible = true;
            chart1.Visible = true;
        }
        void ApproDecEquVisible()
        {
            panel4.Visible = true;
            textBox1.Text = "2.113 * x^3 - 6.44 * x^2 - 3.19 * x + 15.13";
            label2.Text = "Left limit of segment:";
            tbA.Text = "2,3";
            label3.Text = "Right limit of segment:";
            tbB.Text = "2,7";
            label4.Text = "Initial approaching:";
            tbInitial.Text = "2,3";
            tbInitial.Enabled = true;
            label5.Text = "Exactness of calculation:";
            tbPointsNumber.Text = "0,0001";
            tbInitial.Enabled = true;
            panel5.Visible = false;
            richTextBox2.Visible = true;
            panel2.Visible = true;
            panel2.Location = new System.Drawing.Point(18, 30);
            panel3.Visible = true;
            panel3.Location = new System.Drawing.Point(302, 30);
            label14.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label18.Visible = false;
            panel6.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void IntegrationVisible()
        {
            panel4.Visible = true;
            textBox1.Text = "Math.Sqrt(x)*Math.Sin(x)";
            label2.Text = "Beginning of interval a:";
            tbA.Text = "0";
            label3.Text = "End of interval b:";
            tbB.Text = "1";
            label5.Text = "Points number:";
            tbPointsNumber.Text = "20";
            richTextBox2.Visible = true;
            richTextBox2.Clear();
            panel5.Visible = false;
            panel2.Visible = true;
            panel2.Location = new System.Drawing.Point(18, 30);
            panel3.Visible = true;
            panel3.Location = new System.Drawing.Point(302, 30);
            label14.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label18.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void LinearSystemsVisible()
        {
            panel4.Visible = true;
            richTextBox2.Visible =true;
            panel5.Visible = true;
            panel5.Location = new System.Drawing.Point(76, 12);
            panel2.Visible = false;
            panel3.Visible = false;
            richTextBox2.Clear();
            label14.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label18.Visible = false;
            panel6.Visible = false;
            pictureBox1.Visible = false;  
            chart1.Visible = false;

        }
        void MatrixAlgebraVisible()
        {
            richTextBox2.Clear();
            panel4.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.Location = new System.Drawing.Point(117, 29);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Size = new System.Drawing.Size(260,113);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[0].HeaderText = "b1";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[1].HeaderText = "b2";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[2].HeaderText = "b3";
            dataGridView1.Rows.Add(3);
            label14.Visible = true;
            label14.Location = new System.Drawing.Point(5, 72);
            label4.Text = "Test Matrix  = >";
            dataGridView1.Rows[0].Cells[0].Value = "1";
            dataGridView1.Rows[0].Cells[1].Value = "2";
            dataGridView1.Rows[0].Cells[2].Value = "1";
            dataGridView1.Rows[1].Cells[0].Value = "3";
            dataGridView1.Rows[1].Cells[1].Value = "4";
            dataGridView1.Rows[1].Cells[2].Value = "4";
            dataGridView1.Rows[2].Cells[0].Value = "2";
            dataGridView1.Rows[2].Cells[1].Value = "4";
            dataGridView1.Rows[2].Cells[2].Value = "1";
            panel2.Visible = false;
            panel5.Visible = false;
            panel3.Visible = false;
            groupBox3.Visible = true;
            groupBox3.Location = new System.Drawing.Point(395, 30);
            richTextBox2.Visible = true;
            richTextBox2.Clear();
            label18.Visible = false;
            panel6.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void NonLinearEquations()
        {
            panel4.Visible = true;
            textBox1.Text = "Math.Cos(x) - x * x + 1";
            label2.Text = "Beginning of interval:";
            tbA.Text = "0";
            label3.Text = "End of interval:";
            tbB.Text = "1,5";
            richTextBox2.Visible = true;
            richTextBox2.Clear();
            panel5.Visible = false;
            panel2.Visible = true;
            panel2.Location = new System.Drawing.Point(18, 30);
            panel3.Visible = true;
            panel3.Location = new System.Drawing.Point(302, 30);
            label14.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            tbA.Enabled = true;
            tbB.Enabled = true;
            tbPointsNumber.Enabled = true;
            label18.Visible = false;
            panel6.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void OptimizingVisible()
        {
            panel4.Visible = true;
            textBox1.Text = "Math.Cos(x) - x * x*x + 9";
            label2.Text = "Beginning of interval a:";
            tbA.Text = "0";
            label3.Text = "End of interval b:";
            tbB.Text = "3";
            label5.Text = "Epsilon :";
            tbPointsNumber.Text = "0,0001";
            tbInitial.Enabled = false;
            richTextBox2.Visible = true;
            richTextBox2.Clear();
            panel5.Visible = false;
            panel2.Visible = true;
            panel2.Location = new System.Drawing.Point(18, 30);
            panel3.Visible = true;
            panel3.Location = new System.Drawing.Point(302, 30);
            label14.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label18.Visible = false;
            panel6.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void InterpolationVisible()
        {
            label18.Visible = true;
            label18.Text = "X  -   abscissas of points.";
            label18.Text = label18.Text + "\nF  -   values of functions in these points.";
            label18.Location = new System.Drawing.Point(16, 35);
            panel6.Location = new System.Drawing.Point(352, 30);
            panel6.Visible = true;
            
            richTextBox2.Clear();
            panel4.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.Columns.Clear();
            dataGridView1.Location = new System.Drawing.Point(16, 83);
            dataGridView1.Size = new System.Drawing.Size(320, 60);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[0].HeaderText = "1";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[1].HeaderText = "2";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[2].HeaderText = "3";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[3].HeaderText = "4";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[4].HeaderText = "5";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[5].HeaderText = "6";

            dataGridView1.Rows.Add(2);

            DataGridViewRow row0 = dataGridView1.Rows[0];
            row0.HeaderCell.Value = "X";
            DataGridViewRow row1 = dataGridView1.Rows[1];
            row1.HeaderCell.Value = "F";

            dataGridView1.Rows[0].Cells[0].Value = "1,5";
            dataGridView1.Rows[0].Cells[1].Value = "1,55";
            dataGridView1.Rows[0].Cells[2].Value = "1,6";
            dataGridView1.Rows[0].Cells[3].Value = "1,65";
            dataGridView1.Rows[0].Cells[4].Value = "1,7";
            dataGridView1.Rows[0].Cells[5].Value = "1,75";
            dataGridView1.Rows[1].Cells[0].Value = "-1,1";
            dataGridView1.Rows[1].Cells[1].Value = "-0,9";
            dataGridView1.Rows[1].Cells[2].Value = "-0,7";
            dataGridView1.Rows[1].Cells[3].Value = "-0,4";
            dataGridView1.Rows[1].Cells[4].Value = "-0,2";
            dataGridView1.Rows[1].Cells[5].Value = "0,1";
            panel2.Visible = false;
            panel5.Visible = false;
            panel3.Visible = false;
            richTextBox2.Visible = true;
            richTextBox2.Clear();

            label8.Text = "Mumber of points";
            label17.Visible = true;
            textBox22.Visible = true;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void StatisticsVisible()
        {
            label18.Visible = true;
            label18.Text = "X       -   sample 1.";
            label18.Text = label18.Text + "\nY       -   sample 2.";
            label18.Location = new System.Drawing.Point(16, 35);
            panel6.Location = new System.Drawing.Point(352, 30);
            panel6.Visible = true;
            label8.Text = "Sample size";
            label17.Visible = false;
            textBox22.Visible = false;

            richTextBox2.Clear();
            panel4.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.Columns.Clear();
            dataGridView1.Location = new System.Drawing.Point(16, 83);
            dataGridView1.Size = new System.Drawing.Size(320, 60);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[0].HeaderText = "1";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[1].HeaderText = "2";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[2].HeaderText = "3";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[3].HeaderText = "4";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[4].HeaderText = "5";
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridView1.Columns[5].HeaderText = "6";

            dataGridView1.Rows.Add(2);

            DataGridViewRow row0 = dataGridView1.Rows[0];
            row0.HeaderCell.Value = "X";
            DataGridViewRow row1 = dataGridView1.Rows[1];
            row1.HeaderCell.Value = "Y";

            dataGridView1.Rows[0].Cells[0].Value = "1,5";
            dataGridView1.Rows[0].Cells[1].Value = "1,55";
            dataGridView1.Rows[0].Cells[2].Value = "1,6";
            dataGridView1.Rows[0].Cells[3].Value = "1,65";
            dataGridView1.Rows[0].Cells[4].Value = "1,7";
            dataGridView1.Rows[0].Cells[5].Value = "1,75";
            dataGridView1.Rows[1].Cells[0].Value = "-1,1";
            dataGridView1.Rows[1].Cells[1].Value = "-0,9";
            dataGridView1.Rows[1].Cells[2].Value = "-0,7";
            dataGridView1.Rows[1].Cells[3].Value = "-0,4";
            dataGridView1.Rows[1].Cells[4].Value = "-0,2";
            dataGridView1.Rows[1].Cells[5].Value = "0,1";
            panel2.Visible = false;
            panel5.Visible = false;
            panel3.Visible = false;
            richTextBox2.Visible = true;
            richTextBox2.Clear();
            label14.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }
        void RandomGeneratorVisible()
        {
            label18.Visible = true;
            label18.Location = new System.Drawing.Point(140, 45);
            panel2.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            richTextBox2.Visible = true;
            richTextBox2.Clear();
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label14.Visible = false;
            pictureBox1.Visible = false;
            chart1.Visible = false;
        }

        private void treeView_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

            switch (treeView.SelectedNode.Name.ToString())
            {
                /// <summary>
                /// Approximate decision of equalization f(x)=0
                /// </summary>
                case "bm":
                    //richTextBox1.LoadFile("../../../NumericalMethodsLibrary/Approximation/Bisection.dat", RichTextBoxStreamType.PlainText);
                    CodeTextBox.Text = AllMethodText.Approximation_Bisection;
                    CodeTextBox.ProcessAllLines();
                    
                    ApproDecEquVisible();
                    tbInitial.Enabled = false;
                    break;
                case "im":
                    CodeTextBox.Text = AllMethodText.Approximation_Iterationmethod;
                    CodeTextBox.ProcessAllLines();
                    ApproDecEquVisible();
                    break;
                case "cm":
                    CodeTextBox.Text = AllMethodText.Approximation_Chord;
                    CodeTextBox.ProcessAllLines();
                    ApproDecEquVisible(); 
                    break;
                case "nm":
                    CodeTextBox.Text = AllMethodText.Approximation_Newton;
                    CodeTextBox.ProcessAllLines();
                    ApproDecEquVisible(); 
                    break;
                /// <summary>
                /// Differential Equations
                /// </summary>
                case "rk":
                    richTextBox3.Text = "";
                    pictureBox1.Load("../../../NumericalMethodsLibrary/DifferentialEquations/1.jpg");
                    CodeTextBox.Text = AllMethodText.DifferentialEquations_RungeKutta4;
                    DiffeEquatVisible();
                    break;
                case "es":
                    richTextBox3.Text = "";
                    pictureBox1.Load("../../../NumericalMethodsLibrary/DifferentialEquations/1.jpg");
                   // richTextBox3.LoadFile("../../../NumericalMethodsLibrary/DifferentialEquations/DirrefentialEquations.rtf", RichTextBoxStreamType.PlainText);
                    CodeTextBox.Text = AllMethodText.DifferentialEquations_EulerModified;
                    DiffeEquatVisible();
                    break;
                case "em":
                    richTextBox3.Text = "";
                    pictureBox1.Load("../../../NumericalMethodsLibrary/DifferentialEquations/1.jpg");
                    CodeTextBox.Text = AllMethodText.DifferentialEquations_EulerSimple;
                    DiffeEquatVisible();
                    break;
                case "ec":
                    richTextBox3.Text = "";
                    pictureBox1.Load("../../../NumericalMethodsLibrary/DifferentialEquations/1.jpg");
                    CodeTextBox.Text = AllMethodText.DifferentialEquations_EulerCorrected;
                    DiffeEquatVisible();
                    break;
                /// <summary>
                /// Integration
                /// </summary>
                case "che":
                    CodeTextBox.Text = AllMethodText.Integration_Chebishev;
                    IntegrationVisible();
                    break;
                case "sim":
                    CodeTextBox.Text = AllMethodText.Integration_Simpson;
                    IntegrationVisible();
                    break;
                case "sim2":
                    CodeTextBox.Text = AllMethodText.Integration_Simpson2;
                    IntegrationVisible();
                    break;
                case "tra":
                    CodeTextBox.Text = AllMethodText.Integration_Trapezium;
                    tbInitial.Enabled = false;
                    IntegrationVisible();
                    break;
                /// <summary>
                /// System Linear Equalizations
                /// </summary>

                case "gau":
                    CodeTextBox.Text = AllMethodText.SystemLinearEqualizations_Gaus;
                    LinearSystemsVisible();
                    break;
                case "zei":
                    CodeTextBox.Text = AllMethodText.SystemLinearEqualizations_Zeidel;
                    LinearSystemsVisible();
                    break;
                /// <summary>
                /// Matrix Algebra
                /// </summary>
                case "mad":
                    CodeTextBox.Text = AllMethodText.MatrixAlgebra_MatrixDeterminan;
                    MatrixAlgebraVisible();
                    break;
                case "decmat":
                    CodeTextBox.Text = AllMethodText.MatrixAlgebra_RMatrixLU;
                    MatrixAlgebraVisible();
                    break;
                case "matinv":
                    CodeTextBox.Text = AllMethodText.MatrixAlgebra_RMatrixLuInverse;
                    MatrixAlgebraVisible();
                    break;
                /// <summary>
                /// NonLinear Equations
                /// </summary>
                case "half":
                    CodeTextBox.Text = AllMethodText.NonLinearEquations_HalfDivision;
                    NonLinearEquations();
                    tbInitial.Enabled = false;
                    label5.Text = "Exactness of calculation:";
                    tbPointsNumber.Text = "0,00001";
                    
                    break;
                case "hord":
                    CodeTextBox.Text = AllMethodText.NonLinearEquations_MetodHord;
                    NonLinearEquations();
                    tbInitial.Enabled = false;
                    label5.Text = "Amount divisions of segmen:";
                    tbPointsNumber.Text = "20";
                    
                    break;
                case "newt":
                    CodeTextBox.Text = AllMethodText.NonLinearEquations_Newton;
                    NonLinearEquations();
                    tbA.Enabled = false;
                    tbB.Enabled = false;
                    tbInitial.Enabled = true;
                    label4.Text = "Initial approaching:";
                    tbInitial.Text = "2,1";
                    label5.Text = "Amount divisions of segmen:";
                    tbPointsNumber.Text = "20";
                    
                    break;
                case "seca":
                    CodeTextBox.Text = AllMethodText.NonLinearEquations_Secant;
                    NonLinearEquations();
                    tbA.Enabled = false;
                    tbB.Enabled = false;
                    label4.Text = "Delta:";
                    tbInitial.Text = "0,01";
                    label5.Text = "Step of treatment:";
                    tbPointsNumber.Text = "0,0001";                
                    break;
                /// <summary>
                /// Optimizing
                /// </summary>
                case "brent":
                    CodeTextBox.Text = AllMethodText.Optimizing_Brentopt;
                    OptimizingVisible();
                    break;
                case "golsec":
                    CodeTextBox.Text = AllMethodText.Optimizing_GoldenSection;
                    OptimizingVisible();
                    label5.Text = "Points number:";
                    tbPointsNumber.Text = "20";
                    break;
                case "pija":
                    CodeTextBox.Text = AllMethodText.Optimizing_Pijavsky;
                    OptimizingVisible();
                    label5.Text = "Number of steps of search:";
                    tbPointsNumber.Text = "20";
                    label4.Text = "Constant of Lipshits(>0):";
                    tbInitial.Text = "8,6";
                    tbInitial.Enabled = true;
                    break;

                /// <summary>
                /// Interpolation
                /// </summary>
                case "li":
                    CodeTextBox.Text = AllMethodText.Interpolation_LagrangeInterpolator;
                    InterpolationVisible();
                    break;
                case "ni":
                    CodeTextBox.Text = AllMethodText.Interpolation_NevilleInterpolator;
                    InterpolationVisible();
                    break;
                case "nin":
                    CodeTextBox.Text = AllMethodText.Interpolation_NewtonInterpolator;
                    InterpolationVisible();
                    break;
                case "si":
                    CodeTextBox.Text = AllMethodText.Interpolation_SplineInterpolator;
                    InterpolationVisible();
                    break;
                case "bi":
                    CodeTextBox.Text = AllMethodText.Interpolation_BarycentricInterpolation;
                    InterpolationVisible();
                    dataGridView1.Location = new System.Drawing.Point(16, 63);
                    dataGridView1.Size = new System.Drawing.Size(320, 80);
                    dataGridView1.Rows.Add(2);
                    DataGridViewRow row2 = dataGridView1.Rows[2];
                    row2.HeaderCell.Value = "W";
                    dataGridView1.Rows[2].Cells[0].Value = "50";
                    dataGridView1.Rows[2].Cells[1].Value = "20";
                    dataGridView1.Rows[2].Cells[2].Value = "90";
                    dataGridView1.Rows[2].Cells[3].Value = "11";
                    dataGridView1.Rows[2].Cells[4].Value = "10";
                    dataGridView1.Rows[2].Cells[5].Value = "15";
                    label18.Location = new System.Drawing.Point(16, 25);
                    label18.Text = "X  -   abscissas of points.  W -  gravimetric coefficients";
                    label18.Text =label18.Text+ "\n F  -   values of functions in these points.";
                    break;

                /// <summary>
                /// Statistics
                /// </summary>
                case "cp":
                    CodeTextBox.Text = AllMethodText.Statistics_CorrelationPearson;
                    StatisticsVisible();
                    break;
                case "csr":
                    CodeTextBox.Text = AllMethodText.Statistics_CorrelationSpearmansRank;
                    StatisticsVisible();
                    break;
                case "dsad":
                    CodeTextBox.Text = AllMethodText.Statistics_DescriptiveStatisticsADev;
                    StatisticsVisible();

                    richTextBox2.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Location = new System.Drawing.Point(16, 83);
                    dataGridView1.Size = new System.Drawing.Size(320, 60);
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[0].HeaderText = "1";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[1].HeaderText = "2";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[2].HeaderText = "3";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[3].HeaderText = "4";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[4].HeaderText = "5";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[5].HeaderText = "6";

                    dataGridView1.Rows.Add(1);

                    DataGridViewRow row0 = dataGridView1.Rows[0];
                    row0.HeaderCell.Value = "X";

                    dataGridView1.Rows[0].Cells[0].Value = "1,5";
                    dataGridView1.Rows[0].Cells[1].Value = "1,55";
                    dataGridView1.Rows[0].Cells[2].Value = "1,6";
                    dataGridView1.Rows[0].Cells[3].Value = "1,65";
                    dataGridView1.Rows[0].Cells[4].Value = "1,7";
                    dataGridView1.Rows[0].Cells[5].Value = "1,75";
                    label18.Text = "X       -   sample 1.";
                    break;
                case "dsm":
                    CodeTextBox.Text = AllMethodText.Statistics_DescriptiveStatisticsMedian;
                    StatisticsVisible();

                    richTextBox2.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Location = new System.Drawing.Point(16, 83);
                    dataGridView1.Size = new System.Drawing.Size(320, 60);
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[0].HeaderText = "1";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[1].HeaderText = "2";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[2].HeaderText = "3";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[3].HeaderText = "4";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[4].HeaderText = "5";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[5].HeaderText = "6";

                    dataGridView1.Rows.Add(1);

                    DataGridViewRow row00 = dataGridView1.Rows[0];
                    row00.HeaderCell.Value = "X";

                    dataGridView1.Rows[0].Cells[0].Value = "1,5";
                    dataGridView1.Rows[0].Cells[1].Value = "1,55";
                    dataGridView1.Rows[0].Cells[2].Value = "1,6";
                    dataGridView1.Rows[0].Cells[3].Value = "1,65";
                    dataGridView1.Rows[0].Cells[4].Value = "1,7";
                    dataGridView1.Rows[0].Cells[5].Value = "1,75";
                    label18.Text = "X       -   sample 1.";

                    
                    break;
                case "dsmo":
                    CodeTextBox.Text = AllMethodText.Statistics_DescriptiveStatisticsMoments;
                    StatisticsVisible();

                    richTextBox2.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Location = new System.Drawing.Point(16, 83);
                    dataGridView1.Size = new System.Drawing.Size(320, 60);
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[0].HeaderText = "1";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[1].HeaderText = "2";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[2].HeaderText = "3";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[3].HeaderText = "4";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[4].HeaderText = "5";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[5].HeaderText = "6";

                    dataGridView1.Rows.Add(1);

                    DataGridViewRow row01 = dataGridView1.Rows[0];
                    row01.HeaderCell.Value = "X";

                    dataGridView1.Rows[0].Cells[0].Value = "1,5";
                    dataGridView1.Rows[0].Cells[1].Value = "1,55";
                    dataGridView1.Rows[0].Cells[2].Value = "1,6";
                    dataGridView1.Rows[0].Cells[3].Value = "1,65";
                    dataGridView1.Rows[0].Cells[4].Value = "1,7";
                    dataGridView1.Rows[0].Cells[5].Value = "1,75";
                    label18.Text = "X       -   sample 1.";
                    break;
                case "dsp":
                    CodeTextBox.Text = AllMethodText.Statistics_DescriptiveStatisticsPercentile;
                    StatisticsVisible();

                    richTextBox2.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Location = new System.Drawing.Point(16, 83);
                    dataGridView1.Size = new System.Drawing.Size(320, 60);
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[0].HeaderText = "1";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[1].HeaderText = "2";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[2].HeaderText = "3";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[3].HeaderText = "4";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[4].HeaderText = "5";
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                    dataGridView1.Columns[5].HeaderText = "6";

                    dataGridView1.Rows.Add(1);

                    DataGridViewRow row02 = dataGridView1.Rows[0];
                    row02.HeaderCell.Value = "X";

                    dataGridView1.Rows[0].Cells[0].Value = "1,5";
                    dataGridView1.Rows[0].Cells[1].Value = "1,55";
                    dataGridView1.Rows[0].Cells[2].Value = "1,6";
                    dataGridView1.Rows[0].Cells[3].Value = "1,65";
                    dataGridView1.Rows[0].Cells[4].Value = "1,7";
                    dataGridView1.Rows[0].Cells[5].Value = "1,75";
                    label18.Text = "X       -   sample 1.";

                    label17.Text = "Percentile (0<=P<=1):";
                    label17.Visible = true;
                    textBox22.Visible = true;
                    textBox22.Text = "0,4";
                    break;
                /// <summary>
                /// Random Generator
                /// </summary>
                  case "m1":
                    CodeTextBox.Text = AllMethodText.RandomGeneratorsMethod1;
                    RandomGeneratorVisible();
                    
                    label18.Text = " Generator of the evenly distributed random material numbers ";
                    label18.Text = label18.Text + "\n\n                          in a range [0, 1]";
                    label18.Location = new System.Drawing.Point(120, 35);
                    break;
                  case "m2":
                    CodeTextBox.Text = AllMethodText.RandomGeneratorsMethod2;
                    RandomGeneratorVisible();
                    panel6.Location = new System.Drawing.Point(352, 30);
                    panel6.Visible = true;
                    label8.Visible = true;
                    label8.Text = "  Input range N:";
                    label17.Visible = false;
                    textBox25.Enabled = true;
                    textBox22.Visible = false;
                    label18.Text = " Generator of the evenly distributed random material numbers ";
                    label18.Text = label18.Text + "\n\n                          in a range [0, N]";
                    label18.Location = new System.Drawing.Point(30, 35);
                    break;
                  case "m3":
                    CodeTextBox.Text = AllMethodText.RandomGeneratorsMethod3;
                    RandomGeneratorVisible();
                    label18.Text = "             Generator of the normally distributed random numbers. ";
                    label18.Text = label18.Text + "\n\n  Generates two independent random   numbers,having standard distributing. ";
                    label18.Text = label18.Text + "\n  On the expenses of time equal to podprogramme of RndNormal,generating ";
                    label18.Text = label18.Text + "\n                                     one random number.";
                    label18.Location = new System.Drawing.Point(90, 35);
                    break;
                  case "m4":
                    CodeTextBox.Text = AllMethodText.RandomGeneratorsMethod4;
                    RandomGeneratorVisible();
                    label18.Text = "             Generator of the normally distributed random numbers. ";
                    label18.Text = label18.Text + "\n\n  Generates one random  number, having  the standard  distributing.  ";
                    label18.Text = label18.Text + "\n  On the expenses of time equal to podprogramme of RndNormal2, to ";
                    label18.Text = label18.Text + "\n                        generating two random numbers.";
                    label18.Location = new System.Drawing.Point(90, 35);
                    break;
                  case "m5":
                    CodeTextBox.Text = AllMethodText.RandomGeneratorsMethod5;
                    RandomGeneratorVisible();
                    panel6.Location = new System.Drawing.Point(352, 30);
                    panel6.Visible = true;
                    label8.Visible = true;
                    label8.Text = "   Input lambda ";
                    label17.Visible = false;
                    textBox25.Enabled = true;
                    textBox22.Visible = false;
                    label18.Text = " Generator of the exponentially ";
                    label18.Location = new System.Drawing.Point(90, 35);
                    label18.Text = label18.Text + "\n\n  distributed random numbers.";
                    break;
            }
        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {

      
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        } 
    }
}