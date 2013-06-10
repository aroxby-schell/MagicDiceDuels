using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour
{
	private const string formatString = "F2";
	
	public DamageController opponentHealth;
	public SpellController opponentAttack;
	public DiceController left, center, right;
	public TextMesh leftLabel, centerLabel, rightLabel, totalLabel;
	private TextMesh leftShort, centerShort, rightShort;
	
	private int complete = 3;
	private float dmg, acc, speed;
	private float? opponentSpeed = 0f;
	
	void Awake()
	{
		leftLabel.text = "";
		centerLabel.text = "";
		rightLabel.text = "";
		totalLabel.text = "";
	}
	
	void Start()
	{
		leftShort = leftLabel.transform.GetChild(0).GetComponent<TextMesh>();
		centerShort = centerLabel.transform.GetChild(0).GetComponent<TextMesh>();
		rightShort = rightLabel.transform.GetChild(0).GetComponent<TextMesh>();
	}
	
	public void Cast()
	{
		if(complete==3 && opponentSpeed!=null)
		{
			dmg = 0f;
			acc = 0f;
			speed = 0f;
			complete = 0;
			opponentSpeed = null;
			
			StartSpin(left, leftLabel, leftShort, 0);
			StartSpin(center, centerLabel, centerShort, 1);
			StartSpin(right, rightLabel, rightShort, 2);
			totalLabel.text = "---";
		}
	}
	
#if false
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R) && complete==3 && opponentSpeed!=null)
		{
			dmg = 0f;
			acc = 0f;
			speed = 0f;
			complete = 0;
			opponentSpeed = null;
			
			StartSpin(left, leftLabel, 0);
			StartSpin(center, centerLabel, 1);
			StartSpin(right, rightLabel, 2);
			totalLabel.text = "---";
		}
	}
#endif
	
	private void StartSpin(DiceController die, TextMesh label, TextMesh labelShort, int position)
	{
		label.text = "---";
		labelShort.text = "";
		die.StartSpin( ()=>RollComplete(die, label, labelShort, position) );
	}
	
	public void SetOpponentSpeed(float opSpeed)
	{
		opponentSpeed = opSpeed;
		if(complete==3) DoAttack();
	}
	
	public void RollComplete(DiceController die, TextMesh label, TextMesh labelShort, int position)
	{		
		Element e = die.GetElement();
		
		labelShort.text = e.GetName();
		
		label.text = e.GetName() + "\nDmg:\n" + e.GetDamage(position).ToString(formatString)
				+ "\nAcc:\n" + e.GetAccuracy(position).ToString(formatString)
				+ "\nSpeed:\n" + e.GetSpeed(position).ToString(formatString)
				+ "\nDie Summary:\n(" + die.GetDieName() + ")";
		
		dmg += e.GetDamage(position);
		acc += e.GetAccuracy(position);
		speed += e.GetSpeed(position);
		complete++;
		
		if(complete==3)
		{
			opponentAttack.SetOpponentSpeed(speed);
			if(opponentSpeed!=null) DoAttack();
		}
	}
	
	private void DoAttack()
	{
		string atkOrder;
		if(opponentSpeed==null)
		{
			atkOrder = "First";
		}
		else
		{
			atkOrder = "Tie";
			if(speed<opponentSpeed) atkOrder = "Second";
			else if(speed>opponentSpeed) atkOrder = "First";
		}
	
		totalLabel.text ="Total\nDmg:\n" + dmg.ToString(formatString)
			+ "\nAcc:\n" + acc.ToString(formatString)
			+ "\nSpeed:\n" + speed.ToString(formatString)
			+ "\nAttack #:\n" + atkOrder;
		
		opponentHealth.TakeDamge(dmg, acc);
	}
}
