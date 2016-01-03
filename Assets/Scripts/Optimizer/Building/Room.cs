using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public float Width { get; private set;}
	public float Height { get; private set;}
	public float Depth { get; private set;}

	public float Surface { get; private set;}
	public float Volume { get; private set;}

	private HashSet<Wall> m_walls;
	private HashSet<RoomContainer> m_adjacentRoomContainers;

	private HashSet<Actionner> m_commandableActionners;
	private HashSet<InteractableObject> m_interactabeObjects;

	public void Initialize()
	{
		Bounds colliderBoxBounds = GetComponent<BoxCollider> ().bounds;
		Vector3 dimensions = colliderBoxBounds.max - colliderBoxBounds.min;

		Width = dimensions.x;
		Height = dimensions.y;
		Depth = dimensions.z;

		Surface = Width * Height;
		Volume = Surface * Depth;

		m_walls = new HashSet<Wall> ();
		m_adjacentRoomContainers = new HashSet<RoomContainer> ();
		m_commandableActionners = new HashSet<Actionner> ();
		m_interactabeObjects = new HashSet<InteractableObject> ();
		for (int i = 0; i < transform.childCount; ++i) 
		{
			if (transform.GetChild (i).tag.Equals ("commandableObject"))
				m_commandableActionners.Add (transform.GetChild (i).gameObject.GetComponent<Actionner> ());
			if (transform.GetChild (i).tag.Equals ("interactable_object"))
				m_interactabeObjects.Add (transform.GetChild (i).gameObject.GetComponent<InteractableObject> ());
		}
	}

	public HashSet<Wall> GetWalls()
	{
		return m_walls;
	}

	public List<Wall> GetWallsAsList()
	{
		return new List<Wall>(m_walls);
	}

	public void AddWall(Wall _wall)
	{
		m_walls.Add (_wall);

		if (!m_adjacentRoomContainers.Contains (_wall.GetRoomContainer1 ()))
			m_adjacentRoomContainers.Add (_wall.GetRoomContainer1 ());

		if (!m_adjacentRoomContainers.Contains (_wall.GetRoomContainer2 ()))
			m_adjacentRoomContainers.Add (_wall.GetRoomContainer2 ());
	}

	public HashSet<Actionner> GetCommandableActionnerHashSet()
	{
		return m_commandableActionners;
	}

	public List<Actionner> GetCommandableActionnerList()
	{
		return new List<Actionner>(m_commandableActionners);
	}

	public HashSet<RoomContainer> GetAdjacentRoomContainers()
	{
		return m_adjacentRoomContainers;
	}

	public HashSet<InteractableObject> GetInteractableObjects()
	{
		return m_interactabeObjects;
	}

	public override string ToString()
	{
		string display = "";

		display += "\nRoom - "+ name;
		display += "\n width  = " + Width;
		display += "\n height = " + Height;
		display += "\n depth  = " + Depth;
		display += "\nComposed of " + m_walls.Count + " walls :";
		foreach (Wall w in m_walls)
			display += w.ToString ();
		display += "\nEndRoom";

		return display;
	}

	public void ResetRoom()
	{
		foreach (Actionner ac in m_commandableActionners)
			ac.ResetActionner ();
	}
}