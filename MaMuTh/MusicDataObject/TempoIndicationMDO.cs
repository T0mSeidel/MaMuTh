using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	class TempoIndicationMDO
	{
		public int Tempo;
		public double ValidAt;
		
		public TempoIndicationMDO(int tempo, double validAt )
		{
			Tempo = tempo;
			ValidAt = validAt;
		}
	}
}
