using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiTime
    {
        public String musical;
        public String barBeat;
        public String metric;
        public long midi;
        public MidiTime(long timeMidi, TempoMap tempoMap)
        {
            this.midi = timeMidi;
            this.metric = TimeConverter.ConvertTo(timeMidi, TimeSpanType.Metric, tempoMap).ToString();
            this.barBeat = TimeConverter.ConvertTo(timeMidi, TimeSpanType.BarBeat, tempoMap).ToString();
            this.musical = TimeConverter.ConvertTo(timeMidi, TimeSpanType.Musical, tempoMap).ToString();
        }
        public MidiTime(string timeMidi, TempoMap tempoMap)
        {
            this.midi = long.Parse(timeMidi);
            this.metric = TimeConverter.ConvertTo(this.midi, TimeSpanType.Metric, tempoMap).ToString();
            this.barBeat = TimeConverter.ConvertTo(this.midi, TimeSpanType.BarBeat, tempoMap).ToString();
            this.musical = TimeConverter.ConvertTo(this.midi, TimeSpanType.Musical, tempoMap).ToString();
        }
        public override string ToString()
        {
            return String.Format("{0} | {1} | {2} | {3}", midi, metric, barBeat, musical);
        }
    }
}
