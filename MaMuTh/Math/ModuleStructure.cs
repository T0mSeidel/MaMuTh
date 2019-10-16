using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaMuTh;

namespace MaMuTh.Math
{
	class ModuleStructure<T>
	{
		protected ModuleStructure()
		{
			//No instance of moduleStructure wanted
			throw new Exception( "No Instance allowed" );
		}

		static public T Add( T summand, T summand2 )
		{
			return default( T );
		}

		static public T Substract( T minuend, T subtrahend )
		{
			return default( T );
		}

		static public T Mult( T multiplicand, T multiplier )
		{
			return default( T );
		}

		static public T MultScalar( T multiplicand, T multiplier )
		{
			return default( T );
		}
	}
}
