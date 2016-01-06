using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TelevisionsInteractionDispatcher : MonoBehaviour {

	private HashSet<Television> m_televisions;
	private Television m_interactingTelevision;

	// Use this for initialization
	void Start () {
		m_televisions = new HashSet<Television> ();
		List<GameObject> interactableObjects = GameObject.FindGameObjectsWithTag ("interactable_object").ToList ();
		foreach (GameObject obj in interactableObjects) 
		{
			Television tv = obj.GetComponent<Television> ();
			if (tv != null)
				m_televisions.Add (tv);
		}
	}
	
	// Update is called once per frame
	void Update() {
		if (m_interactingTelevision != null) 
		{
			m_interactingTelevision.transform.GetChild(0).GetChild(1).GetComponent<TelevisionMenuHandler>().Interaction ();
		}
	}

	public void SetInteractingTelevision(Television _interactingTelevision)
	{
		m_interactingTelevision = _interactingTelevision;
	}

	public  void ResetInteractionTelevision()
	{
		m_interactingTelevision = null;
	}
}
