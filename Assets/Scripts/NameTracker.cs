using UnityEngine;
using System.Collections;

public class NameTracker : MonoBehaviour
{
	private Collider ignore;
	
	public const string NONAME = "none";
	public string objName = NONAME;
	
	void Start()
	{
		ignore = transform.parent.collider;
		transform.parent = null;
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c!=ignore) objName = c.name;
	}
	
	void OnTriggerExit(Collider c)
	{
		if(c!=ignore) objName = NONAME;
	}
}
