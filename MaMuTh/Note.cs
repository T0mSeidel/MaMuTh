using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh
{
	class Note
	{
		public Pitch Pitch { get; set; }
		public EulerPoint EulerPoint { get; set; }
		public int Dynamics { get; set; }
		public Fraction Duration { get; set; }
		public Fraction Onset { get; set; }

		private Instrument Instrument; //vllt raus

		public Note(int dynamics, Fraction duration, Fraction onset, Fraction[] exponents)
		{
			//own members
			Dynamics = dynamics;
			Duration = duration;
			Onset = onset;

			//create objects for module
			InitializeModuleData( exponents );
		}

		internal void SetInstrument( Instrument instrument )
		{
			if(instrument != null)
				Instrument = instrument;
			else
			{
				throw new ArgumentNullException( "Note instrument can not be set. Instrument is null" );
			}
		}

		private void InitializeModuleData( Fraction[] exponents )
		{
			//EulerPoint init
			switch( exponents.Length )
			{
				case 3:
					EulerPoint = new EulerPoint( exponents[ 0 ], exponents[ 1 ], exponents[ 2 ] );
					break;
				case 4:
					break;
				default:
					Console.WriteLine( "Exponents: " + exponents.Length + " - Number of exponents is not suppoted." );
					throw new ArgumentException( "ERROR: Exponent error." );
			}
		}

		//### Information
		public void PrintNoteInformation()
		{
			Console.WriteLine( "Onset: " + Onset.ToString() +
				" Duration: " + Duration.ToString() +
				" Dynamics: " + Dynamics.ToString() );
			
			EulerPoint.PrintEulerPointInformation();
		}
		//## Helper

	}
}
