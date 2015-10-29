using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	CharacterController m_controller;

	public float m_movementSpeed;
	private Vector3 m_movementDirection;

	public float m_horizontalAngleSpeed;
	public float m_verticalAngleSpeed;
	public float m_maxYAngle;
	public float m_minYAngle;

	// Use this for initialization
	void Start () {
		m_controller = GetComponent<CharacterController> ();
		m_movementDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
