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
		public List<TimeSignature> TimeSignatures;
		public List<TempoIndication> TempoIndications;
		public List<Channel> Channels;
		public float TotalDuration;
		//globales Zeitmaß ist ein Floatwert

		public MusicDataObject(TemperamentDataObject temperament, int baseFrequency,
			List<TimeSignature> timeSignatures, List<TempoIndication> tempoIndications, List<Channel> channels)
		{
			Temperament = temperament;
			BaseFrequency = baseFrequency;
			TimeSignatures = timeSignatures;
			TempoIndications = tempoIndications;
			Channels = channels;
		}

		public void CalculateTotalDuration()
		{
			float totalDuration = 0f;

			foreach(Channel channel in Channels )
			{
				NoteData latestNote = channel.Notes.Find( y => ( y.Onset + y.Duration ) > totalDuration );
				if( latestNote != null )
				{
					totalDuration = latestNote.Onset + latestNote.Duration;
				}
			}
		}

	}
}
