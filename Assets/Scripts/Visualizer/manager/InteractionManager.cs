using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour {

	private BodyMovement m_player;
	private InteractionAgent m_interactionAgent;
	private Dictionary<int, HashSet<InteractableObject>> m_unvolontaryInteractableObjects;
	private Dictionary<int, HashSet<InteractableObject>> m_volontaryInteractableObjects;

	// Use this for initialization
	void Start () {
		m_player = GameObject.Find ("player").GetComponent<BodyMovement>();
		m_interactionAgent =  m_player.GetComponent<InteractionAgent> ();
		m_unvolontaryInteractableObjects = new Dictionary<int, HashSet<InteractableObject>> ();
		m_volontaryInteractableObjects = new Dictionary<int, HashSet<InteractableObject>> ();

		GameObject[] unvolontaryInteractableObjects = GameObject.FindGameObjectsWithTag("unvolontary_interactable_object");
		foreach (GameObject o in unvolontaryInteractableObjects) 
		{
			Transform parentTransform = o.transform.parent;
			int index = 0;
			int i = 0;
			while (!parentTransform.parent.GetChild (i).Equals (parentTransform)) 
			{
				if(parentTransform.parent.GetChild(i).tag.Equals("floor"))
					++index;
				++i;
			}
			
			if (!m_unvolontaryInteractableObjects.ContainsKey (index))
				m_unvolontaryInteractableObjects.Add (index, new HashSet<InteractableObject> ());

			HashSet<InteractableObject> hashset;
			m_unvolontaryInteractableObjects.TryGetValue (index, out hashset);
			hashset.Add(o.GetComponent<InteractableObject>());
		}

		GameObject[] volontaryInteractableObjects = GameObject.FindGameObjectsWithTag("interactable_object");
		foreach (GameObject o in volontaryInteractableObjects)
		{
			Transform parentTransform = o.transform.parent;
			int index = 0;
			int i = 0;
			while (!parentTransform.parent.GetChild (i).Equals (parentTransform)) 
			{
				if(parentTransform.parent.GetChild(i).tag.Equals("floor"))
					++index;
				++i;
			}

			if (!m_volontaryInteractableObjects.ContainsKey (index))
				m_volontaryInteractableObjects.Add (index, new HashSet<InteractableObject> ());

			HashSet<InteractableObject> hashset;
			m_volontaryInteractableObjects.TryGetValue (index, out hashset);
			hashset.Add(o.GetComponent<InteractableObject>());
		}
	}
	
	// Update is called once per frame
	public void CheckInteractions() {
		int floorIndex = m_player.GetFloorIndex ();
		HashSet<InteractableObject> hashset;
		m_unvolontaryInteractableObjects.TryGetValue (floorIndex, out hashset);
		foreach(InteractableObject o in hashset)
			o.Interaction(m_player.gameObject);

		if(m_interactionAgent.GetInteractableObject() == null)
			m_volontaryInteractableObjects.TryGetValue (floorIndex, out hashset);
			foreach(InteractableObject o in hashset)
				if(CameraCanObserveObject(o.gameObject))
					o.Interaction(m_player.gameObject);
	}

	private bool CameraCanObserveObject(GameObject _o)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(m_player.m_cardboardHead.transform.GetChild(0).GetComponent<Camera>());
		return GeometryUtility.TestPlanesAABB (planes, _o.GetComponent<MeshRenderer> ().bounds);
	}
}
