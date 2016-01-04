using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomContainer : MonoBehaviour {

	private HashSet<Room> m_rooms;

	public int number { get; set;}

	public float Volume { get; private set;}

	public float ObjectiveTemperature { get; set;}
	public float CurrentTemperature { get; set;}
	public float MinDeliveredEnergy {get; set;}
	public float MaxDeliveredEnergy { get; set;}
	public float CurrentDeliveredEnergy { get; set;}

	public bool Prepared { get; set;}

	private bool m_containsCommandableActionners;
	private HashSet<Actionner> m_actionners;
	private HashSet<InteractableObject> m_interactableObjects;

	private HashSet<RoomContainer> m_adjacentRoomContainers;

	private HashSet<GameObject> m_transitions;

	// Use this for initialization
	public void Initialize() {
		m_rooms = new HashSet<Room> ();
		m_transitions = new HashSet<GameObject> ();
		Volume = 0;
		ObjectiveTemperature = float.NaN;
		CurrentTemperature = float.NaN;
		MinDeliveredEnergy = float.NaN;
		MaxDeliveredEnergy = float.NaN;
		CurrentDeliveredEnergy = float.NaN;
		Prepared = false;
		
		m_containsCommandableActionners = false;
		m_adjacentRoomContainers = new HashSet<RoomContainer> ();
		m_actionners = new HashSet<Actionner> ();
		m_interactableObjects = new HashSet<InteractableObject> ();

		for (int i = 0; i < transform.childCount; ++i)
		{
			Room r = transform.GetChild (i).gameObject.GetComponent<Room> ();
			m_rooms.Add(r);
			r.Initialize();
	
			foreach (Actionner a in r.GetCommandableActionnerHashSet())
				if (!m_actionners.Contains (a))
					m_actionners.Add (a);
			foreach (InteractableObject obj in r.GetInteractableObjects())
				if (!m_interactableObjects.Contains (obj))
					m_interactableObjects.Add (obj);

			AddVolume(r);
			
			m_containsCommandableActionners |= (r.GetCommandableActionnerList().ToArray().Length > 0);
		}
	}

	public bool ContainsCommandableActionners()
	{
		return m_containsCommandableActionners;
	}

	public HashSet<Room> GetRooms()
	{
		return m_rooms;
	}

	public List<Room> GetRoomsAsList()
	{
		List<Room> roomList = new List<Room> (m_rooms);
		return roomList;
	}

	private void AddVolume(Room _room)
	{
		Volume += _room.Volume;
	}

	public void InitializeAdjacentRoomContainers()
	{
		foreach (Room r in m_rooms)
			foreach (RoomContainer rc in r.GetAdjacentRoomContainers())
				if (!m_adjacentRoomContainers.Contains (rc))
					m_adjacentRoomContainers.Add (rc);
	}

	public HashSet<RoomContainer> GetAdjacentRoomContainers()
	{
		return m_adjacentRoomContainers;
	}

	public HashSet<Actionner> GetActionners()
	{
		return m_actionners;
	}

	public HashSet<InteractableObject> GetInteractableObjects()
	{
		return m_interactableObjects;
	}

	public override string ToString ()
	{
		string display = "";

		display += "\nRoom container : " + name;
		display += "\n Composed of "+m_rooms.Count+" room spaces.";
		foreach (Room r in m_rooms)
			display += r.ToString ();
		display += "\nEndRoomContainer";

		return display;
	}

	public void ResetRoomContainer()
	{
		ObjectiveTemperature = float.NaN;
		CurrentDeliveredEnergy = float.NaN;
		MinDeliveredEnergy = float.NaN;
		MaxDeliveredEnergy = float.NaN;

		Prepared = false;
	}

	public HashSet<GameObject> GetTransitions()
	{
		return m_transitions;
	}
}
