using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh.MusicDataObject
{
	class TempoIndicationMDO
	{
		public int Tempo { get; set; }
		public Fraction ValidAt { get; set; }
		
		public TempoIndicationMDO(int tempo, int validAtNumerator, int validAtDenominator )
		{
			Tempo = tempo;
			ValidAt = new Fraction( validAtNumerator, validAtDenominator);
		}

		public TempoIndicationMDO( int tempo, Fraction validAt )
		{
			Tempo = tempo;
			ValidAt = new Fraction( validAt.Numerator, validAt.Denominator );
		}
	}
}
