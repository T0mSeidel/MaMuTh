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
		public int Dynamics;	//Oder Enum wie in Enums.cs

	}
}
