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

		//###Methods
		//##Information

		public void PrintInstrumentInformation()
		{
			Console.WriteLine( "> Name: " + Name );
			Console.WriteLine( " Note count: " + Notes.Count );
			for(int i=0; i<Notes.Count; i++ )
			{
				Console.WriteLine( "> Note #" + i.ToString() );
				Notes[ i ].PrintNoteInformation();
			}
		}
	}
}
