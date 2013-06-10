using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiceController : MonoBehaviour
{
	public delegate void CompeletionCallback();
	
	public NameTracker tracker;
	
	private string[] keys = {"Back", "Bottom", "Front", "Left", "Right", "Top"};	

	private Dictionary<string, Renderer> faces = new Dictionary<string, Renderer>();
	private Dictionary<string, int> reverseMap = new Dictionary<string, int>();
	
	private const float spinAxisSpeed = 600f;
	private Vector3[] spinAxes = { Vector3.up, Vector3.left, Vector3.zero };
	private int spinAxis = 2;
	private float spinAxisCurrent;
	private float spinAxisGoalBase = 1080f;
	//Changes based on selected roll
	private float spinAxisGoal;
	private CompeletionCallback rollCompelete = null;
	
	private Die die;
	
	void Awake()
	{
		Die.EnsureLoaded();
	}
	
	void Start()
	{		
		for(int i = 0; i<keys.Length; i++)
		{
			faces[keys[i]] = transform.Find(keys[i]).renderer;
			reverseMap[keys[i]] = i;
		}
		
		die = Die.GetRandomDie();

		for(int lazyIdx = 0; lazyIdx<Die.SIDES; lazyIdx++)
		{
			_Setup(lazyIdx);
		}
	}
	
	private void _Setup(int i)
	{
		SetFace(keys[i], die.GetElement(i).GetIcon());
	}
	
	public string GetDieName()
	{
		return die.GetName();
	}
	
	public Element GetElement()
	{
		int idx = reverseMap[tracker.objName];
		return die.GetElement(idx);
	}
	
	public void StartSpin(CompeletionCallback cb)
	{
		spinAxis = 0;
		spinAxisCurrent = 0f;
		spinAxisGoal = spinAxisGoalBase + 90f * Random.Range(1, 4);
		rollCompelete = cb;
	}
	
	void Update()
	{		
		if(spinAxes[spinAxis]!=Vector3.zero)
		{
			float degrees = Mathf.Min(spinAxisSpeed*Time.deltaTime, spinAxisGoal-spinAxisCurrent);
			transform.RotateAroundLocal(spinAxes[spinAxis], degrees*Mathf.Deg2Rad);
			spinAxisCurrent += degrees;
			if(spinAxisCurrent>=spinAxisGoal)
			{
				spinAxis++;
				if(spinAxes[spinAxis]==Vector3.zero)
				{
					if(rollCompelete!=null) rollCompelete();
					rollCompelete = null;
					return;
				}
				spinAxisCurrent = 0;
				spinAxisGoal = spinAxisGoalBase + 90f * Random.Range(1, 4);
			}
		}
	}
	
	private void SetFace(string faceName, Material mat)
	{
		faces[faceName].material = mat;
	}
}
