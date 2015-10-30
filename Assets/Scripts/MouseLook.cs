using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public enum RotationInputDevice {Mouse = 0, Cardboard = 1}

	public RotationAxes m_axes;
	public RotationInputDevice m_rotationInputDevice;

	public float m_sensitivityX;
	public float m_sensitivityY ;
	
	public float m_minimumX;
	public float m_maximumX;
	
	public float m_minimumY;
	public float m_maximumY;

	private float m_rotationX = 0F;
	private float m_rotationY = 0F;

	private GameObject m_panelMenu;

	
	void Start ()
	{
		/* Make the rigid body not change rotation */
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		/* Gets the menu containing all options the user can choose. */
		m_panelMenu = GameObject.Find ("panel_bottom_menu");
	}
	
	void Update ()
	{
		/* Computes the X and Y rotation from the current device used. */
		m_rotationX  = GetRotationXFromDevice() * m_sensitivityX;
		m_rotationY += GetRotationYFromDevice() * m_sensitivityY;

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
		
		m_panelMenu.transform.Rotate(new Vector3(0, 0, m_rotationX));
	}
	
	private float GetRotationXFromDevice()
	{
		switch (m_rotationInputDevice) 
		{
			case RotationInputDevice.Mouse:
				return Input.GetAxis ("Mouse X");
			case RotationInputDevice.Cardboard:
				// TODO
				return 0F;
			default:
				return 0F;
		}

	}

	private float GetRotationYFromDevice()
	{
		switch (m_rotationInputDevice) 
		{
			case RotationInputDevice.Mouse:
				return Input.GetAxis ("Mouse Y");
			case RotationInputDevice.Cardboard:
				// TODO
				return 0F;
			default:
				return 0F;
		}
	}
}