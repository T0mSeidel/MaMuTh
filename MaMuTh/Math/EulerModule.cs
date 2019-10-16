using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaMuTh;
using Mehroz;

namespace MaMuTh.Math
{
	class EulerModule: ModuleStructure<EulerPoint>
	{
		private EulerModule(){   /*No instance of moduleStructure wanted*/ }

		new static public EulerPoint Add( EulerPoint summand, EulerPoint summand2 )
		{
			Fraction P, S, R;
			P = summand.P + summand2.P;
			S = summand.S + summand2.S;
			R = summand.R + summand2.R;
			return new EulerPoint( P, S, R );
		}
		
		new static public EulerPoint Substract( EulerPoint minuend, EulerPoint subtrahend )
		{
			Fraction P, S, R;
			P = minuend.P - subtrahend.P;
			S = minuend.S - subtrahend.S;
			R = minuend.R - subtrahend.R;
			return new EulerPoint( P, S, R );
		}

		new static public EulerPoint Mult(EulerPoint multiplicand, EulerPoint multiplier )
		{
			Fraction P, S, R;
			P = multiplicand.P * multiplier.P;
			S = multiplicand.S * multiplier.S;
			R = multiplicand.R * multiplier.R;
			return new EulerPoint( P, S, R );
		}

		new static public EulerPoint MultScalar( EulerPoint multiplicand, EulerPoint multiplier )
		{
			Fraction P, S, R;
			P = multiplicand.P * multiplier.P;
			S = multiplicand.S * multiplier.S;
			R = multiplicand.R * multiplier.R;
			return new EulerPoint( P, S, R );
		}

	}
}
