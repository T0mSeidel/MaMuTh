using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh.MusicDataObject
{
	class Channel
	{
		public string Name;
		public List<NoteData> Notes;

		public Channel(string name, List<NoteData> notes )
		{
			Name = name;
			Notes = notes;
		}
	}
}
