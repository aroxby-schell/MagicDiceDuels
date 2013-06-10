using UnityEngine;
using System.Collections;

public class NoGUI : MonoBehaviour
{
	public SpellController leftSpell ,rightSpell;
	public DamageController leftDmg, rightDmg;
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1)) leftDmg.ResetDmg();
		if(Input.GetKeyDown(KeyCode.Alpha2)) rightDmg.ResetDmg();
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			leftDmg.ResetDmg();
			rightDmg.ResetDmg();
		}
		
		if(Input.GetKeyDown(KeyCode.Return))
		{
			leftSpell.Cast();
			rightSpell.Cast();
		}
	}
}
