using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	public enum Temperament
	{
		Default = 0,
		WellTemperament = 1,
		JustIntonation = 2,
		PythagoreanTuning = 3,
		EqualTemperament = 4
	}

	public enum Dynamics
	{
		pianississimo = 0,
		pianissimo = 1,
		piano = 2,
		mezzopiano = 3,
		mezzoforte = 4,
		forte = 5,
		fortissimo = 6,
		fortississimo = 7
	}

	public enum TriadType
	{
		MajorTriad = 0,
		MinorTriad = 1,
		DiminishedTriad = 2, //verminderter Dreiklang
		AugmentedTriad = 3, //übermäßiger Dreiklang
		AllTriads = 4,
		NoTriad = 5,
		AllRealTriads = 6
	}

	public enum TriadInversion
	{
		NoInversion = 0,
		FirstInversion = 1,
		SecondInversion = 2,
		StatisticalInversionFundamentalToneFirst = 3,
		StatisticalInversionFundamentalToneSecond = 4,
		StatisticalInversionFundamentalToneThird = 5
	}

	public enum FundamentalTone
	{
		Default = 0,
		FirstNote = 1,
		SecondNote = 2,
		ThirdNote = 3
	}

    public enum Key
    {
        custom = 0,
        major = 1,
        minor = 2,
        locrian = 3,
        myxolydian = 4,
        lydian = 5,
        phrygian = 6,
        dorian = 7,
        harmonicMinor = 8,
        melodicMinor = 9,
        altered = 10,
        wholeTone = 11,
        messiaen2 = 12,
        messiaen3 = 13,
        messiaen4 = 14,
        messiaen5 = 15,
        messiaen6 = 16,
        messiaen7 = 17,

    }

    public enum Notes
    {
        A = 0,
        Ais = 1,
        H = 2,
        C = 3,
        Cis = 4,
        D = 5,
        Dis = 6,
        E = 7,
        F = 8,
        Fis = 9,
        G = 10,
        Gis = 11
    }
}
