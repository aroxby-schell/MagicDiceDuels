using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class IconList : MonoBehaviour
{
	private class NameValuePair : IComparable<NameValuePair>
	{
		public string name, value;
		
		public NameValuePair(string name, string value)
		{
			this.name = name;
			this.value = value;
		}
		
		public static NameValuePair Auto(string name)
		{
			return new NameValuePair(name, "DiceMaterials/"+name);
		}
		
		public int CompareTo(NameValuePair nvp)
		{
			return name.CompareTo(nvp.name);
		}
	}
	
	///////////////////////////////////////////////////////////////////////////////
	
	private static NameValuePair[] materialTable;
	
	public static void EnsureLoaded()
	{
		materialTable = new NameValuePair[]{
				NameValuePair.Auto("Air"),
				NameValuePair.Auto("Earth"),
				NameValuePair.Auto("Fire"),
				NameValuePair.Auto("Water")
			};

		Array.Sort(materialTable);
	}
	
	private static string GetMaterialPath(string name)
	{
		int idx = Array.BinarySearch(materialTable, NameValuePair.Auto(name));
		return materialTable[idx].value;
	}
	
	public static Material GetMaterial(string name)
	{
		return Resources.Load( GetMaterialPath(name) ) as Material;
	}
}
