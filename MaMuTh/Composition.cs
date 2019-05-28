using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	class Composition
	{
		public int Tempo;
		public int BaseFrequency; //oder float?
		public Temperament Temperament;

		public int[] Multiplicator; // 2^p+*3^s*5^r*n^q
		public List<Instrument> Instruments;

		public Composition(int tempo, int baseFrequency, Temperament temperament, 
			int[] multiplicator, List<Instrument> instruments)
		{
			Tempo = tempo;
			BaseFrequency = baseFrequency;
			Temperament = temperament;
			Multiplicator = multiplicator;
			Instruments = instruments;
		}
	}
}
