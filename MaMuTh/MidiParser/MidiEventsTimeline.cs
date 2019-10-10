using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiEventsTimeline
    {
        public string eventName;
        public long time;
        public MidiNote note;
        public MidiPitchBend pb;
        public MidiTimeSignature ts;
        public MidiTime midiTime;

        public MidiEventsTimeline(string eventName, MidiNote note)
        {
            this.eventName = eventName;
            this.time = note.time.midi;
            this.note = note;
            this.pb = null;
            this.ts = null;
            this.midiTime = note.time;
        }
        public MidiEventsTimeline(string eventName, MidiPitchBend pb)
        {
            this.eventName = eventName;
            this.time = pb.time.midi;
            this.note = null;
            this.pb = pb;
            this.ts = null;
            this.midiTime = pb.time;
        }
        public MidiEventsTimeline(string eventName, MidiTimeSignature ts)
        {
            this.eventName = eventName;
            this.time = ts.time.midi;
            this.note = null;
            this.pb = null;
            this.ts = ts;
            this.midiTime = ts.time;
        }

    }
}
