using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	class TimeSignature
	{
		public int Numerator; //Zähler
		public int Denominator; //Nenner
		public float ValidAt; //Gültig ab 

		public TimeSignature(int numerator, int denominator, float validAt )
		{
			Numerator = numerator;
			Denominator = denominator;
			ValidAt = validAt;
		}
	}
}
