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
        static void Showhelp()
        {
            Console.WriteLine("> Usage: <path_of_midi_file> {options} [value]");
            Console.WriteLine("> Availble options:");

            Console.WriteLine("\n{0,-25} {1,20}", "--print <output_file> ","Prints Information about composition In File");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "input_file.mid --print output_file");

            Console.WriteLine("\n{0,-25} {1,20}",  "--triads ", "Shows triads for each instrument");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --triads");

            Console.WriteLine("\n{0,-25} {1,20}", "--info (notes | distance)", "Shows information about all scales");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --info notes");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --info distance");

            Console.WriteLine("\n{0,-25} {1,20}",  "--scales ", "Shows all scales with more than 50% matches");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --scales");

            Console.WriteLine("\n{0,-25} {1,20}",  "--top <Value> ", "Show only a certain number of best scales");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --top 10");

            Console.WriteLine("\n{0,-25} {1,20}",  "--percent <Value> ", "Shows only scales that has more matches than <value>");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --percent 80");

            Console.WriteLine("\n{0,-25} {1,20}",  "--scaletyp <scale> ", "Shows only the specified scale");
            Console.WriteLine("{0,-25} {1,20}", "> Example:", "example.mid --scaletyp wholeTone");

            Console.WriteLine("\n{0,-25} {1,20}",  "--custom <distances> ", "Shows only this custom scale defined by distances");
            Console.WriteLine("{0,-25} {1,20}", "> Example (major scale):", "example.mid --custom 221222");
        }
        static void Main(string[] args)
		{
           
            if (args.Length > 0)
            {      
                String pfad = args[0];
                MidiComposition song = new MidiComposition(pfad);
                MusicDataObject.MusicDataObject musicDataObject = song.getMusicDataObject();
                MaMuThFacade facade = new MaMuThFacade();
                facade.CreateComposition(musicDataObject);

                if (args.Contains("--help"))
                {
                    Showhelp();
                }
                else if (args.Contains("--scaletyp") && args.Length == 3) // Shows how the specified scale fits into this composition
                {
                    facade.FindScales(scaletyp: args[2]);
                }
                else if (args.Contains("--custom") && args.Length == 3) // Shows how this custom scale fits into this composition
                {
                    facade.FindScales(customScale: args[2]);
                }
                else if (args.Contains("--info") && args.Length == 3) // Shows information about all scales
                {
                    if(args[2] == "notes")
                        facade.PrintScalesDistance();
                    else if(args[2] == "distance")
                        facade.PrintScalesDistance();
                    else
                        Console.WriteLine("> Error: Wrong command line parameter");
                }   
                else if (args.Contains("--print") && args.Length == 3) // Prints Information about composition In File
                {
                    facade.PrintInformationInFile(args[2]);
                }
                else if (args.Contains("--top") && args.Length == 3) // Shows only the best matching scale, limited by given value
                    facade.FindScales(top: Int32.Parse(args[2]), percent:0);

                else if (args.Contains("--percent") && args.Length == 3) //  Shows only scales that has more matches in % than the given value
                    facade.FindScales(percent: Int32.Parse(args[2]));

                else if (args.Contains("--triads"))
                {
                    facade.InitializeTriads();
                    //facade.GetTriads(TriadType.MinorTriad);
                    facade.PrintTriads();
                }
                   
                else if (args.Contains("--scales")) // Shows all scales with more than 50% matches for current composition
                    facade.FindScales();
            }
            else
            {
                Console.WriteLine("> Error: Wrong number of command line parameters");
                Showhelp();
            }
        }
	}
}
