using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh.MusicDataObject
{
	class TimeSignatureMDO
	{
		public Fraction Fraction { get; set; }
		public double ValidAt { get; set; } //Gültig ab 

		public TimeSignatureMDO(int numerator, int denominator, double validAt )
		{
			Fraction = new Fraction( numerator, denominator );
			ValidAt = validAt;
		}
	}
}
