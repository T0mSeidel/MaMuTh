using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh.MusicDataObject
{
	class NoteData
	{
		public Fraction[] Exponents;
		public Fraction Duration;
		public Fraction Onset;
		public int Dynamics;	

		public NoteData(Fraction[] exponents, Fraction duration, Fraction onset, int dynamics)
		{
			Exponents = exponents;
			Duration = duration;
			Onset = onset;
			Dynamics = dynamics;
		}
	}
}
