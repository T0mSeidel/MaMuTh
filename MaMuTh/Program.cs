using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaMuTh.Math;

namespace MaMuTh
{
	class Program
	{
		static void Main( string[] args )
		{

			EulerPoint a, b, c;
			a = new EulerPoint( 2.0, 1, 3 );
			b = new EulerPoint( 1.0, 3, 0.3 );

			c = EulerModule.Add( a, b );

			Console.WriteLine( "Ergebnis: P " + c.P.ToString() );
			Console.WriteLine( "Ergebnis: S " + c.S.ToString() );
			Console.WriteLine( "Ergebnis: R " + c.R.ToString() );
		}
	}
}
