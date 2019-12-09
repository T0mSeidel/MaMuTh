using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh.Music
{
	class Triad
	{
		public Note FirstNote { get; set; }
		public Note SecondNote { get; set; }
		public Note ThirdNote { get; set; }
		public FundamentalTone FundamentalTone { get; set; }

		public Fraction Onset { get; set; }

		public TriadType triadType { get; set; }
		public TriadInversion triadInversion { get; set; }

		public Triad( Note note1, Note note2, Note note3 )
		{
			FirstNote = note1;
			SecondNote = note2;
			ThirdNote = note3;

			if((note1.Onset == note2.Onset) && (note2.Onset == note3.Onset ) )
			{
				Onset = note1.Onset;
			}
			else
			{
				throw new Exception( "Triad onsets differ. Pass onset as parameter." );
			}

			triadType = TriadType.NoTriad;
			triadInversion = TriadInversion.NoInversion;
			FundamentalTone = FundamentalTone.Default;
		}

		public Triad( Note note1, Note note2, Note note3, Fraction onset )
		{
			FirstNote = note1;
			SecondNote = note2;
			ThirdNote = note3;
			Onset = onset;
			triadType = TriadType.NoTriad;
			triadInversion = TriadInversion.NoInversion;
			FundamentalTone = FundamentalTone.Default;
		}

		public void PrintTriadInformation()
		{
			Console.WriteLine( "----------" );
			FirstNote.PrintNoteInformation();
			SecondNote.PrintNoteInformation();
			ThirdNote.PrintNoteInformation();
			Console.WriteLine( "TriadType: " + triadType.ToString() );
			Console.WriteLine( "Inversion: " + triadInversion.ToString() );

			Note fundamentalNote = GetFundamentalTone();
			if(fundamentalNote != null )
			{
				Console.WriteLine( "" );
				Console.WriteLine( "Fundamental Tone: " + FundamentalTone);
				fundamentalNote.EulerPoint.PrintEulerPointInformation();
			}		
			Console.WriteLine( "----------" );
		}

		public Note GetFundamentalTone()
		{
			if( FundamentalTone == FundamentalTone.FirstNote )
			{
				return FirstNote;
			}

			if( FundamentalTone == FundamentalTone.SecondNote )
			{
				return SecondNote;
			}

			if( FundamentalTone == FundamentalTone.ThirdNote )
			{
				return ThirdNote;
			}

			return null;
		}


		//Helper
		public bool Equals(Triad comparedTriad)
		{
			if(Onset == comparedTriad.Onset 
				&& FirstNote.Equals(comparedTriad.FirstNote) 
				&& SecondNote.Equals(comparedTriad.SecondNote) 
				&& ThirdNote.Equals(comparedTriad.ThirdNote))
			{
				return true;
			}
			else
			{

				return false;
			}

		}
	}
}
