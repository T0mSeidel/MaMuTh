using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaMuTh.Music;
using MaMuTh.Math;

namespace MaMuTh
{
	public class Instrument
	{
		public string Name { get; set; }
		public List<Note> Notes { get; set; }

		public List<Triad> Triads { get; private set; }

		public Instrument(string name, List<Note> notes)
		{
			Name = name;
			Notes = notes;
			
			foreach(Note note in Notes )
			{
				note.SetInstrument( this );
			}

			Triads = new List<Triad>();
		}

		//###Methods
		//##Information

		public void PrintInstrumentInformation()
		{
			Console.WriteLine( "> Name: " + Name );
			Console.WriteLine( " Note count: " + Notes.Count );
			for(int i=0; i<Notes.Count; i++ )
			{
				Console.WriteLine( "> Note #" + i.ToString() );
				Notes[ i ].PrintNoteInformation();
			}
		}

		public void PrintTriadInformation()
		{
			for(int i=0; i< Triads.Count; i++ )
			{
				Console.WriteLine( "Triad #" + i.ToString() );
				Triads[ i ].PrintTriadInformation();
			}
		}

		//##Analysing

		private void SearchForTriads() //TODO Verbessern damit garantiert nichts geskippt wird
		{
			IEnumerable<Note> notesWithSameOnset;
			List<Note> notesWithSameOnsetList;
			Note previousNote;

			if( Triads.Count > 0 ) //Reset member list of triads
			{
				Triads.Clear(); 
			}

			for(int i=0; i< Notes.Count; i++ )
			{
				Note note = Notes[ i ];
				notesWithSameOnset = Notes.Where( comparedNote => comparedNote.Onset == note.Onset ); //Filtere Noten mit gleicher Einsatzzeit
				if( notesWithSameOnset.Count() == 3 ) //Wenn 3 Noten zwischenspeichern
				{
					notesWithSameOnsetList = notesWithSameOnset.ToList();
					Triad triad = new Triad( notesWithSameOnsetList[ 0 ], notesWithSameOnsetList[ 1 ], notesWithSameOnsetList[ 2 ] );
					Triads.Add( triad );

					if(i+2 < Notes.Count ) //skip next two notes, cause they are part of the triad
					{
						i += 2; //skip two notes of triad
					}
					else
					{
						break;
					}
				}
			}
		}

		public void InitializeTriads()
		{
			SearchForTriads(); //all "triads" with same onset
			ClassifiyTriadsZ12(); //setze die Klasse des Dreiklangs
		}

		public List<Triad> GetTriads( TriadType type )
		{
			if( Triads.Count <= 0 )
			{
				Console.WriteLine( "No triads found. You may first use the method 'InitializeTriads' and then repeat this method-call." );
				Console.WriteLine( "If still nothing is shown, the analyzed music doesn't contain any triads" );
			}

			if(type == TriadType.AllTriads)
			{
				return Triads;
			}

			if( type == TriadType.AllRealTriads )
			{
				return Triads.Where( t => t.triadType != TriadType.NoTriad ).ToList();
			}

			return Triads.Where( t => t.triadType == type ).ToList();
		}

		private void ClassifiyTriadsZ12() //in einzelne Methoden für major, minor, ... und dann gleich Umkehrungen checken
		{
			foreach(Triad triad in Triads )
			{
				triad.triadType = TriadType.NoTriad; //Reset TriadType
				ClassifyMajorTriadsZ12( triad );
				ClassifyMinorTriadsZ12( triad );
				ClassifyAugmentedTriadsZ12( triad );
				ClassifyDiminishedTriadsZ12( triad );
			}
		}

