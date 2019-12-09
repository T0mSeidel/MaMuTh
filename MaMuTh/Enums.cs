using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
	enum Temperament
	{
		Default = 0,
		WellTemperament = 1,
		JustIntonation = 2,
		PythagoreanTuning = 3,
		EqualTemperament = 4
	}

	enum Dynamics
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

	enum TriadType
	{
		MajorTriad = 0,
		MinorTriad = 1,
		DiminishedTriad = 2, //verminderter Dreiklang
		AugmentedTriad = 3, //übermäßiger Dreiklang
		AllTriads = 4,
		NoTriad = 5
	}

	enum TriadInversion
	{
		NoInversion = 0,
		FirstInversion = 1,
		SecondInversion = 2,
		StatisticalInversionFundamentalToneFirst = 3,
		StatisticalInversionFundamentalToneSecond = 4,
		StatisticalInversionFundamentalToneThird = 5
	}

	enum FundamentalTone
	{
		Default = 0,
		FirstNote = 1,
		SecondNote = 2,
		ThirdNote = 3
	}
}
