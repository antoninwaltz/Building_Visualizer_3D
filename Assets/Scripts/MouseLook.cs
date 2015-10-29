using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes m_axes = RotationAxes.MouseXAndY;
	public float m_sensitivityX;
	public float m_sensitivityY ;
	
	public float m_minimumX;
	public float m_maximumX;
	
	public float m_minimumY;
	public float m_maximumY;
	
	float m_rotationY = 0F;
	
	void Update ()
	{
		if (m_axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
			
			m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
			m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
			
			transform.localEulerAngles = new Vector3(-m_rotationY, rotationX, 0);
		}
		else if (m_axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * m_sensitivityX, 0);
		}
		else
		{
			m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
			m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
			
			transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
		}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
}