using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingPositionUpdater : MonoBehaviour {

	public int FloorIndex { get; private set;}
	private Building m_building;
	private Dictionary<int, Bounds> m_floorBounds;

	private int[] m_keysArray;

	private GameObject m_groundStairs0;
	private GameObject m_groundStairs1;
	private GameObject m_groundStairs2;
	private GameObject m_roof;

	void Start () {
		m_building = GameObject.Find ("building").GetComponent<Building> ();
		m_groundStairs0 = GameObject.Find ("W_0");
		m_groundStairs1 = GameObject.Find ("W_0_1");
		m_groundStairs2 = GameObject.Find ("W_1_2");
		m_roof = GameObject.Find ("roof");

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
		int i = 0;
		while(!stop && i < m_keysArray.Length) 
		{
			Bounds b;
			m_floorBounds.TryGetValue (m_keysArray [i], out b);
			float lowerMidHeight = (b.center.y + b.min.y) / 2;
			float upperMidHeight = (b.center.y + b.max.y) / 2;

			//Debug.Log (transform.position +" / lower : "+lowerMidHeight+" / upper :"+upperMidHeight);

			if (i == 0 && b.max.y > transform.position.y) 
			{
				stop = true;
				FloorIndex = i;

				m_building.GetFloors ().ToArray () [0].gameObject.SetActive (true);
				m_building.GetFloors ().ToArray () [2].gameObject.SetActive (false);
				m_groundStairs0.SetActive (true);
				m_groundStairs1.SetActive (true);
				m_groundStairs2.SetActive(false);
				m_roof.SetActive (false);

				if(upperMidHeight < transform.position.y)
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);
					
			}
			else if (i == 2) 
			{
				stop = true;
				FloorIndex = i;

				m_building.GetFloors ().ToArray () [0].gameObject.SetActive (false);
				m_building.GetFloors ().ToArray () [2].gameObject.SetActive (true);
				m_groundStairs0.SetActive(false);
				m_groundStairs1.SetActive(false);
				m_groundStairs2.SetActive (true);
				m_roof.SetActive (true);

				if(transform.position.y < lowerMidHeight)
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);
				else
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (false);
			} 
			else 
			{
				if (lowerMidHeight > transform.position.y) {
					stop = true;
					//Debug.Log ("below lower of "+i);
					m_building.GetFloors ().ToArray () [0].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [2].gameObject.SetActive (false);

					m_groundStairs0.SetActive (true);
					m_groundStairs1.SetActive (true);
					m_groundStairs2.SetActive (false);
					m_roof.SetActive (false);
					
				} else if (upperMidHeight > transform.position.y && FloorIndex != 2) {
					stop = true;
					//Debug.Log ("below upper of "+i);
					FloorIndex = i;
					m_building.GetFloors ().ToArray () [0].gameObject.SetActive (false);
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [2].gameObject.SetActive (false);

					m_groundStairs0.SetActive (false);
					m_groundStairs1.SetActive (true);
					m_groundStairs2.SetActive (true);
					m_roof.SetActive (false);
					
				} else if (FloorIndex != 2 && upperMidHeight < transform.position.y) {
//					Debug.Log ("sdfdffddfs");
					//Debug.Log ("above upper of "+i);
					m_building.GetFloors ().ToArray () [0].gameObject.SetActive (false);
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);
					m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);

					m_groundStairs0.SetActive (false);
					m_groundStairs1.SetActive (true);
					m_groundStairs2.SetActive (true);
					m_roof.SetActive (true);
				} 
			}
			++i;
		}
	}
}

/*
if (i == 0) {
	m_building.GetFloors ().ToArray () [0].gameObject.SetActive (true);
	m_building.GetFloors ().ToArray () [1].gameObject.SetActive (false);
	m_building.GetFloors ().ToArray () [2].gameObject.SetActive (false);
	m_groundStairs0.SetActive (true);
	m_groundStairs1.SetActive (true);
	m_groundStairs2.SetActive (false);
	m_roof.SetActive (false);

} else if (i == 1) {
	m_building.GetFloors ().ToArray () [0].gameObject.SetActive (false);
	m_building.GetFloors ().ToArray () [1].gameObject.SetActive (true);
	m_building.GetFloors ().ToArray () [2].gameObject.SetActive (false);
	m_groundStairs0.SetActive (false);
	m_groundStairs1.SetActive (true);
	m_groundStairs2.SetActive (true);
	m_roof.SetActive (true);

} else if (i == 2) {
	m_building.GetFloors ().ToArray () [0].gameObject.SetActive (false);
	m_building.GetFloors ().ToArray () [1].gameObject.SetActive (false);
	m_building.GetFloors ().ToArray () [2].gameObject.SetActive (true);
	m_groundStairs0.SetActive (false);
	m_groundStairs1.SetActive (false);
	m_groundStairs2.SetActive (true);
	m_roof.SetActive (true);

}*/