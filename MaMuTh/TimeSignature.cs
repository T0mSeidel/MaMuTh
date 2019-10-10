using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh
{
	class TimeSignature
	{
		public Fraction Fraction { get; set; }
		public Fraction ValidAt { get; set; } //Gültig ab 

		public TimeSignature( int numerator, int denominator, int validAtNumerator, int validAtDenominator )
		{
			Fraction = new Fraction( numerator, denominator );
			ValidAt = new Fraction( validAtNumerator, validAtDenominator);
		}

		public TimeSignature( Fraction fraction, Fraction validAt )
		{
			Fraction = new Fraction( fraction.Numerator, fraction.Denominator );
			ValidAt = new Fraction( validAt.Numerator, validAt.Denominator);
		}

		//### Methods

		public void PrintTimeSignature()
		{
			Console.WriteLine( "> " + Fraction.ToString() + " - Valid at " + ValidAt.ToString() );
		}
	}
}
