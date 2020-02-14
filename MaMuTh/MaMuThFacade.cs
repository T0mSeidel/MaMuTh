using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaMuTh.MusicDataObject;
using Mehroz;
using MaMuTh.Math;
using MaMuTh.Music;

namespace MaMuTh
{
	public class MaMuThFacade
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

		//Note passed will be Origin of Euler Coordinates - Default is A - (0,0,0) = Note A
		public void SetEulerPointOriginToSpecificNote( Notes OriginNote )
		{
			foreach( Instrument instrument in Composition.Instruments )
			{
				foreach( Note note in instrument.Notes )
				{
					note.EulerPoint.P -= (int)OriginNote;
				}
			}
		}


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

		//### Methods Information
		public void PrintMusicInformation()
		{
			Console.WriteLine( "Base frequency: " + Composition.BaseFrequency.ToString() );
			Console.WriteLine( "Temperament: " + Composition.Temperament.ToString() );

			for(int i=0; i<Composition.TimeSignatures.Count; i++ )
			{
				Console.WriteLine( "TimeSignature #" + i.ToString() );
				Composition.TimeSignatures[ i ].PrintTimeSignature();
			}

			for( int i = 0; i < Composition.Tempo.Count; i++ )
			{
				Console.WriteLine( "TempoIndication #" + i.ToString() );
				Composition.Tempo[ i ].PrintTempoIndication();
			}

			Console.WriteLine( "Instruments count" + Composition.Instruments.Count );
			for(int i=0; i<Composition.Instruments.Count; i++ )
			{
				Console.WriteLine( "> Instrument #" + i.ToString() );
				Composition.Instruments[ i ].PrintInstrumentInformation();
			}
		}

		public void PrintInformationInFile( String path )
		{
			FileStream ostrm;
			StreamWriter writer;
			TextWriter oldOut = Console.Out;

			try
			{
				ostrm = new FileStream( path +".txt", FileMode.OpenOrCreate, FileAccess.Write );
				writer = new StreamWriter( ostrm );
			}
			catch( Exception e )
			{
				Console.WriteLine( "Cannot open " + path + ".txt for writing" );
				Console.WriteLine( e.Message );
				return;
			}

			Console.SetOut( writer );
			PrintMusicInformation();
			Console.SetOut( oldOut );
			writer.Close();
			ostrm.Close();
		}

		public void PrintTriads()
		{
			foreach(Instrument instrument in Composition.Instruments )
			{
				Console.WriteLine( "#################" );
				Console.WriteLine( "Triads of instrument: " + instrument.Name );
				instrument.PrintTriadInformation();
				Console.WriteLine( "#################" );
			}
		}

		//###Methods Calculating
		public void TransposeComposition(EulerPoint ToAdd )
		{
			foreach(Instrument instrument in Composition.Instruments )
			{
				foreach(Note note in instrument.Notes )
				{
					note.EulerPoint = EulerModule.Add( note.EulerPoint, ToAdd );
				}
			}
		}

		//###Methods Analysing
		public void InitializeTriads()
		{
			foreach(Instrument instrument in Composition.Instruments )
			{
				instrument.InitializeTriads();
			}
		}

		public List<Triad> GetTriadsOfInstrument(TriadType type, int indexOfInstrument)
		{
			if (Composition.Instruments[indexOfInstrument] != null)
				return Composition.Instruments[indexOfInstrument].GetTriads(type);
			else
				throw new NullReferenceException("Instrument doesn't exist");
		}

		public List<Triad> GetTriadsOfInstrument(TriadType type, string nameOfInstrument)
		{
			Instrument instrument;
			try
			{
				instrument = Composition.GetInstrumentByName(nameOfInstrument);
			

				if (instrument != null)
					return instrument.GetTriads(type);
				else
					throw new NullReferenceException("Instrument doesn't exist");
			}
			catch (ArgumentException e)
			{
				Console.WriteLine(e.ToString());
			}
			return null;
		}

		public List<Triad> GetTriadsOfInstrument(TriadType type, string nameOfInstrument, int indexOfInstrument)
		{
			Instrument instrument;
			try
			{
				instrument = Composition.GetInstrumentByName(nameOfInstrument, indexOfInstrument);


				if (instrument != null)
					return instrument.GetTriads(type);
				else
					throw new NullReferenceException("Instrument doesn't exist");
			}
			catch (ArgumentException e)
			{
				Console.WriteLine(e.ToString());
			}
			return null;
		}

        public void FindScales(string scaletyp = "all", string customScale = "none", int top = 20, int percent = 50)
        {          
            for (int i = 0; i < Composition.Instruments.Count; i++)
            {
                List<Note> Notes = Composition.Instruments[i].Notes;

                List<int> notesCounter = CountNotes(Notes, Temperament.Default);

                Dictionary<int, int> noteList = FillNoteList(notesCounter);

                PrintNotesAbsoluteFrequency(noteList);

                List<Scale> scales = InitScales(scaletyp, customScale);
            
                IdentifyKeyAndScale(scales, noteList, notesCounter.Sum(), top, percent);

            }
        }

        private List<int> CountNotes(List<Note> Notes, Temperament temperament)
        {
            if (temperament == Temperament.Default)
            {
                List<int> notesCounter = new List<int>();
                int denominator = 12;
                for (int i = 0; i < denominator; i++)
                {
                    notesCounter.Add(0);
                }

                for (int j = 0; j < Notes.Count; j++)
                {
                    denominator = Convert.ToInt32(Notes[j].EulerPoint.P.Denominator);
                    int ptr = Convert.ToInt32(Notes[j].EulerPoint.P.Numerator % denominator);
                    if (ptr < 0) ptr += denominator;

                    notesCounter[ptr] += 1;
                }
                return notesCounter;
            }
            else // only default temperament supported yet
                return null;
        }

