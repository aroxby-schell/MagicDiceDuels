using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Element
{
	private static bool loaded = false;
	
	private static Dictionary<string, Element> elements = new Dictionary<string, Element>();
	
	public static void AddElement(Element e)
	{
		elements.Add(e.GetName(), e);
	}
	
	public static Element GetElement(string name)
	{
		return elements[name];
	}
	
	public static void EnsureLoaded()
	{
		if(loaded) return;
		loaded = true;
		
		IconList.EnsureLoaded();
			
		AddElement( new Element("Air", IconList.GetMaterial("Air"),
			3f, 2f, 2f,
			0f, 1f, 0f,
			2f, 2f, 2f)	);
		
		AddElement( new Element("Fire", IconList.GetMaterial("Fire"),
			2f, 1f, 1f,
			3f, 4f, 2f,
			1f, 1f, 2f)	);
		
		AddElement( new Element("Water", IconList.GetMaterial("Water"),
			1f, -1f, -1f,
			1f, 1f, 1f,
			0f, 0f, 2f)	);
		
		AddElement( new Element("Earth", IconList.GetMaterial("Earth"),
			4f, -2f, -2f,
			3f, 2f, 1f,
			0f, 0f, 0f)	);
	}
	
	////////////////////////////////////////////////////////////////////////
	
	private string name;

	private float[] dmg;
	private float[] speed;
	private float[] acc;

	private Material ico;

	public Element(string elementName, Material icon,
			float speed1, float speed2, float speed3,
			float dmg1, float dmg2, float dmg3,
			float acc1, float acc2, float acc3)
	{
		dmg = new float[3];
		acc = new float[3];
		speed = new float[3];
		
		name = elementName;
		
		speed[0] = speed1;
		speed[1] = speed2;
		speed[2] = speed3;
		
		dmg[0] = dmg1;
		dmg[1] = dmg2;
		dmg[2] = dmg3;
		
		acc[0] = acc1;
		acc[1] = acc2;
		acc[2] = acc3;
		
		ico = icon;
	}
	
	public string GetName()
	{
		return name;
	}
	
	public Material GetIcon()
	{
		return ico;
	}
	
	public float GetDamage(int position)
	{
		return dmg[position];
	}
	
	public float GetAccuracy(int position)
	{
		return acc[position];
	}
	
	public float GetSpeed(int position)
	{
		return speed[position];
	}
}

#if false
	public static void EnsureLoaded()
	{
		IconList.EnsureLoaded();
		
		const float thrid = 1f/3f;
		const float baseDmg = 1f, baseCrit = 0.1f*thrid, baseAcc = 0.5f*thrid;
		
		if(loaded) return;
		loaded = true;
		
		string[] name = { "Fire", "Water", "Earth", "Air" };
		float[] damage = { 2f, 1f, 0.5f, 1f };
		float[] critcal = { 0.5f, 1f, 1f, 2f };
		float[] accuracy = { 0.5f, 1f, 1f, 2f };
		for(int i = 0; i<4; i++)
		{
			damage[i] *= baseDmg;
			critcal[i] *= baseCrit;
			accuracy[i] *= baseAcc;
			
			AddElement(
				new Element(name[i], IconList.GetMaterial(name[i]),
				damage[i], damage[i], damage[i],
				critcal[i], critcal[i], critcal[i],
				accuracy[i], accuracy[i], accuracy[i])
			);
		}
#endif