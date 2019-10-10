using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiNote
    {
        public string name;
        public int octave;
        public int midiNumber;
        public MidiTime time;
        public MidiTime length;
        public int velocity;
        public int velocityOff;
        public double frequency;

        public MidiNote(string name, int octave, MidiTime time, MidiTime length, int velocity, int velocityOff, int midiNumber)
        {
            if (name.Length > 1)
                this.name = name.First().ToString() + "#";
            else
                this.name = name;
            this.octave = octave;
            this.time = time;
            this.length = length;
            this.velocity = velocity;
            this.velocityOff = velocityOff;
            this.midiNumber = midiNumber;
            this.frequency = MidiComposition.getFrequency(midiNumber);
        }

        private static double getFrequency(int nr)
        {
            double a = 440.0; //  440 Hz.
            double x = (nr - 9) / 12.0;
            double freq = (a / 32) * Math.Pow(2, x);
            return freq;
        }

        public List<string> getValues()
        {
            List<string> list = new List<string>();
            list.Add(String.Format("{0}{1}", name, octave));
            list.Add(length.musical);
            list.Add(velocity.ToString());
            list.Add("Nr: " + midiNumber.ToString());
            list.Add(frequency.ToString() + " Hz");

            return list;
        }

        public string printNoteEvent()
        {
            return String.Format("{0}{1} {2} {3} {4}Hz", name, octave, length.musical, velocity, frequency);
        }
        public override string ToString()
        {
            return String.Format(" [ {0} ]\n {1}{2} {3} >{4} {5}> Nr.:{6} f:{7}Hz\n", time.ToString(), name, octave, length.musical, velocity, velocityOff, midiNumber, frequency);
        }

    }
}
