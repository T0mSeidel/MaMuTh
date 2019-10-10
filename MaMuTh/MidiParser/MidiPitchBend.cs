using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiPitchBend
    {
        public MidiTime time;
        public int value;

        public MidiPitchBend(long startTime, int value, TempoMap tempoMap)
        {
            this.time = new MidiTime(startTime, tempoMap); ;
            this.value = value;
        }
        public override string ToString()
        {
            return String.Format("[ {0} ]\n {1}", time.ToString(), value);
        }
    }
}
