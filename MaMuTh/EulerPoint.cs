using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mehroz;

namespace MaMuTh
{
	class EulerPoint
	{
		public Fraction P, S, R;
		private float Frequency;

		public EulerPoint( Fraction p, Fraction s, Fraction r )
		{
			P = p;
			S = s;
			R = r;
		}

		//### Mehtods
		//## Information
		public void PrintEulerPointInformation()
		{
			Console.WriteLine( ToString() );
		}

		//#Helper
		public String ToString()
		{
			return "EulerPoint3D: P" + P.ToString() + " S " + S.ToString() + " R " + R.ToString();
		}

		public void NormalizeFractions()
		{
			P *= P.Denominator;
			S *= S.Denominator;
			R *= R.Denominator;
		}
	}
}
