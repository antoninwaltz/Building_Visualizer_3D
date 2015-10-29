using UnityEngine;
using System.Collections;

public class SimulationController : MonoBehaviour {

	public static bool m_isWalking = true;

	private Camera m_sceneCamera;
	private Camera m_uiCamera;

	// Use this for initialization
	void Start () {
		m_sceneCamera = GameObject.Find("camera_scene").GetComponent<Camera>();
		m_uiCamera = GameObject.Find("camera_ui").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetIsWalking(bool _isWalking)
	{
		m_isWalking = _isWalking;
	}

	void LateUpdate()
	{
		m_uiCamera.transform.rotation = m_sceneCamera.transform.rotation;
	}
}
