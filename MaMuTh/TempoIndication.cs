using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh
{
	class TempoIndication
	{ 
		public int Tempo;
		public Fraction ValidAt;

		public TempoIndication( int tempo, int validAtNumerator, int validAtDenominator )
		{
			Tempo = tempo;
			ValidAt = new Fraction( validAtNumerator, validAtDenominator );
		}

		public TempoIndication( int tempo, Fraction validAt )
		{
			Tempo = tempo;
			ValidAt = new Fraction(validAt.Numerator, validAt.Denominator);
		}	
	}
}
