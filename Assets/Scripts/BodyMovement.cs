using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour {
	
	public float m_walkSpeed;
	public float m_runSpeed;
	public float m_gravity;
	private Vector3 m_movementDirection;
	
	private CharacterController m_controller;
	
	private bool m_isWalking;
	private bool m_is_Running;
	
	// Use this for initialization
	void Start () {
		m_controller = GetComponent<CharacterController> ();
		m_isWalking = m_is_Running = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isWalking)
		{
			m_movementDirection = new Vector3 (0, 0, 1) * m_walkSpeed;
			m_movementDirection = transform.rotation * m_movementDirection;
			m_movementDirection.y -= m_gravity * Time.deltaTime;
		}
		else if (m_is_Running)
		{
			m_movementDirection = new Vector3 (0, 0, 1) * m_runSpeed;
			m_movementDirection = transform.rotation * m_movementDirection;
			m_movementDirection.y -= m_gravity * Time.deltaTime;
		} 
		else
		{
			m_movementDirection = Vector3.zero;
		}
		
		m_controller.Move (m_movementDirection);
	}
	
	public void StopMoving()
	{
		m_isWalking = m_is_Running = false;
	}
	
	public void StartWalking()
	{
		m_isWalking = true;
		m_is_Running = false;
	}
	
	public void StartRunning()
	{
		m_isWalking = false;
		m_is_Running = true;
	}
	
}
