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
using System.Xml;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

namespace NumericalMethods_Silverlight.Code
{
    public class ProcessingData
    {

        public string resultXML;
        public string TestFunnction;
        public string ParamInput1 = "";
        public string ParamInput2 = "";
        public string ParamInput3 = "";
        public string ParamInput4 = "";
        public int rangeArray;
        public double pointPercentile;
        public double[] LinSysMasA;
        public double[,] LinSysMatrixB;
        public double[,] MatrixAlgebraA;
        public string str1 = "";
        public string str2 = "";
        public double[] massX;
        public double[] massF;
        public double[] massW;
        public double pointInterpolation;
        public double pointGenerator;
        public ProcessingData()
        {

        }

        public void InpuParamMethod(string NameTypeMethod, string NameInMethod)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            XmlReader reader = XmlReader.Create("SampleData/SaveData.xml");
            
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement(NameTypeMethod))
                {
                    if (reader.GetAttribute("id") == NameInMethod)
                    {
                        TestFunnction = reader.GetAttribute("Function");
                        ParamInput1 = reader.GetAttribute("ParamInput1");
                        ParamInput2 = reader.GetAttribute("ParamInput2");
                        ParamInput3 = reader.GetAttribute("ParamInput3");
                        ParamInput4 = reader.GetAttribute("ParamInput4");
                    }
                }
            }
            reader.Close();
        }
        public void InpuLinearSystems(string NameTypeMethod, string NameInMethod)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            XmlReader reader = XmlReader.Create("SampleData/SaveData.xml");

            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement(NameTypeMethod))
                {
                    if (reader.GetAttribute("id") == NameInMethod)
                    {
                        rangeArray = Convert.ToInt32(reader.GetAttribute("Range"));
                        str1 = reader.GetAttribute("LinSysMasA");
                        str2 = reader.GetAttribute("LinSysMatrixB");
                        LinSysMasA = new double[rangeArray];
                        for (int i = 0; i < rangeArray; i++)
                        {
                            LinSysMasA[i] = Convert.ToDouble(str1.Split(' ')[i]);
                        }
                        int s = 0;
                        LinSysMatrixB = new double[rangeArray, rangeArray];
                        for (int i = 0; i < rangeArray; i++)
                        {
                            for (int j = 0; j < rangeArray; j++)
                            {
                                LinSysMatrixB[i, j] = Convert.ToDouble(str2.Split(' ')[s]);
                                s++;
                            }
                        }
                        break;
                    }
                }
            }
            reader.Close();
        }
        public void InpuMatrixAlgebra(string NameTypeMethod, string NameInMethod)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            XmlReader reader = XmlReader.Create("SampleData/SaveData.xml");

            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement(NameTypeMethod))
                {
                    if (reader.GetAttribute("id") == NameInMethod)
                    {
                        rangeArray = Convert.ToInt32(reader.GetAttribute("Range"));
                        str1 = reader.GetAttribute("Matrix");
                        MatrixAlgebraA = new double[rangeArray, rangeArray];
                        int s = 0;
                        for (int i = 0; i < rangeArray; i++)
                        {
                            for (int j = 0; j < rangeArray; j++)
                            {
                                MatrixAlgebraA[i, j] = Convert.ToDouble(str1.Split(' ')[s]);
                                s++;
                            }
                        }
                        break;
                    }
                }
            }
            reader.Close();
        }
        public void InpuInterpolation(string NameTypeMethod, string NameInMethod)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            XmlReader reader = XmlReader.Create("SampleData/SaveData.xml");

            reader.MoveToContent();
            string strX = "";
            string strF = "";
            string strW = "";
            int count = 0;
            while (reader.Read())
            {
                if (reader.IsStartElement(NameTypeMethod))
                {
                    if (reader.GetAttribute("id") == NameInMethod)
                    {
                        pointInterpolation = Convert.ToDouble(reader.GetAttribute("ParamInput1"));
                        strX = reader.GetAttribute("X");
                        strF = reader.GetAttribute("F");
                        if (reader.GetAttribute("id") == "BarycentricInterpolator")
                            strW = reader.GetAttribute("W");
                        for (int i = 0; i < strX.Length; i++)
                            if (strX[i] == ';')
                                count++;
                        massX = new double[count];
                        massF = new double[count];

                        massW = new double[count];
                        for (int i = 0; i < count; i++)
                        {
                            massX[i] = Convert.ToDouble(strX.Split(';')[i]);
                            massF[i] = Convert.ToDouble(strF.Split(';')[i]);
                        }
                        if (reader.GetAttribute("id") == "BarycentricInterpolator")
                        {
                            for (int i = 0; i < count; i++)
                            {
                                massW[i] = Convert.ToDouble(strW.Split(';')[i]);
                            }
                        }
                        break;
                    }
                }
            }
            reader.Close();
        }
        public void InpuStatistics(string NameTypeMethod, string NameInMethod)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            XmlReader reader = XmlReader.Create("SampleData/SaveData.xml");

            reader.MoveToContent();
            string strX = "";
            string strF = "";

            int count = 0;
            while (reader.Read())
            {
                if (reader.IsStartElement(NameTypeMethod))
                {
                    if (reader.GetAttribute("id") == NameInMethod)
                    {
                        if (reader.GetAttribute("id") == "DescriptiveStatisticsPercentile")
                            pointPercentile = Convert.ToDouble(reader.GetAttribute("ParamInput"));

                        strX = reader.GetAttribute("X");
                        if ((reader.GetAttribute("id") == "CorrelationPearson") || (reader.GetAttribute("id") == "CorrelationSpearmansRank"))
                            strF = reader.GetAttribute("F");

                        for (int i = 0; i < strX.Length; i++)
                            if (strX[i] == ';')
                                count++;

                        massX = new double[count];
                        massF = new double[count];

                        for (int i = 0; i < count; i++)
                        {
                            massX[i] = Convert.ToDouble(strX.Split(';')[i]);
                        }
                        if ((reader.GetAttribute("id") == "CorrelationPearson") || (reader.GetAttribute("id") == "CorrelationSpearmansRank"))
                        {
                            for (int i = 0; i < count; i++)
                            {
                                massF[i] = Convert.ToDouble(strF.Split(';')[i]);
                            }
                        }
                        break;
                    }
                }
            }
            reader.Close();
        }
        public void InpuRandomGenerator(string NameTypeMethod, string NameInMethod)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();
            XmlReader reader = XmlReader.Create("SampleData/SaveData.xml");

            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement(NameTypeMethod))
                {
                    if (reader.GetAttribute("id") == NameInMethod)
                    {
                        if (reader.GetAttribute("id") == "Generator2")
                            pointGenerator = Convert.ToDouble(reader.GetAttribute("ParamInput"));

                        if (reader.GetAttribute("id") == "Generator5")
                            pointGenerator = Convert.ToDouble(reader.GetAttribute("ParamInput"));

                        break;
                    }
                }
            }
            reader.Close();
        }
       
        public void SeveTest()
        {
            
            
        }
        public void Save(xmlMethod method)
        {
        //  xmlMethod method
            /*xmlMethod method = new xmlMethod()
            {
                Approximate_Decision = new method()
                {
                    ID = "BisectionMethod",
                    Function = "tan(0.9464 * x) - 1.3825*x",
                    ParamInput1 = "1",
                    ParamInput2 = "1",
                    ParamInput3 = "1",
                    ParamInput4 = "1"
                }
            };*/

        /*    XDocument doc = new XDocument(
                                           new XComment("Numerical Methods Silverlight version"),
                                           new XElement("Numerical_Methods"),
                                                new XElement("Approximate_Decision",
                                                    new XElement("ID", method.Approximate_Decision.ID),
                                                    new XElement("Function", method.Approximate_Decision.Function),
                                                    new XElement("ParamInput1", method.Approximate_Decision.ParamInput1),
                                                    new XElement("ParamInput2", method.Approximate_Decision.ParamInput2),
                                                    new XElement("ParamInput3", method.Approximate_Decision.ParamInput3),
                                                    new XElement("ParamInput4", method.Approximate_Decision.ParamInput4)
                                           )
                                       );*/

            using (IsolatedStorageFile isoStore =
    IsolatedStorageFile.GetUserStoreForApplication())
            {

                // Create new file
                using (IsolatedStorageFileStream isoStream =
                    new IsolatedStorageFileStream("SaveData.xml",
                        FileMode.Create, isoStore))
                {
                    // Write to the Isolated Storage for the user.
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    // Create an XmlWriter.
                    using (XmlWriter writer = XmlWriter.Create(isoStream, settings))
                    {

                        writer.WriteComment("sample XML document");
                        // Write an element (this one is the root).
                        writer.WriteStartElement("Numerical_Methods");

                        // Write the Approximate_Decision.
                        writer.WriteStartElement("Approximate_Decision");
                        writer.WriteAttributeString("id", "BisectionMethod");
                        writer.WriteAttributeString("Function", method.Approximate_Decision.Function);
                        writer.WriteAttributeString("ParamInput1", method.Approximate_Decision.ParamInput1);
                        writer.WriteAttributeString("ParamInput2", method.Approximate_Decision.ParamInput2);
                        writer.WriteAttributeString("ParamInput3", method.Approximate_Decision.ParamInput3);
                        writer.WriteAttributeString("ParamInput4", method.Approximate_Decision.ParamInput4);
                        writer.WriteEndElement();
                       

                        // Write the XML to the file.
                        writer.Flush();
                    }
                }
                // Open the file again for reading.
                using (StreamReader reader =
                                new StreamReader(isoStore.OpenFile("SaveData.xml", FileMode.Open)))
                {
                    resultXML = reader.ReadToEnd();
                }


                // Delete the IsoStoreFile.xml file.
                //isoStore.DeleteFile("IsoStoreFile.xml");
            }
           
        

        }


    }
}
