using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Die
{
	private const string elementFilePath = @"Dice.xml";
	private static bool loaded = false;
	
	private static Dictionary<string, Die> dice = new Dictionary<string, Die>();
	private static Dictionary<int, string> indexMap = new Dictionary<int, string>();
	
	public static void AddDie(Die d)
	{
		indexMap.Add(dice.Count, d.name);
		dice.Add(d.name, d);
	}
	
	public static Die GetRandomDie()
	{
		int idx = Random.Range(0, indexMap.Count);
		return dice[indexMap[idx]];
	}
	
	public static Die GetDie(string name)
	{
		return dice[name];
	}
	
	public static void EnsureLoaded()
	{
		if(loaded) return;
		loaded = true;		
		Element.EnsureLoaded();
		
		AddDie( new Die("WWWEAF", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("EEEWAF", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("FFFWAE", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("AAAWFE", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("EEWWAA", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("WWAAFF", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("AAFFEE", "Water", "Water", "Water", "Earth", "Air", "Fire") );
		AddDie( new Die("FFEEWW", "Water", "Water", "Water", "Earth", "Air", "Fire") );
	}
	
	////////////////////////////////////////////////////////////////////////
	
	public const int SIDES = 6;
	
	private string name;
	private Element[] sides;
	
	public Die(string dieName, params Element[] elems)
	{
		name = dieName;
		sides = new Element[SIDES];
		for(int i = 0; i<sides.Length; i++)
		{
			sides[i] = elems[i];
		}
	}
	
	public Die(string dieName, params string[] elementNames)
	{
		name = dieName;
		sides = new Element[SIDES];
		for(int i = 0; i<sides.Length; i++)
		{
			sides[i] = Element.GetElement(elementNames[i]);
		}
	}
	
	public string GetName()
	{
		return name;
	}
	
	public Element GetElement(int idx)
	{
		return sides[idx];
	}
}
