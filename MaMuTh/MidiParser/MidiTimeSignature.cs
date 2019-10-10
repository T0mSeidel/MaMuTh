using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiTimeSignature
    {
        public MidiTime time;
        public string metrum;
        public string clocksPerCklicks;
        public string thirtySecondNotesPerBeat;

        public MidiTimeSignature(long startTime, string metrum, string clocksPerCklicks, string thirtySecondNotesPerBeat, TempoMap tempoMap)
        {
            this.time = new MidiTime(startTime, tempoMap);
            this.metrum = metrum;
            this.clocksPerCklicks = clocksPerCklicks;
            this.thirtySecondNotesPerBeat = thirtySecondNotesPerBeat;
        }

        public override string ToString()
        {
            return String.Format("[ {0} ]\nTime Signature: {1}, {2}, {3}", time.ToString(), metrum, clocksPerCklicks, thirtySecondNotesPerBeat);
        }
    }
}
