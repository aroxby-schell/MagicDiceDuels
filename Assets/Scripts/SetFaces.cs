using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetFaces : MonoBehaviour
{
	public bool setOnStart;
	
	public Material mat_back, mat_bottom, mat_front, mat_left, mat_right, mat_top;
	//public Renderer left,right,front,back,top,bottom;
	
	private string[] keys = {"Back", "Bottom", "Front", "Left", "Right", "Top"};	
	private float[] rotationValues = {	180f, 180f,
										0f, -90f,
										90f, 180f	};
	
	private Dictionary<string, Renderer> faces = new Dictionary<string, Renderer>();
	private Dictionary<string, Material> mats = new Dictionary<string, Material>();
	private Dictionary<string, float> rots = new Dictionary<string, float>();
	
	private bool slotRotation =  false;
	private const float slotSpeed = 0.1f;
	private float nextRotation;
	
	private const float spinAxisGoal = 1080f;
	private const float spinAxisSpeed = 400f;
	private Vector3[] spinAxes = { Vector3.up, Vector3.left, Vector3.zero };
	private int spinAxis = 2;
	private float spinAxisCurrent;
	
	void Start()
	{
		for(int i = 0; i<keys.Length; i++)
		{
			faces[keys[i]] = transform.Find(keys[i]).renderer;
			rots[keys[i]] = rotationValues[i];
		}
		
		if(setOnStart)
		{
			int lazyIdx = 0;
			_Setup(lazyIdx++, mat_back);
			_Setup(lazyIdx++, mat_bottom);
			_Setup(lazyIdx++, mat_front);
			_Setup(lazyIdx++, mat_left);
			_Setup(lazyIdx++, mat_right);
			_Setup(lazyIdx++, mat_top);
			
			SetAll();
		}
	}
	
	private void RandomizeDictionary<TKey, TValue>(ref Dictionary<TKey, TValue> d)
	{
		List<TKey> _keys = new List<TKey>(d.Keys);
		List<TValue> _vals = new List<TValue>(d.Values);
		d.Clear();
		
		while(_keys.Count>0)
		{
			int Kidx = Random.Range(0, _keys.Count-1);
			int Vidx = Random.Range(0, _vals.Count-1);
			d[_keys[Kidx]] = _vals[Vidx];
			_keys.RemoveAt(Kidx);
			_vals.RemoveAt(Vidx);
		}
	}
	
	private void _Setup(int i, Material m)
	{
		mats[keys[i]] = m;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.S))
		{
			slotRotation = true;
			nextRotation = Time.timeSinceLevelLoad;
		}
		
		if(slotRotation && nextRotation<=Time.timeSinceLevelLoad)
		{
			RandomizeDictionary(ref mats);
			SetAll();
			nextRotation = Time.timeSinceLevelLoad + slotSpeed;
		}

		
		if(Input.GetKeyDown(KeyCode.R))
		{
			spinAxis = 0;
			spinAxisCurrent = 0f;
		}
		
		if(spinAxes[spinAxis]!=Vector3.zero)
		{
			float degrees = Mathf.Min(spinAxisSpeed*Time.deltaTime, spinAxisGoal-spinAxisCurrent);
			transform.RotateAroundLocal(spinAxes[spinAxis], degrees*Mathf.Deg2Rad);
			spinAxisCurrent += degrees;
			if(spinAxisCurrent>=spinAxisGoal)
			{
				spinAxis++;
				spinAxisCurrent = 0;
			}
		}

		
		{
			//Fixes the faces on restart
			Material m;
			if(!Application.isEditor) enabled = false;
			if(!mats.TryGetValue("Back", out m)) Start();
		}
	}
	
	private void SetAll()
	{		
		for(int i = 0; i<keys.Length; i++)
		{
			SetFace(keys[i], mats[keys[i]]);
		}
	}
	
	private void SetFace(string faceName, Material mat)
	{
		Matrix4x4 rot = Matrix4x4.TRS( Vector3.zero, Quaternion.Euler(Vector3.forward*rots[faceName]), Vector3.one );
		mat.SetMatrix("_Rotation", rot);
		faces[faceName].material = mat;
	}
}
