using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	private float m_rotationX;
	private float m_rotationY;

	public GameObject m_camera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		m_rotationY = Input.GetAxis ("Mouse X");
		m_rotationX += Input.GetAxis ("Mouse Y");
		
		m_rotationY += m_camera.transform.localEulerAngles.y;
		//m_rotationX = Mathf.Clamp (m_rotationX, m_minimumX, m_maximumX);
		
		m_camera.transform.localEulerAngles = new Vector3(-m_rotationX, m_rotationY, 0);
	}
}