		private void ClassifyDiminishedTriadsZ12( Triad triad )
		{
			long firstNotePitchClass = MusicHelper.ReduceToPitchClass( triad.FirstNote.EulerPoint.P.Numerator, 12 );
			long secondNotePitchClass = MusicHelper.ReduceToPitchClass( triad.SecondNote.EulerPoint.P.Numerator, 12 );
			long thirdNotePitchClass = MusicHelper.ReduceToPitchClass( triad.ThirdNote.EulerPoint.P.Numerator, 12 );

			if( triad.triadType == TriadType.NoTriad ) //Abbrechen wenn Dreiklangtyp gefunden wurde
			{
				long lowerInterval = secondNotePitchClass - firstNotePitchClass;
				lowerInterval = MusicHelper.ReduceToPitchClass( lowerInterval, 12 );
				long upperInterval = thirdNotePitchClass - secondNotePitchClass;
				upperInterval = MusicHelper.ReduceToPitchClass( upperInterval, 12 );

				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorThird, lowerInterval, MusicHelper.MinorThird, upperInterval, TriadType.DiminishedTriad, TriadInversion.NoInversion, FundamentalTone.Default, triad );
				//CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorThird, lowerInterval, MusicHelper.MinorThird, upperInterval, TriadType.AugmentedTriad, TriadInversion.FirstInversion, triad );
				//CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorThird, lowerInterval, MusicHelper.MinorThird, upperInterval, TriadType.AugmentedTriad, TriadInversion.SecondInversion, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorSixth, lowerInterval, MusicHelper.Tritone, upperInterval, TriadType.DiminishedTriad, TriadInversion.NoInversion, FundamentalTone.Default, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.Tritone, lowerInterval, MusicHelper.MajorSixth, upperInterval, TriadType.DiminishedTriad, TriadInversion.NoInversion, FundamentalTone.Default, triad );
			}
			else
			{
				return;
			}
		}

