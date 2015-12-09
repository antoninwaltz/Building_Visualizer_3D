using UnityEngine;
using System.Collections;

public class OrientationChecker : MonoBehaviour {
	
	public float m_minXAngle;
	public float m_maxXAngle;
	
	private void Start()
	{
		m_minXAngle *= -1;
		m_maxXAngle = 180 + m_maxXAngle;
	}
	
	// Update is called once per frame
	private void LateUpdate () {
		float angle = GetUnityAngle ();
		
		Quaternion q = transform.rotation;
		q.eulerAngles = new Vector3(angle, q.eulerAngles.y, q.eulerAngles.z);
		transform.rotation = q;
	}
	
	private float GetUnityAngle()
	{
		float angle = transform.rotation.eulerAngles.x;
		if(angle < 180)
			return Mathf.Clamp (angle, 0, m_minXAngle);
		else 
			return Mathf.Clamp (angle, m_maxXAngle, 360);
	}
}
