using System;

namespace NumberGenerator.Logic
{
	/// <summary>
	/// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
	/// </summary>
	public class StatisticsObserver : BaseObserver
	{
		#region Fields
		private int _counter;
		#endregion

		#region Properties

		/// <summary>
		/// Enthält das Minimum der generierten Zahlen.
		/// </summary>
		public int Min { get; private set; } = int.MaxValue;

		/// <summary>
		/// Enthält das Maximum der generierten Zahlen.
		/// </summary>
		public int Max { get; private set; } = int.MinValue;

		/// <summary>
		/// Enthält die Summe der generierten Zahlen.
		/// </summary>
		public int Sum { get; private set; } = 0;

		/// <summary>
		/// Enthält den Durchschnitt der generierten Zahlen.
		/// </summary>
		public int Avg { get; set; }
		public int Counter { get => _counter; set => _counter = value; }

		#endregion

		#region Constructors

		public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
		{
			if(countOfNumbersToWaitFor < 0)
			{
				throw new ArgumentException("");
			}
			Counter = countOfNumbersToWaitFor;
		}

		#endregion

		#region Methods

		public override void OnNextNumber(int number)
		{
			if (number > Max)
			{
				Max = number;
			}
			if (number < Min)
			{
				Min = number;
			}
			Sum += number;
			Avg = Sum / CountOfNumbersToWaitFor;
			if(Counter-- == 0)
			{
				_numberGenerator.NextNumberHandlerChanged -= this.OnNextNumber;
			}
			base.OnNextNumber(number);
		}
		public override string ToString()
		{
			return $"BaseObserver [CountOfNumbersReceived='{CountOfNumbersReceived}', CountOfNumbersToWaitFor='{CountOfNumbersToWaitFor}'] => {this.GetType().Name} [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']";
		}

		#endregion
	}
}