		private void ClassifyAugmentedTriadsZ12( Triad triad )
		{
			long firstNotePitchClass = MusicHelper.ReduceToPitchClass( triad.FirstNote.EulerPoint.P.Numerator, 12 );
			long secondNotePitchClass = MusicHelper.ReduceToPitchClass( triad.SecondNote.EulerPoint.P.Numerator, 12 );
			long thirdNotePitchClass = MusicHelper.ReduceToPitchClass( triad.ThirdNote.EulerPoint.P.Numerator, 12 );

			if( triad.triadType == TriadType.NoTriad ) //Abbrechen wenn Dreiklangtyp gefunden wurde
			{
				long lowerInterval = secondNotePitchClass - firstNotePitchClass;
				lowerInterval = MusicHelper.ReduceToPitchClass( lowerInterval, 12 );
				long upperInterval = thirdNotePitchClass - secondNotePitchClass;
				upperInterval = MusicHelper.ReduceToPitchClass( upperInterval, 12 );

				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorThird, lowerInterval, MusicHelper.MajorThird, upperInterval, TriadType.AugmentedTriad, TriadInversion.NoInversion, FundamentalTone.Default, triad );
				//CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorThird, lowerInterval, MusicHelper.MajorThird, upperInterval, TriadType.AugmentedTriad, TriadInversion.FirstInversion, triad );
				//CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorThird, lowerInterval, MusicHelper.MajorThird, upperInterval, TriadType.AugmentedTriad, TriadInversion.SecondInversion, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorSixth, lowerInterval, MusicHelper.MajorThird, upperInterval, TriadType.AugmentedTriad, TriadInversion.NoInversion, FundamentalTone.Default, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorSixth, lowerInterval, MusicHelper.MinorSixth, upperInterval, TriadType.AugmentedTriad, TriadInversion.NoInversion, FundamentalTone.Default, triad );
			}
			else
			{
				return;
			}
		}

		private void ClassifyMajorTriadsZ12( Triad triad )
		{
			long firstNotePitchClass = MusicHelper.ReduceToPitchClass( triad.FirstNote.EulerPoint.P.Numerator, 12 );
			long secondNotePitchClass = MusicHelper.ReduceToPitchClass( triad.SecondNote.EulerPoint.P.Numerator, 12 );
			long thirdNotePitchClass = MusicHelper.ReduceToPitchClass( triad.ThirdNote.EulerPoint.P.Numerator, 12 );

			if( triad.triadType == TriadType.NoTriad ) //Abbrechen wenn Dreiklangtyp gefunden wurde
			{
				long lowerInterval = secondNotePitchClass - firstNotePitchClass;
				lowerInterval = MusicHelper.ReduceToPitchClass( lowerInterval, 12 );
				long upperInterval = thirdNotePitchClass - secondNotePitchClass;
				upperInterval = MusicHelper.ReduceToPitchClass( upperInterval, 12 );

				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorThird, lowerInterval, MusicHelper.MinorThird, upperInterval, TriadType.MajorTriad, TriadInversion.NoInversion, FundamentalTone.FirstNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorThird, lowerInterval, MusicHelper.Fourth, upperInterval, TriadType.MajorTriad, TriadInversion.FirstInversion, FundamentalTone.ThirdNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.Fourth, lowerInterval, MusicHelper.MajorThird, upperInterval, TriadType.MajorTriad, TriadInversion.SecondInversion, FundamentalTone.SecondNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.Fifth, lowerInterval, MusicHelper.MajorSixth, upperInterval, TriadType.MajorTriad, TriadInversion.StatisticalInversionFundamentalToneFirst, FundamentalTone.FirstNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorSixth, lowerInterval, MusicHelper.Fourth, upperInterval, TriadType.MajorTriad, TriadInversion.StatisticalInversionFundamentalToneSecond, FundamentalTone.SecondNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorSixth, lowerInterval, MusicHelper.MinorSixth, upperInterval, TriadType.MajorTriad, TriadInversion.StatisticalInversionFundamentalToneThird, FundamentalTone.ThirdNote, triad );
			}
			else
			{
				return;
			}
		}

		private void ClassifyMinorTriadsZ12( Triad triad )
		{
			long firstNotePitchClass = MusicHelper.ReduceToPitchClass( triad.FirstNote.EulerPoint.P.Numerator, 12 );
			long secondNotePitchClass = MusicHelper.ReduceToPitchClass( triad.SecondNote.EulerPoint.P.Numerator, 12 );
			long thirdNotePitchClass = MusicHelper.ReduceToPitchClass( triad.ThirdNote.EulerPoint.P.Numerator, 12 );

			if( triad.triadType == TriadType.NoTriad ) //Abbrechen wenn Dreiklangtyp gefunden wurde
			{
				long lowerInterval = secondNotePitchClass - firstNotePitchClass;
				lowerInterval = MusicHelper.ReduceToPitchClass( lowerInterval, 12 );
				long upperInterval = thirdNotePitchClass - secondNotePitchClass;
				upperInterval = MusicHelper.ReduceToPitchClass( upperInterval, 12 );

				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorThird, lowerInterval, MusicHelper.MajorThird, upperInterval, TriadType.MinorTriad, TriadInversion.NoInversion, FundamentalTone.FirstNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorThird, lowerInterval, MusicHelper.Fourth, upperInterval, TriadType.MinorTriad, TriadInversion.FirstInversion, FundamentalTone.ThirdNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.Fourth, lowerInterval, MusicHelper.MinorThird, upperInterval, TriadType.MinorTriad, TriadInversion.SecondInversion, FundamentalTone.SecondNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.Fifth, lowerInterval, MusicHelper.MinorSixth, upperInterval, TriadType.MinorTriad, TriadInversion.StatisticalInversionFundamentalToneFirst, FundamentalTone.FirstNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MajorSixth, lowerInterval, MusicHelper.Fourth, upperInterval, TriadType.MinorTriad, TriadInversion.StatisticalInversionFundamentalToneSecond, FundamentalTone.SecondNote, triad );
				CheckIfExpectedEqualsValueAndSetType( MusicHelper.MinorSixth, lowerInterval, MusicHelper.MajorSixth, upperInterval, TriadType.MinorTriad, TriadInversion.StatisticalInversionFundamentalToneThird, FundamentalTone.ThirdNote, triad );
			}
			else
			{
				return;
			}
		}

		private void CheckIfExpectedEqualsValueAndSetType(
			long lowerIntervalExpected,
			long lowerIntervalValue,
			long upperIntervalExpected,
			long upperIntervalValue,
			TriadType triadTypeToSet,
			TriadInversion triadInversionToSet,
			FundamentalTone fundamentalToneToSet,
			Triad triad)
		{

			if( lowerIntervalExpected == lowerIntervalValue && upperIntervalExpected == upperIntervalValue )
			{
				triad.triadType = triadTypeToSet;
				triad.FundamentalTone = fundamentalToneToSet;
				triad.triadInversion = triadInversionToSet;
			}
		}
	}	
}
