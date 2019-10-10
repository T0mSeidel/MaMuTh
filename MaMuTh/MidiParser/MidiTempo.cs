using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiTempo
    {
        public MidiTime time;
        public int bpm;
        public long midiValue;

        public MidiTempo(long startTime, long value, TempoMap tempoMap)
        {
            this.time = new MidiTime(startTime, tempoMap);
            this.midiValue = value;
            this.bpm = Convert.ToInt32(60000000 / value);

        }

        public override string ToString()
        {
            return String.Format("{0} bpm at {1}", bpm, time.midi.ToString());
        }
    }
}
