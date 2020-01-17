using MaMuTh.MusicDataObject;
using Mehroz;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Melanchall.DryWetMidi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiParser
{
    public class MidiComposition
    {
        public string title;
        public string midiFormat;
        public string timeDivision;
        public MidiTime duration;
        public List<MidiTrack> tracks;
        public List<MidiTimeSignature> timeSignatures;
        public List<MidiTempo> tempo;
        public static double[] frequencies;
        private MidiFile file;
        private Playback playback;

        public MidiComposition(string filePath)
        {
            MidiFile midiFile = MidiFile.Read(filePath);           
            if (midiFile.OriginalFormat != MidiFileFormat.MultiTrack) { 
                midiFile.Write(filePath, format: MidiFileFormat.MultiTrack, overwriteFile: true);
                midiFile = MidiFile.Read(filePath);
            }          
            this.midiFormat = midiFile.OriginalFormat.ToString();           
            this.file = midiFile;
            this.title = "";       
            this.timeDivision = midiFile.TimeDivision.ToString();
            this.duration = new MidiTime(midiFile.GetDuration(TimeSpanType.Midi).ToString(), midiFile.GetTempoMap());
            this.tracks = new List<MidiTrack>();
            this.timeSignatures = new List<MidiTimeSignature>();
            this.tempo = new List<MidiTempo>();
            initFrequencies(440);
            parseTracks(midiFile);
            initTimeSignatures();
        }
        public MidiComposition(string filePath, string mode)
        {
            CsvConverter csv = new CsvConverter();
            MidiFileCsvConversionSettings setting = new MidiFileCsvConversionSettings
            {
                TimeType = TimeSpanType.Midi,
                NoteLengthType = TimeSpanType.Musical,
                NoteFormat = NoteFormat.Note,
                NoteNumberFormat = NoteNumberFormat.Letter
            };
            MidiFile midiFile = csv.ConvertCsvToMidiFile(filePath, setting);
            this.file = midiFile;
            this.title = "";
            this.midiFormat = midiFile.OriginalFormat.ToString();
            this.timeDivision = midiFile.TimeDivision.ToString();
            this.duration = new MidiTime(midiFile.GetDuration(TimeSpanType.Midi).ToString(), midiFile.GetTempoMap());
            this.tracks = new List<MidiTrack>();
            this.timeSignatures = new List<MidiTimeSignature>();
            this.tempo = new List<MidiTempo>();
            initFrequencies(440);
            parseTracks(midiFile);
            initTimeSignatures();
        }

        ~MidiComposition()
        {
            if (playback != null)
            {
                playback.Dispose();
            }
        }

        private void parseHeader(TrackChunk track, TempoMap tempoMap)
        {
            foreach (var ev in track.GetTimedEvents())
            {
                String str = ev.Event.ToString();
                if (str.Contains("Sequence/Track Name"))
                {
                    this.title = str.Split('(').Last().Split(')').First();
                }
                else if (str.Contains("Set Tempo"))
                {
                    long value = long.Parse(str.Split('(').Last().Split(')').First());
                    this.tempo.Add(new MidiTempo(ev.Time, value, tempoMap));
                }
            }

        }

        private void parseTracks(MidiFile midiFile)
        {
            IEnumerable<TrackChunk> trackChunks = midiFile.GetTrackChunks();
            if (trackChunks.Count() > 1)
            {
                for (int i = 0; i < trackChunks.Count(); i++)
                {
                    TrackChunk tr = trackChunks.ElementAt(i);
                    if (i == 0)
                    {
                        parseHeader(tr, midiFile.GetTempoMap());
                    }
                    else
                    {
                        MidiTrack track = new MidiTrack(tr, midiFile.GetTempoMap());
                        if (track.timeSignatures.Count > 0)
                            this.timeSignatures.AddRange(track.timeSignatures);
                        this.tracks.Add(track);
                    }
                }
            }
        }

        private void initTimeSignatures()
        {
            foreach (var track in tracks)
            {
                track.timeSignatures.Clear();
                track.timeSignatures.AddRange(timeSignatures);
            }
        }

        private void initFrequencies(int baseFrequency)
        {
            frequencies = new double[127];
            for (int i = 0; i < 127; ++i)
            {
                double x = (i - 9) / 12.0;
                frequencies[i] = (baseFrequency / 32.0) * Math.Pow(2, x);
            }
        }

        public static double getFrequency(int nr)
        {
            return frequencies[nr];
        }

        public void saveAsCsv(string filepath)
        {
            CsvConverter csv = new CsvConverter();
            MidiFileCsvConversionSettings setting = new MidiFileCsvConversionSettings();
            setting.TimeType = TimeSpanType.Midi;
            setting.NoteLengthType = TimeSpanType.Musical;
            setting.NoteFormat = NoteFormat.Note;
            setting.NoteNumberFormat = NoteNumberFormat.Letter;
            csv.ConvertMidiFileToCsv(file, filepath, true, setting);
        }

        public void play(string deviceName)
        {
            if (playback == null)
            {
                OutputDevice outputDevice = OutputDevice.GetByName(deviceName);
                this.playback = file.GetPlayback(outputDevice);
            }
            playback.MoveToStart();
            playback.Start();

        }
        public void stop()
        {
            playback.InterruptNotesOnStop = true;
            ITimeSpan t = playback.GetDuration(TimeSpanType.Midi);
            playback.MoveToTime(t);
            playback.Stop();
        }


        public List<string> getDeviceList()
        {
            List<string> list = new List<string>();
            foreach (var device in OutputDevice.GetAll())
            {
                list.Add(device.Name);
            }
            return list;
        }

        public MusicDataObject getMusicDataObject()
        {
            TemperamentDataObject temperament = TemperamentDataObject.Default;
            int baseFrequency = 440;
            List<TimeSignatureMDO> timeSignatures = new List<TimeSignatureMDO>();
            foreach (var ts in this.timeSignatures)
            {
                int numerator = Int32.Parse(ts.metrum.Split('/').First());
                int denominator = Int32.Parse(ts.metrum.Split('/').Last()); ;

                int n = Int32.Parse(ts.time.musical.Split('/').First());
                int d = Int32.Parse(ts.time.musical.Split('/').Last());

                timeSignatures.Add(new TimeSignatureMDO(numerator, denominator, n, d));

            }


            List<TempoIndicationMDO> tempoIndications = new List<TempoIndicationMDO>();
            foreach (var t in tempo)
            {
                int tempoMDO = t.bpm;
                int n = Int32.Parse(t.time.musical.Split('/').First());
                int d = Int32.Parse(t.time.musical.Split('/').Last());

                tempoIndications.Add(new TempoIndicationMDO(tempoMDO, n, d) );
            }

            List<Channel> channels = new List<Channel>();
            foreach (var track in tracks)
            {
                string name = track.trackName;
                List<NoteData> notesMDO = new List<NoteData>();

                foreach (var note in track.notes)
                {
                    Fraction[] exponents = new Fraction[3];
                    exponents[0] = new Fraction(note.midiNumber - 69, 12);
                    exponents[1] = new Fraction(0);
                    exponents[2] = new Fraction(0);

                    Fraction duration = new Fraction(note.length.musical);
                    Fraction onset = new Fraction(note.time.musical);
                    int dynamics = note.velocity;

                    notesMDO.Add(new NoteData(exponents, duration, onset, dynamics));
                }

                channels.Add(new Channel(name, notesMDO));
            }
            return new MaMuTh.MusicDataObject.MusicDataObject(temperament, baseFrequency, timeSignatures, tempoIndications, channels);
        }

        public override string ToString()
        {
            string timeSigRow = "";
            foreach (var t in timeSignatures)
            {
                timeSigRow += t.ToString() + "\n\n";
            }

            string trRow = "";
            foreach (var tr in tracks)
            {
                trRow += tr.ToString() + "\n";
            }
            string tempoRow = "";
            foreach (var temp in tempo)
            {
                tempoRow += temp.ToString() + "\n";
            }

            return String.Format("Title: {0} \nMidiformat: {1} \nTime Division: {2} \nDuration: {3}\n{4}\n{5}\n{6}", title, midiFormat, timeDivision, duration.ToString(), tempoRow, timeSigRow, trRow);
        }

    }
}