        private Dictionary<int, int> FillNoteList(List<int> notesCounter)
        {
            Dictionary<int, int> noteList = new Dictionary<int, int>();
            for (int j = 0; j < notesCounter.Count; j++)
            {
                if (notesCounter[j] > 0)
                {
                    noteList.Add(j, notesCounter[j]);
                }
            }
            return noteList;
        }

        private void PrintNotesAbsoluteFrequency(Dictionary<int, int> noteList)
        {
            string str = "";
            foreach (var item in noteList.OrderByDescending(i => i.Value))
            {
                str += "[" + Enum.GetName(typeof(Notes), item.Key) + " x " + item.Value.ToString() + "] ";
            }
            Console.WriteLine(str + "\n");
        }

        private List<Scale> InitScales(string scaletyp = "all", string customScale = "none")
        {
            List<Scale> scales = new List<Scale>();

            foreach (var item in Enum.GetValues(typeof(Notes)))
            {
                if (customScale == "none")
                {
                    for (int i = 1; i < Enum.GetValues(typeof(Key)).Length; i++)
                    {
                        Key k = (Key)i;
                        if (scaletyp == "all")
                        {
                            scales.Add(new Scale(Convert.ToInt64(item), k, Temperament.Default));
                        }
                        else
                        {
                            if (k.ToString() == scaletyp)
                                scales.Add(new Scale(Convert.ToInt64(item), k, Temperament.Default));
                        }
                    }
                }
                else
                {
                    scales.Add(new Scale(Convert.ToInt64(item), customScale.ToCharArray(), Temperament.Default));

                }
            }

            return scales;
        }

        public void PrintScalesNotes()
        {
            List<Scale> scales = InitScales();
            foreach (var scale in scales)
            {
                scale.PrintScale();
            }
        }

        public void PrintScalesDistance()
        {
            List<Scale> scales = InitScales();
            for (int i = 1; i < Enum.GetValues(typeof(Key)).Length; i++)
            {
                Key k = (Key)i;

                Scale s = scales.Find(x => x.key == k);
                s.PrintDistance();
            }
        }

        public void IdentifyKeyAndScale(List<Scale> scales, Dictionary<int, int> noteList, int denominator, int top, int percent)
        {
            Dictionary<string, Fraction> scaleList = new Dictionary<string, Fraction>();
            foreach (var item in noteList.OrderByDescending(i => i.Value))
            {
                var note = item.Key;
                var value = item.Value;
                foreach (var scale in scales)
                {
                    if (scale.Contains(note))
                    {
                        if (scaleList.ContainsKey(scale.title))
                        {
                            scaleList[scale.title] += new Fraction(value, denominator);
                        }
                        else
                        {
                            scaleList.Add(scale.title, new Fraction(value, denominator));
                        }
                    }
                }
            }

            if (percent > 0)
                PrintBestKeyAndScale(scaleList, percent);
            else
                PrintTopKeyAndScale(scaleList, top);

        }

        private void PrintTopKeyAndScale(Dictionary<string, Fraction> scaleList, int counter)
        {
            Console.WriteLine("|{0,-22}|{1,11} |{2,4} |", "Key Scale", "hit/total", "%");
            Console.Write(("+").PadRight(23, '-'));
            Console.Write(("+").PadRight(13, '-'));
            Console.Write(("+").PadRight(6, '-'));
            Console.WriteLine("+");
            foreach (var item in scaleList.OrderByDescending(i => i.Value.Numerator))
            {
                if (counter == 0) break;
                long value = item.Value.Numerator * 100 / item.Value.Denominator;
                Console.WriteLine("| {0,-20} | {1,10} | {2,2}% |", item.Key, item.Value, value);
                counter--;
            }
        }

        private void PrintBestKeyAndScale(Dictionary<string, Fraction> scaleList, int percent)
        {
            Console.WriteLine("|{0,-22}|{1,11} |{2,4} |", "Key Scale", "hit/total", "%");
            Console.Write(("+").PadRight(23, '-'));
            Console.Write(("+").PadRight(13, '-'));
            Console.Write(("+").PadRight(6, '-'));
            Console.WriteLine("+");
            foreach (var item in scaleList.OrderByDescending(i => i.Value.Numerator))
            {
                long value = item.Value.Numerator * 100 / item.Value.Denominator;
                if (value >= percent) { }
                    Console.WriteLine("| {0,-20} | {1,10} | {2,2}% |", item.Key, item.Value, value);
            }
        }

 

        public void PrintsBars()
        {
            List<Note> Notes = Composition.Instruments[0].Notes;
            TimeSignature ts = Composition.TimeSignatures[0];

            Dictionary<Note, int> dic = new Dictionary<Note,int>();

            int bar = 1;
            foreach (var note in Notes)
            {

                Fraction fr = note.Onset + note.Duration;
                double div = (fr / ts.Fraction).ToDouble();

                if (div > bar)
                {
                    bar += 1;
                }
                dic.Add(note,bar);
            }

            for (int i = dic.Values.First(); i <= dic.Values.Last(); i++)
            {
                List<Note> n = new List<Note>();
                Console.WriteLine("#### Takt #" + i);
                foreach (var item in dic.ToList().Where(x => x.Value == i))
                {
                    n.Add(item.Key);
                }
                List<int> notesCounter = CountNotes(n, Temperament.Default);
                Dictionary<int, int> noteList = FillNoteList(notesCounter);
                PrintNotesAbsoluteFrequency(noteList);
                
            }
        }

    }
}
