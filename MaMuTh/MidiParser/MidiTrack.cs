using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiTrack
    {
        public string trackName;
        public MidiInstrument instrument;
        public List<MidiNote> notes;
        public List<MidiPitchBend> pitchBends;
        public List<MidiTimeSignature> timeSignatures;

        public MidiTrack(TrackChunk track, TempoMap tempoMap)
        {
            this.trackName = "";
            this.pitchBends = new List<MidiPitchBend>();
            this.notes = new List<MidiNote>();
            this.timeSignatures = new List<MidiTimeSignature>();
            this.instrument = new MidiInstrument(0, 0);
            parseEvents(track.GetTimedEvents(), tempoMap);
            parseNotes(track.GetNotes(), tempoMap);

        }

        private string parseEventValue(string text)
        {
            return text.Split('(').Last().Split(')').First();
        }

        private void parseEvents(IEnumerable<TimedEvent> events, TempoMap tempoMap)
        {
            foreach (var ev in events)
            {
                String str = ev.Event.ToString();
                if (str.Contains("Sequence/Track Name"))
                {
                    this.trackName = parseTrackName(str);
                }
                else if (str.Contains("Program Change"))
                {
                    this.instrument = parseInstrument(str);
                   
                }
                else if (str.Contains("Pitch Bend"))
                {
                    parsePitchBendEvents(str, ev.Time, tempoMap);
                }
                else if (str.Contains("Time Signature"))
                {
                    parseTimeSignatureEvents(str, ev.Time, tempoMap);
                }
                else if (str.Contains("Key Signature"))
                {
                    // 
                }
            }
        }

        private string parseTrackName(string eventString)
        {
            return parseEventValue(eventString);
        }

        private MidiInstrument parseInstrument(string eventString)
        {
              int channelNr = Int32.Parse(eventString.Split('[').Last().Split(']').First());
              int nr = Int32.Parse(parseEventValue(eventString));              
            
            return new MidiInstrument(nr, channelNr);
        }

        private void parseNotes(IEnumerable<Note> noteList, TempoMap tempoMap)
        {
            foreach (var note in noteList)
            {
                int nr = Int32.Parse(note.Channel.ToString());
                if (nr == instrument.channel)
                {
                    string name = note.NoteName.ToString();
                    int octave = note.Octave;
                    MidiTime time = new MidiTime(note.Time, tempoMap);
                    MidiTime length = new MidiTime(note.Length, tempoMap);
                    int velocity = Int32.Parse(note.Velocity.ToString());
                    int velocityOff = Int32.Parse(note.OffVelocity.ToString());
                    int noteNr = Int32.Parse(note.NoteNumber.ToString());
                    MidiNote midiNote = new MidiNote(name, octave, time, length, velocity, velocityOff, noteNr);
                    this.notes.Add(midiNote);
                }
            }
        }

        private void parsePitchBendEvents(string eventString, long timeMidi, TempoMap tempoMap)
        {
            int value = Int32.Parse(parseEventValue(eventString));
            this.pitchBends.Add(new MidiPitchBend(timeMidi, value, tempoMap));
        }

        private void parseTimeSignatureEvents(string eventString, long timeMidi, TempoMap tempoMap)
        {
            string metrum, clocksPerClicks, thirtySecondNotesPerBeat;
            eventString = parseEventValue(eventString);
            string[] values = eventString.Split(',');
            metrum = values[0];
            clocksPerClicks = values[1];
            thirtySecondNotesPerBeat = values[2];

            this.timeSignatures.Add(new MidiTimeSignature(timeMidi, metrum, clocksPerClicks, thirtySecondNotesPerBeat, tempoMap));
        }

        public List<MidiEventsTimeline> getEventsTimeline()
        {
            List<MidiEventsTimeline> timeline = new List<MidiEventsTimeline>();

            foreach (var ts in timeSignatures)
            {
                MidiEventsTimeline tl = new MidiEventsTimeline("Time Signature", ts);
                timeline.Add(tl);
            }

            foreach (var note in notes)
            {
                MidiEventsTimeline tl = new MidiEventsTimeline("Note", note);
                timeline.Add(tl);
            }
            foreach (var pb in pitchBends)
            {
                MidiEventsTimeline tl = new MidiEventsTimeline("Pitch bend", pb);
                timeline.Add(tl);
            }
            return timeline.OrderBy(o => o.time).ToList();
        }


        public override string ToString()
        {
            string note = "";
            foreach (var n in notes)
            {
                note += n.ToString() + "\n";
            }

            string pitchBend = "";
            foreach (var pb in pitchBends)
            {
                pitchBend += pb.ToString() + "\n";
            }

            return String.Format("Track Name: {0} \n {1}\n{2}\n{3}", trackName, instrument.ToString(), note, pitchBend);
        }

    }
}
