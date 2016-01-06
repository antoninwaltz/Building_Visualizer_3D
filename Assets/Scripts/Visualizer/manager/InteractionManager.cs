using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour {

	private BodyMovement m_player;
	private BuildingPositionUpdater m_floorUpdater;
	private InteractionAgent m_interactionAgent;
	private Dictionary<int, HashSet<InteractableObject>> m_unvolontaryInteractableObjects;
	private Dictionary<int, HashSet<InteractableObject>> m_volontaryInteractableObjects;

	private bool m_buildingInsideDisabled;
	private bool m_otherFloorsDisabled;

	private Building m_building;

	// Use this for initialization
	void Start () {
		m_buildingInsideDisabled = false;
		m_otherFloorsDisabled = false;

		m_building = GameObject.Find ("building").GetComponent<Building>();
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
			{
				HashSet<InteractableObject> hashset = new HashSet<InteractableObject> ();
				hashset.Add(o.GetComponent<InteractableObject>());
				m_unvolontaryInteractableObjects.Add (index, hashset);
			}
			else 
			{
				HashSet<InteractableObject> hashset;
				m_unvolontaryInteractableObjects.TryGetValue (index, out hashset);
				hashset.Add(o.GetComponent<InteractableObject>());
			}
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
			{
				HashSet<InteractableObject> hashset = new HashSet<InteractableObject> ();
				hashset.Add(o.GetComponent<InteractableObject>());
				m_volontaryInteractableObjects.Add (index, hashset);
			}
			else 
			{
				HashSet<InteractableObject> hashset;
				m_volontaryInteractableObjects.TryGetValue (index, out hashset);
				hashset.Add(o.GetComponent<InteractableObject>());
			}
		}
	}
	
	public void CheckInteractions() {
		
		if (m_interactionAgent.GetInteractableObject () == null) 
		{
			m_floorUpdater.UpdateFloorWithHeight ();
			
			HashSet<InteractableObject> hashset;
			m_unvolontaryInteractableObjects.TryGetValue (m_floorUpdater.FloorIndex, out hashset);
			foreach (InteractableObject o in hashset)
				o.Interaction (m_player.gameObject);
		
			foreach (RoomContainer rc in m_building.GetFloors()[m_floorUpdater.FloorIndex].GetRoomContainers())
				foreach(Room r in rc.GetRooms())
					foreach(InteractableObject o in r.GetInteractableObjects())
						if (CameraCanObserveObject (o.gameObject))
							o.Interaction (m_player.gameObject);	
		} 
		else 
		{
			m_interactionAgent.GetInteractableObject().HandleInteractableObject();
		}

	}

	private bool CameraCanObserveObject(GameObject _o)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(m_player.m_cardboardHead.transform.GetChild(0).GetComponent<Camera>());
		return GeometryUtility.TestPlanesAABB (planes, _o.GetComponent<MeshRenderer> ().bounds);
	}

	/*public void DisableOtherFloors()
	{
		switch (m_floorUpdater.FloorIndex) 
		{
		case 0:
			m_building.transform.GetChild (8).gameobject.setActive (false);
			if (m_floorUpdater.GetRoomContainer () != null)
			{
				m_building.transform.GetChild (4).gameobject.setActive (false);
				m_building.transform.GetChild (5).gameobject.setActive (false);
			}
			else
			{
				m_building.transform.GetChild (4).gameobject.setActive (true);
				m_building.transform.GetChild (5).gameobject.setActive (true);
			}
			break;
		case 1:
			
			break:
		}
	}*/
}
