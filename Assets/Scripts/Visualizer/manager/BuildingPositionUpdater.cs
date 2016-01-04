using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingPositionUpdater : MonoBehaviour {

	public int FloorIndex { get; private set;}

	private Dictionary<int, float> m_floorHeights;

	void Start () {
		FloorIndex = 0;	
		m_floorHeights = new Dictionary<int, float> ();

		Transform b = GameObject.Find ("building").transform;
		int index = 0;
		for (int i = 0; i < b.childCount; ++i) 
		{
			if (b.GetChild (i).tag.Equals ("floor")) 
			{
				Vector3 v = b.GetChild (i).GetComponent<MeshCollider> ().bounds.max;
				m_floorHeights.Add (index, v.y);
				++index;
			}
		}
	}

	public void UpdateFloorWithHeight()
	{
		bool stop = false;
		int index = 0;
		Dictionary<int, float>.Enumerator enumerator = m_floorHeights.GetEnumerator ();
		while (!stop && enumerator.MoveNext ()) 
		{
			KeyValuePair<int, float> pair = enumerator.Current;
			if (pair.Value > transform.position.y) 
			{
				stop = true;
				FloorIndex = index;
			}
			++index;
		}
	}
}
