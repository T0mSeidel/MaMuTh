using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	class TempoIndication
	{ 
		public int Tempo;
		public double ValidAt;

		public TempoIndication( int tempo, double validAt )
		{
			Tempo = tempo;
			ValidAt = validAt;
		}	
	}
}
