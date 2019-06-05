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
	}
}
