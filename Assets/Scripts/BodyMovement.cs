using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour {
	
	public float m_walkSpeed;
	public float m_runSpeed;
	public float m_gravity;
	private Vector3 m_movementDirection;
	
	private CharacterController m_controller;
	
	private bool m_isWalking;
	private bool m_isRunning;
	
	// Use this for initialization
	void Start () {
		m_controller = GetComponent<CharacterController> ();
		m_isWalking = false;
		m_isRunning = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_isWalking)
			m_movementDirection = new Vector3 (0, 0, 1) * m_walkSpeed;
		else if (m_isRunning)
			m_movementDirection = new Vector3 (0, 0, 1) * m_runSpeed;
		else
			m_movementDirection = Vector3.zero;

		m_movementDirection = transform.rotation * m_movementDirection;
		m_movementDirection.y -= m_gravity * Time.deltaTime;
		m_controller.Move (m_movementDirection);
	}
	
	public void StopMoving()
	{
		m_isWalking = false;
		m_isRunning = false;
		Debug.Log ("Now Standing");
	}
	
	public void StartWalking()
	{
		m_isWalking = true;
		m_isRunning = false;
		Debug.Log ("Now Walking");
	}
	
	public void StartRunning()
	{
		m_isWalking = false;
		m_isRunning = true;
		Debug.Log ("Now Running");
	}
	
	public bool IsStanding()
	{
		return (this.m_isWalking == false && this.m_isRunning == false);
	}
	
	public bool IsWalking()
	{
		return (this.m_isWalking == true);
	}

	
	public bool IsRunning()
	{
		return (this.m_isRunning == true);
	}
}
