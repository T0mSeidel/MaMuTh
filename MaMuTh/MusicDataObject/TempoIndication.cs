using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	class TempoIndication
	{
		public int Tempo;
		public float ValidAt;
		
		public TempoIndication(int tempo, float validAt)
		{
			Tempo = tempo;
			ValidAt = validAt;
		}
	}
}
