using UnityEngine;
using System.Collections;

public class SimulationController : MonoBehaviour {

	public static bool m_volumeON;

	// Use this for initialization
	void Start () {
		m_volumeON = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void LateUpdate()
	{

	}

	public void DebugEvent(string s)
	{
		Debug.Log (s);
	}
}
