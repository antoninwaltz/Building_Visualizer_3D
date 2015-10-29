using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour {

	public float m_translationSpeed;
	public float m_gravity;
	private Vector3 m_movementDirection;

	CharacterController m_controller;

	// Use this for initialization
	void Start () {
		m_movementDirection = Vector3.zero;
		m_controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SimulationController.m_isWalking)
		{
			m_movementDirection = new Vector3(0, 0, 1) * m_translationSpeed;
			m_movementDirection = transform.rotation * m_movementDirection;
			m_movementDirection.y -= m_gravity * Time.deltaTime;
		}
		m_controller.Move (m_movementDirection);
	}
}
