using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidiParser;

namespace MaMuTh
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args.Length != 2 )
			{
				Console.WriteLine( "> Error: Wrong numver of command line parameters" );
				Console.WriteLine( " <path of midi file> <destination for information file>" );
				return;
			}

			String pfad = args[0];
			MidiComposition song = new MidiComposition( pfad );
			MusicDataObject.MusicDataObject musicDataObject = song.getMusicDataObject();
			MaMuThFacade facade = new MaMuThFacade();
			facade.CreateComposition( musicDataObject );
			facade.PrintInformationInFile( args[1]);
		}
	}
}
