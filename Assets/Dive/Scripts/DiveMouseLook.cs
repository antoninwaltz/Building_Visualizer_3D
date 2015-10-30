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
	
	public GameObject m_panelRightBottomMenu;
	public GameObject m_panelLeftBottomMenu;

	public Camera m_leftCamera;
	public Camera m_rightCamera;
	
	public GameObject m_leftCursor;
	public GameObject m_rightCursor;

	public GameObject m_standOption;
	public GameObject m_walkOption;
	public GameObject m_runOption;
	private Vector3 m_optionSize;

	void Start ()
	{
		if (Application.platform == RuntimePlatform.Android)
			mouse_on=false;
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
			mouse_on=false;

		/* Make the rigid body not change rotation */
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		m_optionSize = Vector3.Scale(m_standOption.GetComponent<SpriteRenderer>().sprite.bounds.size, m_standOption.transform.localScale);
	}

	void Update ()
	{	
		if (mouse_on){

			HandleRotation();

			HandleMenuPointer();

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

	private void HandleMenuPointer()
	{
		HandleStandOptionHover ();

		HandleWalkOptionHover ();

		HandleRunOptionHover ();
	}

	private void HandleStandOptionHover()
	{
		Bounds spriteBounds = new Bounds(m_leftCamera.WorldToScreenPoint(m_standOption.transform.position), m_optionSize);
		Vector3 curorPosition = m_leftCamera.WorldToScreenPoint(m_leftCursor.transform.position);
		
		if(spriteBounds.min.x <= curorPosition.x && spriteBounds.min.y <= curorPosition.y && spriteBounds.max.x >= curorPosition.x && spriteBounds.max.y >= curorPosition.y)
			Debug.Log("stand OK !");
	}
	
	private void HandleWalkOptionHover()
	{
		Bounds spriteBounds = new Bounds(m_leftCamera.WorldToScreenPoint(m_walkOption.transform.position), m_optionSize);
		Vector3 curorPosition = m_leftCamera.WorldToScreenPoint(m_leftCursor.transform.position);
		
		if(spriteBounds.min.x <= curorPosition.x && spriteBounds.min.y <= curorPosition.y && spriteBounds.max.x >= curorPosition.x && spriteBounds.max.y >= curorPosition.y)
			Debug.Log("walk OK !");
	}
	
	private void HandleRunOptionHover()
	{
		Bounds spriteBounds = new Bounds(m_leftCamera.WorldToScreenPoint(m_runOption.transform.position), m_optionSize);
		Vector3 curorPosition = m_leftCamera.WorldToScreenPoint(m_leftCursor.transform.position);
		
		if(spriteBounds.min.x <= curorPosition.x && spriteBounds.min.y <= curorPosition.y && spriteBounds.max.x >= curorPosition.x && spriteBounds.max.y >= curorPosition.y)
			Debug.Log("run OK !");
	}
}