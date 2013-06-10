using UnityEngine;
using System.Collections;

public class DieMouseOver : MonoBehaviour
{
	public DiceController[] die;
	public TextMesh[] labels;
	
	private int rayMask;
	private Renderer lastRenderer = null;
	
	void Start()
	{
		rayMask = (1<<LayerMask.NameToLayer("DiceMouseOver"));
	}
	
	private void React(TextMesh mesh)
	{
		if(lastRenderer) lastRenderer.enabled = false;
		if(mesh) lastRenderer = mesh.renderer;
		else lastRenderer = null;
		if(lastRenderer) lastRenderer.enabled = true;
	}

	void Update()
	{
		RaycastHit hit;
		DiceController search;
		
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(mouseRay, out hit, 500f, rayMask);
		
		if(hit.collider && (search=hit.collider.GetComponent<DiceController>()) )
		{
			for(int i = 0; i<die.Length; i++)
			{
				if(search==die[i]) React(labels[i]);
			}
		}
		else
		{
			React(null);
		}
	}
}
