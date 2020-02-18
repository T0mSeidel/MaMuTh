using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.Music
{
	public class MusicHelper
	{
		//interval desriptions and values
		public static long MinorThird = 3;
		public static long MajorThird = 4;
		public static long Fourth = 5;
		public static long Tritone = 6;
		public static long Fifth = 7;
		public static long MinorSixth = 8;
		public static long MajorSixth = 9;

		public static long ReduceToPitchClass(long Numerator, long ReductionClass)
		{
			//Reduce the total note value to a note between c and h

			long pitchClass = Numerator % ReductionClass; //z.B. wird hier ein c' (=13) % 13 gerechnet und Resulatat = 0 (Was ebenfalls ein c ist)

			if(pitchClass < 0 )
			{
				pitchClass *= ( -1 );
				pitchClass = ReductionClass - pitchClass;

				//Wenn Zahl negativ dann zu positiver Zahl machen
				//Auf Grund der Tatsache, dass -1 = Ton 'h' ist muss die Zahl angepasst werden
				//Bsp: gegeben: -5 = 'g | aber in positiven Zahlen ist 5 = f
				//-5 *(-1) = 5 -> 12 - 5 = 7 und 7 = 'g'
			}

			return pitchClass;
		}
	}
}
