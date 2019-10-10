﻿using System.ComponentModel;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Smf.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
    /// <summary>
    /// Settings according to which instances of the <see cref="Note"/> must be read from or written to
    /// CSV representation.
    /// </summary>
    public sealed class NoteCsvConversionSettings
    {
        #region Fields

        private TimeSpanType _timeType = TimeSpanType.Midi;
        private TimeSpanType _noteLengthType = TimeSpanType.Midi;
        private NoteNumberFormat _noteNumberFormat = NoteNumberFormat.NoteNumber;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets delimiter used to separate values in CSV representation. The default value is comma.
        /// </summary>
        public char CsvDelimiter { get; set; } = ',';

        /// <summary>
        /// Gets or sets format of timestamps inside CSV representation. The default value is <see cref="TimeSpanType.Midi"/>
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="value"/> specified an invalid value.</exception>
        public TimeSpanType TimeType
        {
            get { return _timeType; }
            set
            {
                ThrowIfArgument.IsInvalidEnumValue(nameof(value), value);

                _timeType = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of a note length (metric, bar/beat and so on) which should be used to
        /// write to or read from CSV. The default value is <see cref="TimeSpanType.Midi"/>.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="value"/> specified an invalid value.</exception>
        public TimeSpanType NoteLengthType
        {
            get { return _noteLengthType; }
            set
            {
                ThrowIfArgument.IsInvalidEnumValue(nameof(value), value);

                _noteLengthType = value;
            }
        }

        /// <summary>
        /// Gets or sets the format which should be used to write a note's number to or read it from CSV.
        /// The default value is <see cref="NoteNumberFormat.NoteNumber"/>.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="value"/> specified an invalid value.</exception>
        public NoteNumberFormat NoteNumberFormat
        {
            get { return _noteNumberFormat; }
            set
            {
                ThrowIfArgument.IsInvalidEnumValue(nameof(value), value);

                _noteNumberFormat = value;
            }
        }

        #endregion
    }
}
