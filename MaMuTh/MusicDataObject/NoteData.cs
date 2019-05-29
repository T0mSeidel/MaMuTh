using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	class NoteData
	{
		public float P;			//float? Rational mit Zähler/Nenner?
		public float S;
		public float R;
		public float Duration;  //float? oder eigene Einheit? Zählweise?
		public float Onset;
		public int Dynamics;	

		public NoteData(float p, float s, float r, float duration, float onset, int dynamics)
		{
			P = p;
			S = s;
			R = r;
			Duration = duration;
			Onset = onset;
			Dynamics = dynamics;
		}
	}
}
