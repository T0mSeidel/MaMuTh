using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	class Note
	{
		public Pitch Pitch;
		public EulerPoint EulerPoint;
		public Dynamics Dynamics;
		public float Duration;
		public float Onset;

		private Instrument Instrument; //vllt raus

		public Note(float frequency, Dynamics dynamics, float duration, float onset, 
			Composition composition, Instrument instrument)
		{
			//own members
			Dynamics = dynamics;
			Duration = duration;
			Onset = onset;

			//create objects for module
			InitializeModuleData( frequency );

			//support information
			Instrument = instrument;
		}

		private void InitializeModuleData( float frequency )
		{
			Pitch = new Pitch( frequency );
			EulerPoint = new EulerPoint( frequency );
		}

	}
}
