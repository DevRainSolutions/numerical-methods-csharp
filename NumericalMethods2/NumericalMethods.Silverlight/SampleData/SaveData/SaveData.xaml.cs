﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.SaveData
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class Numerical_Methods { }
#else

	public class Numerical_Methods : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public Numerical_Methods()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/NumericalMethods-Silverlight;component/SampleData/SaveData/SaveData.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private Approximate_DecisionCollection _Approximate_DecisionCollection = new Approximate_DecisionCollection();

		public Approximate_DecisionCollection Approximate_DecisionCollection
		{
			get
			{
				return this._Approximate_DecisionCollection;
			}
		}

		private Differential_EquationsCollection _Differential_EquationsCollection = new Differential_EquationsCollection();

		public Differential_EquationsCollection Differential_EquationsCollection
		{
			get
			{
				return this._Differential_EquationsCollection;
			}
		}

		private IntegrationCollection _IntegrationCollection = new IntegrationCollection();

		public IntegrationCollection IntegrationCollection
		{
			get
			{
				return this._IntegrationCollection;
			}
		}

		private NonLinearEqualizationCollection _NonLinearEqualizationCollection = new NonLinearEqualizationCollection();

		public NonLinearEqualizationCollection NonLinearEqualizationCollection
		{
			get
			{
				return this._NonLinearEqualizationCollection;
			}
		}

		private OptimizingCollection _OptimizingCollection = new OptimizingCollection();

		public OptimizingCollection OptimizingCollection
		{
			get
			{
				return this._OptimizingCollection;
			}
		}

		private InterpolationCollection _InterpolationCollection = new InterpolationCollection();

		public InterpolationCollection InterpolationCollection
		{
			get
			{
				return this._InterpolationCollection;
			}
		}

		private StatisticsCollection _StatisticsCollection = new StatisticsCollection();

		public StatisticsCollection StatisticsCollection
		{
			get
			{
				return this._StatisticsCollection;
			}
		}

		private RandomGeneratorCollection _RandomGeneratorCollection = new RandomGeneratorCollection();

		public RandomGeneratorCollection RandomGeneratorCollection
		{
			get
			{
				return this._RandomGeneratorCollection;
			}
		}

		private MatrixAlgebraCollection _MatrixAlgebraCollection = new MatrixAlgebraCollection();

		public MatrixAlgebraCollection MatrixAlgebraCollection
		{
			get
			{
				return this._MatrixAlgebraCollection;
			}
		}

		private LinearSystemsCollection _LinearSystemsCollection = new LinearSystemsCollection();

		public LinearSystemsCollection LinearSystemsCollection
		{
			get
			{
				return this._LinearSystemsCollection;
			}
		}
	}

	public class Approximate_Decision : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private double _ParamInput2 = 0;

		public double ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private double _ParamInput3 = 0;

		public double ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private double _ParamInput4 = 0;

		public double ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class Differential_Equations : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private double _ParamInput2 = 0;

		public double ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private double _ParamInput3 = 0;

		public double ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private double _ParamInput4 = 0;

		public double ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class Integration : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private double _ParamInput2 = 0;

		public double ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private double _ParamInput3 = 0;

		public double ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private string _ParamInput4 = string.Empty;

		public string ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class NonLinearEqualization : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private double _ParamInput2 = 0;

		public double ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private double _ParamInput3 = 0;

		public double ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private string _ParamInput4 = string.Empty;

		public string ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class Optimizing : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private double _ParamInput2 = 0;

		public double ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private double _ParamInput3 = 0;

		public double ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private double _ParamInput4 = 0;

		public double ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class Interpolation : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private string _ParamInput2 = string.Empty;

		public string ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private string _ParamInput3 = string.Empty;

		public string ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private string _ParamInput4 = string.Empty;

		public string ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class Statistics : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private string _ParamInput1 = string.Empty;

		public string ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private string _ParamInput2 = string.Empty;

		public string ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private string _ParamInput3 = string.Empty;

		public string ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private string _ParamInput4 = string.Empty;

		public string ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class RandomGenerator : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private string _Function = string.Empty;

		public string Function
		{
			get
			{
				return this._Function;
			}

			set
			{
				if (this._Function != value)
				{
					this._Function = value;
					this.OnPropertyChanged("Function");
				}
			}
		}

		private double _ParamInput1 = 0;

		public double ParamInput1
		{
			get
			{
				return this._ParamInput1;
			}

			set
			{
				if (this._ParamInput1 != value)
				{
					this._ParamInput1 = value;
					this.OnPropertyChanged("ParamInput1");
				}
			}
		}

		private string _ParamInput2 = string.Empty;

		public string ParamInput2
		{
			get
			{
				return this._ParamInput2;
			}

			set
			{
				if (this._ParamInput2 != value)
				{
					this._ParamInput2 = value;
					this.OnPropertyChanged("ParamInput2");
				}
			}
		}

		private string _ParamInput3 = string.Empty;

		public string ParamInput3
		{
			get
			{
				return this._ParamInput3;
			}

			set
			{
				if (this._ParamInput3 != value)
				{
					this._ParamInput3 = value;
					this.OnPropertyChanged("ParamInput3");
				}
			}
		}

		private string _ParamInput4 = string.Empty;

		public string ParamInput4
		{
			get
			{
				return this._ParamInput4;
			}

			set
			{
				if (this._ParamInput4 != value)
				{
					this._ParamInput4 = value;
					this.OnPropertyChanged("ParamInput4");
				}
			}
		}
	}

	public class MatrixAlgebra : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private double _Range = 0;

		public double Range
		{
			get
			{
				return this._Range;
			}

			set
			{
				if (this._Range != value)
				{
					this._Range = value;
					this.OnPropertyChanged("Range");
				}
			}
		}

		private string _Matrix = string.Empty;

		public string Matrix
		{
			get
			{
				return this._Matrix;
			}

			set
			{
				if (this._Matrix != value)
				{
					this._Matrix = value;
					this.OnPropertyChanged("Matrix");
				}
			}
		}
	}

	public class LinearSystems : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _id = string.Empty;

		public string id
		{
			get
			{
				return this._id;
			}

			set
			{
				if (this._id != value)
				{
					this._id = value;
					this.OnPropertyChanged("id");
				}
			}
		}

		private double _Range = 0;

		public double Range
		{
			get
			{
				return this._Range;
			}

			set
			{
				if (this._Range != value)
				{
					this._Range = value;
					this.OnPropertyChanged("Range");
				}
			}
		}

		private string _LinSysMasA = string.Empty;

		public string LinSysMasA
		{
			get
			{
				return this._LinSysMasA;
			}

			set
			{
				if (this._LinSysMasA != value)
				{
					this._LinSysMasA = value;
					this.OnPropertyChanged("LinSysMasA");
				}
			}
		}

		private string _LinSysMatrixB = string.Empty;

		public string LinSysMatrixB
		{
			get
			{
				return this._LinSysMatrixB;
			}

			set
			{
				if (this._LinSysMatrixB != value)
				{
					this._LinSysMatrixB = value;
					this.OnPropertyChanged("LinSysMatrixB");
				}
			}
		}
	}

	public class Approximate_DecisionCollection : System.Collections.ObjectModel.ObservableCollection<Approximate_Decision>
	{ 
	}

	public class Differential_EquationsCollection : System.Collections.ObjectModel.ObservableCollection<Differential_Equations>
	{ 
	}

	public class IntegrationCollection : System.Collections.ObjectModel.ObservableCollection<Integration>
	{ 
	}

	public class NonLinearEqualizationCollection : System.Collections.ObjectModel.ObservableCollection<NonLinearEqualization>
	{ 
	}

	public class OptimizingCollection : System.Collections.ObjectModel.ObservableCollection<Optimizing>
	{ 
	}

	public class InterpolationCollection : System.Collections.ObjectModel.ObservableCollection<Interpolation>
	{ 
	}

	public class StatisticsCollection : System.Collections.ObjectModel.ObservableCollection<Statistics>
	{ 
	}

	public class RandomGeneratorCollection : System.Collections.ObjectModel.ObservableCollection<RandomGenerator>
	{ 
	}

	public class MatrixAlgebraCollection : System.Collections.ObjectModel.ObservableCollection<MatrixAlgebra>
	{ 
	}

	public class LinearSystemsCollection : System.Collections.ObjectModel.ObservableCollection<LinearSystems>
	{ 
	}
#endif
}
