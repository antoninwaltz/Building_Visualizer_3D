using UnityEngine;
using System.Collections;

public enum BodyState {Standing = 0, Walking = 1, Running = 2}

public class BodyMovement : MonoBehaviour {

	/** Character's speed factor used when walking. */
	public float m_walkSpeed;
	
	/** Character's speed factor used when running. */
	public float m_runSpeed;
	
	/** Gravity constant of the simulation. */
	public float m_gravity;

	/** Direction the character has to move to each update. */
	private Vector3 m_movementDirection;
	
	/** CharacterController needed to move the character. */
	private CharacterController m_controller;
	
	/** BodyState indicating the current state of the body. */
	private BodyState m_bodyState;
	
	/** Boolean telling if the character is running or not. */
	private bool m_isRunning;

	/** Minimum angle the character can look to along the X-axis. */
	private float m_minXAngle;

	/** Angle from which the body starts slowing down. */
	public float m_slowZoneAngleLimit;

	public GameObject m_playerMenu;

	/**
	 * Start()
	 * Method used for initialization.
	 * No parameters.
	 * No returns.
	 */
	private void Start () {
		m_controller = GetComponent<CharacterController> ();
		
		/* Make the rigid body not change rotation */
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		m_bodyState = BodyState.Standing;
		m_minXAngle = 25;
	}

	/**
	 * Update()
	 * Method used when updating the simulation each frame.
	 * No parameters.
	 * No returns.
	 */	
	void Update () 
	{
		/* If the character is walking, multiply the direction vector with the 'walkSpeed' factor. */
		if (m_bodyState == BodyState.Walking)
			m_movementDirection = new Vector3 (0, 0, 1) * m_walkSpeed;
		
		/* If the character is running, multiply the direction vector with the 'runSpeed' factor. */
		else if (m_bodyState == BodyState.Running)
			m_movementDirection = new Vector3 (0, 0, 1) * m_runSpeed;
		
		/* Otherwise, it is standing, so there is no movement. */
		else
			m_movementDirection = Vector3.zero;

		/* Align the movement direction by multiplying it with the caracter's rotation quaternion. */
		m_movementDirection = transform.rotation * m_movementDirection;

		/*  Gets the X-axis angle between [-180;180]. */
		float signedAngle = GetSignedXEulerAngle (transform.eulerAngles.x);

		/* If the signed angle is inside the slow zone, the deceleration function is applied to the movement direction. */
		if (signedAngle <= m_slowZoneAngleLimit) 
			m_movementDirection *= (m_minXAngle - signedAngle)/m_minXAngle;

		/* Gravity is applied to the movement direction. */
		m_movementDirection.y -= m_gravity * Time.deltaTime;

		/* The body is moved towards the movement direction Vector3. */
		m_controller.Move (m_movementDirection);

		m_playerMenu.transform.position = m_controller.transform.position;
	}

	/**
	 * GetSignedXEulerAngle(float _xEulerAngle)
	 * Method used to get an angle between [-180;180].
	 * Parameters :	- float _xEulerAngle : Angle from which a signed angle is returned.
	 * Returns :	- float, signedAngle between [-180;180].
	 */
	private float GetSignedXEulerAngle(float _xEulerAngle)
	{
		if (_xEulerAngle > 180)
			return 180 - transform.eulerAngles.x % 180;
		return -_xEulerAngle;
	}

	/**
	 * StopMoving()
	 * Method used to set the character's BodyState to BodyState.Standing.
	 * No parameters.
	 * No return.
	 */ 
	public void StopMoving()
	{
		m_bodyState = BodyState.Standing;
		Debug.Log ("Now Standing");
	}
	
	/**
	 * StartWalking()
	 * Method used to set the character's BodyState to BodyState.Walking.
	 * No parameters.
	 * No return.
	 */ 
	public void StartWalking()
	{
		m_bodyState = BodyState.Walking;
		Debug.Log ("Now Walking");
	}
	
	/**
	 * StartWRunning()
	 * Method used to set the character's BodyState to BodyState.Running.
	 * No parameters.
	 * No return.
	 */ 
	public void StartRunning()
	{
		m_bodyState = BodyState.Running;
		Debug.Log ("Now Running");
	}

	/**
	 * IsStanding()
	 * Method used to know if the character's BodyState is equal to BodyState.Standing.
	 * No parameters.
	 * No returns.
	 */
	public bool IsStanding()
	{
		return m_bodyState == BodyState.Standing;
	}
	
	/**
	 * IsWalking()
	 * Method used to know if the character's BodyState is equal to BodyState.Walking.
	 * No parameters.
	 * No returns.
	 */
	public bool IsWalking()
	{
		return m_bodyState == BodyState.Walking;
	}

	
	/**
	 * IsRunning()
	 * Method used to know if the character's BodyState is equal to BodyState.Running.
	 * No parameters.
	 * No returns.
	 */
	public bool IsRunning()
	{
		return m_bodyState == BodyState.Running;
	}
}