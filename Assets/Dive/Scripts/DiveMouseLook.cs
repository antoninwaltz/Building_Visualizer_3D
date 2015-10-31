using UnityEngine;
using System.Collections;

public class DiveMouseLook : MonoBehaviour {

	public float m_sensitivityX;
	public float m_sensitivityY;
	
	public float m_minimumX;
	public float m_maximumX;
	
	public float m_minimumY;
	public float m_maximumY;
	
	float m_rotationX = 0F;
	float m_rotationY = 0F;
	bool mouse_on = true;
	
	private PlayerMenuHandler m_menuHandler;
	
	public GameObject m_panelRightBottomMenu;
	public GameObject m_panelLeftBottomMenu;
	
	public Camera m_leftCamera;
	public Camera m_rightCamera;
	
	
	void Start ()
	{
		if (Application.platform == RuntimePlatform.Android)
			mouse_on=false;
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
			mouse_on=false;
		
		/* Make the rigid body not change rotation */
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		m_menuHandler = GetComponent<PlayerMenuHandler> ();
	}
	
	void Update ()
	{	
		if (mouse_on)
		{
			
			HandleRotation();
			
			m_menuHandler.HandleMenuPointer();
		}
	}
	
	private void HandleRotation()
	{
		/* Computes the X and Y rotation from the current device used. */
		m_rotationY  = Input.GetAxis ("Mouse X") * m_sensitivityY;
		m_rotationX += Input.GetAxis ("Mouse Y") * m_sensitivityX;

		m_rotationY += transform.localEulerAngles.y;
		m_rotationX = Mathf.Clamp (m_rotationX, m_minimumX, m_maximumX);
		
		transform.localEulerAngles = new Vector3(-m_rotationX, m_rotationY, 0);

	}
	
	public float GetMaxXAngle()
	{
		return m_maximumX;
	}

	public float GetMinXAngle()
	{
		return m_minimumX;
	}
}