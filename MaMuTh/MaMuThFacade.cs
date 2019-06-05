using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaMuTh.MusicDataObject;

namespace MaMuTh
{
	class MaMuThFacade
	{
		private Composition Composition;

		//### Settings
		private int[] Multiplicator;

		//### Constructor
		public MaMuThFacade()
		{
			Composition = null;
			Multiplicator = new int[] { 2, 3, 5 };
		}

		//### Methods
		public void CreateComposition(MusicDataObject.MusicDataObject musicDataObject)
		{
			Temperament temperament = GetTemperament( musicDataObject.Temperament );
			List<TempoIndication> tempi = GetTempi( musicDataObject.TempoIndications );
			List<TimeSignature> timeSignatures = GetTimeSignatures( musicDataObject.TimeSignatures );
			List<Instrument> instruments = GetInstrumentsAndNotes( musicDataObject );
			int baseFrequency = musicDataObject.BaseFrequency;

			Composition = new Composition(temperament, baseFrequency, tempi, timeSignatures, instruments);
		}

		//## Methods - Setting
		//## Methods - Help
		private List<Instrument> GetInstrumentsAndNotes( MusicDataObject.MusicDataObject musicDataObject )
		{
			List<Instrument> instruments = new List<Instrument>();

			foreach(Channel channel in musicDataObject.Channels )
			{
				List<Note> notes = new List<Note>();
				notes = GetNotes( channel.Notes );
				Instrument instrument = new Instrument( channel.Name, notes );
				instruments.Add( instrument );
			}
			return instruments;
		}

		private List<Note> GetNotes( List<NoteData> notesMDO )
		{
			List<Note> Notes = new List<Note>();
			foreach(NoteData noteMDO in notesMDO )
			{
				Note note = new Note( noteMDO.Dynamics, noteMDO.Duration, noteMDO.Onset,
					noteMDO.Exponents );
				Notes.Add( note );
			}
			return Notes;
		}

		private List<TimeSignature> GetTimeSignatures( List<TimeSignatureMDO> timeSignaturesMDO )
		{
			List<TimeSignature> timeSignatures = new List<TimeSignature>();

			foreach(TimeSignatureMDO timeSignatureMDO in timeSignaturesMDO )
			{
				TimeSignature timeSignature = new TimeSignature
					( timeSignatureMDO.Fraction, timeSignatureMDO.ValidAt );
				timeSignatures.Add( timeSignature );
			}

			return timeSignatures;
		}

		private List<TempoIndication> GetTempi( List<TempoIndicationMDO> tempoIndicationsMDO )
		{
			List<TempoIndication> tempi = new List<TempoIndication>();

			foreach(TempoIndicationMDO tempoMDO in tempoIndicationsMDO )
			{
				TempoIndication tempoIndication = new TempoIndication( tempoMDO.Tempo, tempoMDO.ValidAt );
				tempi.Add( tempoIndication );
			}

			return tempi;
		}

		private Temperament GetTemperament( TemperamentDataObject temperament )
		{
			if( temperament == MusicDataObject.TemperamentDataObject.EqualTemperament )
				return Temperament.EqualTemperament;
			if( temperament == MusicDataObject.TemperamentDataObject.JustIntonation )
				return Temperament.JustIntonation;
			if( temperament == MusicDataObject.TemperamentDataObject.PythagoreanTuning )
				return Temperament.PythagoreanTuning;
			if( temperament == MusicDataObject.TemperamentDataObject.WellTemperament )
				return Temperament.WellTemperament;

			return Temperament.Default;
		}
	}
}
