using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerator.Logic
{
	/// <summary>
	/// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
	/// </summary>
	public class QuickTippObserver : IObserver
	{
		#region Fields

		private readonly IObservable _numberGenerator;

		#endregion

		#region Properties

		public List<int> QuickTippNumbers { get; private set; } = new List<int>();
		public int CountOfNumbersReceived { get; private set; } = 0;

		#endregion

		#region Constructor

		public QuickTippObserver(IObservable numberGenerator)
		{
			_numberGenerator = numberGenerator ?? throw new ArgumentNullException($"{nameof(numberGenerator)} war Null");
			_numberGenerator.NextNumberHandlerChanged += this.OnNextNumber;
		}

		#endregion

		#region Methods

		public void OnNextNumber(int number)
		{
			if (!QuickTippNumbers.Contains(number) && number > 0 && number < 46)
			{
				QuickTippNumbers.Add(number);
			}
			CountOfNumbersReceived++;
			if(QuickTippNumbers.Count == 6)
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"{this.GetType().Name}: Received {CountOfNumbersReceived:d5} numbers ===> Quick-Typ is {ToString()}");
				Console.ResetColor();
				Console.WriteLine();
				DetachFromNumberGenerator();
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int item in QuickTippNumbers)
			{
				stringBuilder.Append(item);
				stringBuilder.Append(", ");
			}
			return stringBuilder.ToString();
		}

		private void DetachFromNumberGenerator()
		{
			_numberGenerator.NextNumberHandlerChanged -= this.OnNextNumber;
		}

		#endregion
	}
}
