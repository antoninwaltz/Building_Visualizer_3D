using UnityEngine;
using System.Collections;

public class MenuInterbuttonsUpdater : MonoBehaviour {

	public GameObject m_interbuttonPanel;

	private Quaternion m_referenceQuaternion;
	public float m_buttonToInterButtonDistance;
	public float m_interButtonSwitchPadding;
	private float m_interButtonHalfInterval;

	// Use this for initialization
	void Start () {
		m_referenceQuaternion = Quaternion.Euler(Vector3.zero);
		m_interButtonHalfInterval = m_buttonToInterButtonDistance + m_interButtonSwitchPadding;
	}
	
	// Update is called once per frame
	void Update () {
		float angleFromReference = 0;
		if(transform.rotation.eulerAngles.y > 360-m_interButtonHalfInterval)
			angleFromReference = 360 - transform.rotation.eulerAngles.y;
		else
			angleFromReference = m_referenceQuaternion.eulerAngles.y - transform.rotation.eulerAngles.y;

		if (angleFromReference > m_interButtonHalfInterval || angleFromReference < -m_interButtonHalfInterval ) 
		{
			m_interbuttonPanel.transform.RotateAround(transform.position, Vector3.up, 2*m_buttonToInterButtonDistance);
			m_referenceQuaternion = m_interbuttonPanel.transform.rotation;
		}
	}
}
