using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerator.Logic
{
	/// <summary>
	/// Beobachter, welcher die Anzahl der generierten Zahlen in einem bestimmten Bereich zählt. 
	/// </summary>
	public class RangeObserver : BaseObserver
	{
		#region Properties

		/// <summary>
		/// Enthält die untere Schranke (inkl.)
		/// </summary>
		public int LowerRange { get; private set; }

		/// <summary>
		/// Enthält die obere Schranke (inkl.)
		/// </summary>
		public int UpperRange { get; private set; }

		/// <summary>
		/// Enthält die Anzahl der Zahlen, welche sich im Bereich befinden.
		/// </summary>
		public int NumbersInRange { get; private set; }

		/// <summary>
		/// Enthält die Anzahl der gesuchten Zahlen im Bereich.
		/// </summary>
		public int NumbersOfHitsToWaitFor { get; private set; }

		#endregion

		#region Constructors

		public RangeObserver(IObservable numberGenerator, int numberOfHitsToWaitFor, int lowerRange, int upperRange) : base(numberGenerator, int.MaxValue)
		{
			if (numberOfHitsToWaitFor < 0)
			{
				throw new ArgumentException($"{nameof(numberOfHitsToWaitFor)} war negativ");
			}
			if (lowerRange <= upperRange)
			{
				LowerRange = lowerRange;
				UpperRange = upperRange;
				NumbersOfHitsToWaitFor = numberOfHitsToWaitFor;
			}
			else
			{
				throw new ArgumentException($"Untere Schranke ist groesser als die obere Schranke");
			}
		}
		#endregion

		#region Methods

		public override string ToString()
		{
			return $"{base.ToString()}: Number is in range ({LowerRange}-{UpperRange}): ";
		}

		public override void OnNextNumber(int number)
		{
			if (number >= LowerRange && number <= UpperRange)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"\t\t>>{ToString()}---> {number}");
				Console.ResetColor();
				NumbersInRange++;
				NumbersOfHitsToWaitFor--;
				if(NumbersOfHitsToWaitFor == 0)
				{
					_numberGenerator.NextNumberHandlerChanged -= this.OnNextNumber;
				}
			}
			base.OnNextNumber(number);
		}
		#endregion
	}
}
