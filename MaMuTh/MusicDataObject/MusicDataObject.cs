using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	class MusicDataObject
	{
		public TemperamentDataObject Temperament;
		public int BaseFrequency;
		public int Tempo;
		public List<TimeSignature> TimeSignatures;
		public List<TempoIndication> TempoIndications;
		public List<Channel> Channels;
		
		//Ein globales Zeitmaß wird benötigt?

		public MusicDataObject()
		{	
			
		}
	}
}
