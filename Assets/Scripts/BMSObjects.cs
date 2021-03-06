﻿using UnityEngine;

public class Line
{
	public ListExtension<Note> NoteList;
	public ListExtension<Note> LandMineList;
	public Line()
	{
		NoteList = new ListExtension<Note>()
		{
			Capacity = 225
		};
		LandMineList = new ListExtension<Note>()
		{
			Capacity = 20
		};
	}
}

public abstract class BMSObject : System.IComparable<BMSObject>
{
	public int Bar { get; protected set; }
	public double Beat { get; protected set; }
	public double Timing { get; set; }

	public BMSObject(int bar, double beat, double beatLength)
	{
		Bar = bar;
		Beat = (beat / beatLength) * 4.0;
	}

	public BMSObject(int bar, double beat)
	{
		Bar = bar;
		Beat = beat;
	}

	public void CalculateBeat(double prevBeats, double beatC)
	{
		Beat = Beat * beatC + prevBeats;
	}

	public int CompareTo(BMSObject other)
	{
		if (Beat < other.Beat) return 1;
		if (Beat == other.Beat) return 0;
		return -1;
	}
}

public class BGChange : BMSObject
{
	public string Key { get; private set; }
	public bool IsPic { get; set; }

	public BGChange(int bar, string key, double beat, double beatLength, bool isPic) : base(bar, beat, beatLength)
	{
		Key = key;
		IsPic = isPic;
	}
}

public class Note : BMSObject
{
	public int KeySound { get; private set; }
	public int Extra { get; set; }
	public GameObject Model { get; set; }

	public Note(int bar, int keySound, double beat, double beatLength, int extra) : base(bar, beat, beatLength)
	{
		KeySound = keySound;
		Extra = extra;
	}
}

public class BPM : BMSObject
{
	public double Bpm { get; private set; }

	public BPM(int bar, double bpm, double beat, double beatLength) : base(bar, beat, beatLength)
	{
		Bpm = bpm;
	}

	public BPM(BPM bpm) : base(bpm.Bar, bpm.Beat)
	{
		Bpm = bpm.Bpm;
	}
}

public class Stop : BMSObject
{
	public string Key;
	public Stop(int bar, string key, double beat, double beatLength) : base(bar, beat, beatLength)
	{
		Key = key;
	}
}

public class BMSResult
{
	public int ClearGauge { get; set; } = -1;
	public int NoteCount { get; set; }
	public int Pgr { get; set; }
	public int Gr { get; set; }
	public int Good { get; set; }
	public int Bad { get; set; }
	public int Poor { get; set; }
	public int Score { get; set; }
	public double Accuracy { get; set; }

}

public class Utility {

	public static double DAbs(double value) => (value > 0) ? value : -value;

	public static int DCeilToInt(double value) => (int)(value + 1);
}

public enum GaugeType
{
	Easy,
	Groove,
	Survival,
	EXSurvival,
	MAXCombo,
	Perfect
}

public class Gauge
{
	public readonly GaugeType Type;
	public readonly float GreatHealAmount;
	public readonly float GoodHealAmount = 0;
	public readonly float BadDamage;
	public readonly float PoorDamage;
	public float Hp { get; set; }
	public Gauge(GaugeType type, float total, int noteCount)
	{
		Type = type;

		if (type == GaugeType.Groove)
		{
			Hp = 0.2f;
			GreatHealAmount = total / noteCount;
			GoodHealAmount = GreatHealAmount / 2;
			BadDamage = 0.04f;
			PoorDamage = 0.06f;
		}
		else if (type == GaugeType.Easy)
		{
			Hp = 0.2f;
			GreatHealAmount = total / noteCount * 1.2f;
			GoodHealAmount = GreatHealAmount / 2;
			BadDamage = 0.032f;
			PoorDamage = 0.048f;
		}
		else if(type == GaugeType.Survival)
		{
			Hp = 1.0f;
			GreatHealAmount = 0.1f;
			BadDamage = 0.06f;
			PoorDamage = 0.1f;
		}
		else if(type == GaugeType.EXSurvival)
		{
			Hp = 1.0f;
			GreatHealAmount = 0.1f;
			BadDamage = 0.1f;
			PoorDamage = 0.18f;
		}
		else if (type == GaugeType.MAXCombo)
		{
			Hp = 1.0f;
			GreatHealAmount = 0.0f;
			BadDamage = 1.0f;
			PoorDamage = 1.0f;
		}
		else if (type == GaugeType.Perfect)
		{
			Hp = 1.0f;
			GreatHealAmount = 0.0f;
			GoodHealAmount = -100.0f;
			BadDamage = 1.0f;
			PoorDamage = 1.0f;
		}

		GreatHealAmount /= 100;
		GoodHealAmount /= 100;
	}
}