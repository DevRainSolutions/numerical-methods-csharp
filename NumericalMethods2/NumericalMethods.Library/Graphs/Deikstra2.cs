using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.NumericalMethods
{
    /// Not tested!!!!!!!!!!
    /// see http://ivanshik.habrahabr.ru/blog/63347/

    /// <summary>
    /// Класс, реализующий ребро
    /// </summary>
    public class Rebro
    {
        public APoint FirstPoint { get; set; }
        public APoint SecondPoint { get; set; }
        public float Value { get; set; }

        public Rebro(APoint first, APoint second, float value)
        {
            FirstPoint = first;
            SecondPoint = second;
            Value = value;
        }

    }

    /// <summary>
    /// Класс, реализующий вершину графа
    /// </summary>
    public class APoint
    {
        public float ValueMetka { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public APoint predPoint { get; set; }
        public object SomeObj { get; set; }
        public APoint(int value, bool ischecked)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            predPoint = new APoint();
        }
        public APoint(int value, bool ischecked, string name)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            Name = name;
            predPoint = new APoint();
        }
        public APoint()
        {
        }
        public string printnamePred()
        {
            string s = "predki=";
            return s;
        }

    }

    public class Deikstra2
    {
        public APoint[] points { get; set; }
        public Rebro[] rebra { get; set; }

        /// <summary>
        /// Запуск алгоритма расчета
        /// </summary>
        /// <param name="beginp"></param>
        public void AlgoritmRun(APoint beginp)
        {
            OneStep(beginp);
            foreach (APoint point in points)
            {
                APoint anotherP = GetAnotherUncheckedPoint();
                if (anotherP != null)
                {
                    OneStep(anotherP);
                }
                else
                {
                    break;
                }

            }

        }
        /// <summary>
        /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
        /// </summary>
        /// <param name="beginpoint"></param>
        public void OneStep(APoint beginpoint)
        {
            foreach (APoint nextp in Pred(beginpoint))
            {
                if (nextp.IsChecked == false)//не отмечена
                {
                    float newmetka = beginpoint.ValueMetka + GetMyRebro(nextp, beginpoint).Value;
                    if (nextp.ValueMetka > newmetka)
                    {
                        nextp.ValueMetka = newmetka;
                        nextp.predPoint = beginpoint;
                    }
                    else
                    {

                    }
                }
            }
            beginpoint.IsChecked = true;//вычеркиваем
        }
        /// <summary>
        /// Поиск соседей для вершины. Для неориентированного графа ищутся все соседи.
        /// </summary>
        /// <param name="currpoint"></param>
        /// <returns></returns>
        private IEnumerable<APoint> Pred(APoint currpoint)
        {
            IEnumerable<APoint> firstpoints = from ff in rebra where ff.FirstPoint == currpoint select ff.SecondPoint;
            IEnumerable<APoint> secondpoints = from sp in rebra where sp.SecondPoint == currpoint select sp.FirstPoint;
            IEnumerable<APoint> totalpoints = firstpoints.Concat<APoint>(secondpoints);
            return totalpoints;
        }
        /// <summary>
        /// Получаем ребро, соединяющее 2 входные точки
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Rebro GetMyRebro(APoint a, APoint b)
        {//ищем ребро по 2 точкам
            IEnumerable<Rebro> myr = from reb in rebra where (reb.FirstPoint == a & reb.SecondPoint == b) || (reb.SecondPoint == a & reb.FirstPoint == b) select reb;
            if (myr.Count() > 1 || myr.Count() == 0)
            {
                throw new Exception("rebra...!");
            }
            else
            {
                return myr.First();
            }
        }
        /// <summary>
        /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
        /// </summary>
        /// <returns></returns>
        private APoint GetAnotherUncheckedPoint()
        {
            IEnumerable<APoint> pointsuncheck = from p in points where p.IsChecked == false select p;
            if (pointsuncheck.Count() != 0)
            {
                float minVal = pointsuncheck.First().ValueMetka;
                APoint minPoint = pointsuncheck.First();
                foreach (APoint p in pointsuncheck)
                {
                    if (p.ValueMetka < minVal)
                    {
                        minVal = p.ValueMetka;
                        minPoint = p;
                    }
                }
                return minPoint;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Метод для "печати" вершин:имя,значение метки,предок
        /// </summary>
        /// <returns></returns>
        public List<string> Print()
        {
            List<string> prlist = new List<string>();
            foreach (APoint ap in points)
            {
                prlist.Add("name=" + ap.Name + "val=" + ap.ValueMetka.ToString() + "predok=" + ap.predPoint.Name);
            }
            return prlist;

        }
        /// <summary>
        /// Печать кратчайшего пути до заданной точки
        /// </summary>
        /// <param name="initp"></param>
        /// <returns></returns>
        public List<string> PrintPaths(APoint initp)
        {
            List<string> prlist = new List<string>();
            foreach (APoint ap in points)
            {
                prlist.Add("От=" + ap.Name + "До=" + initp.Name + "Путь=" + MinPath(initp, ap));
            }
            return prlist;

        }
        /// <summary>
        /// Печать минимального пути от начальной точки до некой входной
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string MinPath(APoint begin, APoint end)
        {
            string s = string.Empty;

            APoint tempp = new APoint();
            tempp = end;
            while (tempp != begin)
            {
                s += " " + tempp.Name;
                tempp = GetPred(tempp);
            }
            s += " " + tempp.Name;
            return s;
        }
        /// <summary>
        /// Получаем кратчайший путь в виде коллекции точек
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<APoint> MinPath1(APoint begin, APoint end)
        {
            string s = string.Empty;
            List<APoint> listOfpoints = new List<APoint>();
            APoint tempp = new APoint();
            tempp = end;
            while (tempp != begin)
            {
                listOfpoints.Add(tempp);
                tempp = GetPred(tempp);
            }

            return listOfpoints;
        }
        /// <summary>
        /// Получаем предка для вершины
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private APoint GetPred(APoint a)
        {
            return points.Where(a1 => a1 == a).First().predPoint;

        }

    }
}
