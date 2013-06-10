using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour
{
	private const string formatString = "F2";
	
	private TextMesh output;
	private float totalDmg = 0f;
	
	void Awake()
	{
		output = GetComponent<TextMesh>();
		output.text = "";
	}
	
	public void ResetDmg()
	{
		float dmg = totalDmg = 0f;
		
		output.text = "Damge taken from other player:";
		output.text += "\nDamage: " + dmg.ToString(formatString);
		output.text += "\nTotal damage: " + totalDmg.ToString(formatString);
	}
	
	public void TakeDamge(float dmg, float acc)
	{
		bool bHit = ((Random.value*3f) < acc);
		
		output.text = "Damge taken from other player:";
		
		if(bHit)
		{
			totalDmg += dmg;
			output.text += "\nDamage: " + dmg.ToString(formatString);
		}
		else
		{
			output.text += "\nDamage: Miss";
		}
		
		output.text += "\nTotal damage: " + totalDmg.ToString(formatString);
	}
}
