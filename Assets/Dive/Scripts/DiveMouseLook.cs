using UnityEngine;
using System.Collections;

public class DiveMouseLook : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes m_axes = RotationAxes.MouseXAndY;
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
		m_rotationX  = Input.GetAxis ("Mouse X") * m_sensitivityX;
		m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;
		
		if (m_axes == RotationAxes.MouseXAndY)
		{
			m_rotationX += transform.localEulerAngles.y;
			m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
			
			transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
		}
		else if (m_axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, m_rotationX, 0);
		}
		else
		{
			m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
			
			transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
		}
	}
	
	public float GetMaxYAngle()
	{
		return m_maximumY;
	}

	public float GetMinYAngle()
	{
		return m_minimumY;
	}
}