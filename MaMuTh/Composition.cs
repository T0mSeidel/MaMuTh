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

		public Instrument GetInstrumentByName(string nameOfInstrument)
		{
			IEnumerable<Instrument> instrumentsGotByName = Instruments.Where(instrument => instrument.Name.Equals(nameOfInstrument));
			int numberOfInstruments = instrumentsGotByName.Count();
			if (numberOfInstruments == 1)
			{
				return instrumentsGotByName.First();

			}else if(numberOfInstruments == 0)
			{
				return null;
			}
			else
			{
				throw new ArgumentException("More than one Instrument found with the same name. Please pass an index ");
			}
		}
		public Instrument GetInstrumentByName(string nameOfInstrument, int indexOfInstrument)
		{
			List<Instrument> instrumentsGotByName = Instruments.Where(instrument => instrument.Name.Equals(nameOfInstrument)).ToList();
			int numberOfInstruments = instrumentsGotByName.Count();
			if (numberOfInstruments == 1)
			{
				return instrumentsGotByName.First();

			}
			else if (numberOfInstruments == 0)
			{
				throw null;
			}
			else
			{
				return instrumentsGotByName[indexOfInstrument];
			}
		}
	}
}
