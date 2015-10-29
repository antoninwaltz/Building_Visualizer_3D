using UnityEngine;
using System.Collections;

public class SimulationController : MonoBehaviour {

	public static bool m_isWalking;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetIsWalking(bool _isWalking)
	{
		m_isWalking = _isWalking;
	}

}
