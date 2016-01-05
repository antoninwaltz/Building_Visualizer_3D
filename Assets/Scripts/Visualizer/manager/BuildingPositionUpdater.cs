using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingPositionUpdater : MonoBehaviour {

	public int FloorIndex { get; private set;}
	private Building m_building;
	private Dictionary<int, Bounds> m_floorBounds;

	private int[] m_keysArray;

	void Start () {
		m_building = GameObject.Find ("building").GetComponent<Building> ();
		m_floorBounds = new Dictionary<int, Bounds> ();

		Transform b = GameObject.Find ("building").transform;
		int index = 0;
		for (int i = 0; i < b.childCount; ++i) 
		{
			if (b.GetChild (i).tag.Equals ("floor")) 
			{
				Bounds bounds = b.GetChild (i).GetComponent<MeshCollider> ().bounds;
				m_floorBounds.Add (index, bounds);
				++index;
			}
		}
			
		int keynb = m_floorBounds.Keys.Count;
		m_keysArray = new int[keynb];
		m_floorBounds.Keys.CopyTo (m_keysArray, 0);

		foreach (Floor f in m_building.GetFloors())
			f.gameObject.SetActive (false);
		UpdateFloorWithHeight ();
	}

	public void UpdateFloorWithHeight()
	{
		bool stop = false;
		/*int index = 0;
		Dictionary<int, float>.Enumerator enumerator = m_floorHeights.GetEnumerator ();
		while (!stop && enumerator.MoveNext ()) {
			KeyValuePair<int, float> pair = enumerator.Current;
			if (pair.Value > transform.position.y) {
				stop = true;
				FloorIndex = index;
			}
			++index;
		}
		*/
		int i = 0;
		while(!stop && i < m_keysArray.Length) 
		{
			Bounds b;
			m_floorBounds.TryGetValue (m_keysArray [i], out b);
			float lowerMidHeight = (b.center.y + b.min.y) / 2;
			float upperMidHeight = b.max.y;//(b.center.y + b.max.y) / 2;

			if (i == 0) 
			{
				if (upperMidHeight > transform.position.y) 
				{
					stop = true;
					FloorIndex = i;

					m_building.GetFloors ().ToArray () [0].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (false);
				}
			}
			else if (i == m_keysArray.Length - 1) 
			{
				stop = true;
				FloorIndex = i;

				m_building.GetFloors ().ToArray () [m_keysArray.Length - 2].gameObject.SetActive (false);
			} 
			else 
			{
				if (lowerMidHeight > transform.position.y) {
					Debug.Log ("below lower of "+i);
					stop = true;
					int floorIndexToReappear = m_keysArray [i - 1];
					m_building.GetFloors ().ToArray () [floorIndexToReappear].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [floorIndexToReappear + 1].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [floorIndexToReappear + 2].gameObject.SetActive (false);
				} else if (upperMidHeight < transform.position.y) {
					Debug.Log ("below upper of "+i);
					stop = true;
					FloorIndex = i;
					int floorIndexToReappear = m_keysArray [i];
					Debug.Log ("i " + i);
					Debug.Log (floorIndexToReappear);
					m_building.GetFloors ().ToArray () [floorIndexToReappear].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [floorIndexToReappear - 1].gameObject.SetActive (false);
				} else if(upperMidHeight > transform.position.y) {
					Debug.Log ("above upper of "+i);
					stop = true;
					int floorIndexToReappear = m_keysArray [i + 1];
					m_building.GetFloors ().ToArray () [floorIndexToReappear - 1].gameObject.SetActive (false);
					m_building.GetFloors ().ToArray () [floorIndexToReappear].gameObject.SetActive (true);
				}

			}
			++i;
		}
	}
}
