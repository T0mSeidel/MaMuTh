using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh.MusicDataObject
{
	public class TimeSignatureMDO
	{
		public Fraction Fraction { get; set; }
		public Fraction ValidAt { get; set; } //Gültig ab 

		public TimeSignatureMDO(int numerator, int denominator, int validAtNumerator, int validAtDenominator )
		{
			Fraction = new Fraction( numerator, denominator );
			ValidAt = new Fraction( validAtNumerator, validAtDenominator );
		}

		public TimeSignatureMDO( int numerator, int denominator, Fraction validAt )
		{
			Fraction = new Fraction( numerator, denominator );
			ValidAt = new Fraction( validAt.Numerator, validAt.Denominator );
		}
	}
}
