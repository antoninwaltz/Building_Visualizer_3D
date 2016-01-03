using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour {

	private BodyMovement m_player;
	private BuildingPositionUpdater m_floorUpdater;
	private InteractionAgent m_interactionAgent;
	private Dictionary<int, HashSet<InteractableObject>> m_unvolontaryInteractableObjects;
	private Dictionary<int, HashSet<InteractableObject>> m_volontaryInteractableObjects;

	// Use this for initialization
	void Start () {
		m_player = GameObject.Find ("player").GetComponent<BodyMovement>();
		m_floorUpdater = m_player.GetComponent<BuildingPositionUpdater> ();
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
		if (m_floorUpdater.RoomContainer == null) 
		{
			HashSet<InteractableObject> hashset;
			m_unvolontaryInteractableObjects.TryGetValue (0, out hashset);
			foreach (InteractableObject o in hashset)
				o.Interaction (m_player.gameObject);
			
		}
		else 
		{
			if (m_interactionAgent.GetInteractableObject () == null) 
			{
				HashSet<InteractableObject> hashset;
				m_unvolontaryInteractableObjects.TryGetValue (m_floorUpdater.FloorIndex, out hashset);
				foreach (InteractableObject o in hashset)
					o.Interaction (m_player.gameObject);
			
				foreach (InteractableObject o in m_floorUpdater.RoomContainer.GetInteractableObjects())
					if (CameraCanObserveObject (o.gameObject))
						o.Interaction (m_player.gameObject);	
			} 
			else 
			{
				m_interactionAgent.GetInteractableObject().HandleInteractableObject();
			}
		}
	}

	private bool CameraCanObserveObject(GameObject _o)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(m_player.m_cardboardHead.transform.GetChild(0).GetComponent<Camera>());
		return GeometryUtility.TestPlanesAABB (planes, _o.GetComponent<MeshRenderer> ().bounds);
	}
}
