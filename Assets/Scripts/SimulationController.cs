using UnityEngine;
using System.Collections;

public class SimulationController : MonoBehaviour {
	
	public Camera m_leftCamera;
	public Camera m_uiLeftCamera;
	
	public Camera m_rightCamera;
	public Camera m_uiRightCamera;

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
		m_uiLeftCamera.transform.rotation = m_leftCamera.transform.rotation;
		m_uiRightCamera.transform.rotation = m_rightCamera.transform.rotation;
	}
}
