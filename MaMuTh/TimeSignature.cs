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
		public double ValidAt { get; set; } //Gültig ab 

		public TimeSignature( int numerator, int denominator, double validAt )
		{
			Fraction = new Fraction( numerator, denominator );
			ValidAt = validAt;
		}

		public TimeSignature( Fraction fraction, double validAt )
		{
			Fraction = new Fraction( fraction.Numerator, fraction.Denominator );
			ValidAt = validAt;
		}
	}
}
