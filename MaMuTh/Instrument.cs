using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	class Instrument
	{
		public string Name;
		public List<Note> Notes;

		public Instrument(string name, List<Note> notes)
		{
			Name = name;
			Notes = notes;
			
			foreach(Note note in Notes )
			{
				note.SetInstrument( this );
			}
		}
	}
}
