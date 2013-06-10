using UnityEngine;
using System.Collections;

public class RemoveOnKeyPress : MonoBehaviour
{
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			renderer.enabled = false;
			Destroy(gameObject);
		}
	}
}
