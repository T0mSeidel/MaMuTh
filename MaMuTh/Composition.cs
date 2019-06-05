using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	class Composition
	{
		private int[] Multiplicator;

		public List<TempoIndication> Tempo;
		public List<TimeSignature> TimeSignatures;
		public int BaseFrequency;
		public Temperament Temperament;
		public List<Instrument> Instruments;

		public Composition(Temperament temperament, int baseFrequency, List<TempoIndication> tempi, 
			List<TimeSignature> timeSignatures, List<Instrument> instruments)
		{
			Tempo = tempi;
			TimeSignatures = timeSignatures;
			Instruments = instruments;
			BaseFrequency = baseFrequency;
			Temperament = temperament;
		}
	}
}
