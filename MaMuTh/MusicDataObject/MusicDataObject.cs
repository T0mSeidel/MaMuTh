using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	public class MusicDataObject
	{
		//MusicDataObject (MDO) is a data object for communication between different applications
		//look at FuE Doku 2019

		//Example: 
		//Midi parser generates an MDO
		//Facade needs an MDO to generate an Composition
	

		public TemperamentDataObject Temperament;
		public int BaseFrequency;
		public List<TimeSignatureMDO> TimeSignatures;
		public List<TempoIndicationMDO> TempoIndications;
		public List<Channel> Channels;
		public double TotalDuration;
		//globales Zeitmaß ist ein Floatwert

		public MusicDataObject(TemperamentDataObject temperament, int baseFrequency,
			List<TimeSignatureMDO> timeSignatures, List<TempoIndicationMDO> tempoIndications, List<Channel> channels)
		{
			Temperament = temperament;
			BaseFrequency = baseFrequency;
			TimeSignatures = timeSignatures;
			TempoIndications = tempoIndications;
			Channels = channels;
		}

		public void CalculateTotalDuration()
		{
			double totalDuration = 0;

			foreach(Channel channel in Channels )
			{
				NoteData latestNote = channel.Notes.Find( y => ( y.Onset + y.Duration ) > totalDuration );
				if( latestNote != null )
				{
					totalDuration = (latestNote.Onset + latestNote.Duration).ToDouble();
				}
			}

			TotalDuration = totalDuration;
		}

	}
}
