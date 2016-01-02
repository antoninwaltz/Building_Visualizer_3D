using UnityEngine;
using System.Collections;

public class MenuInterbuttonsUpdater : MonoBehaviour {

	public GameObject m_interbuttonPanel;
	private GameObject m_cardboardHead;
	public GameObject m_cardboardMain;

	private Quaternion m_referenceQuaternion;
	public float m_buttonToInterButtonDistance;
	public float m_interButtonSwitchPadding;
	private float m_interButtonHalfInterval;

	// Use this for initialization
	void Start () {
		m_cardboardHead = m_cardboardMain.transform.GetChild (0).gameObject;
		m_referenceQuaternion = Quaternion.Euler(Vector3.zero);
		m_interButtonHalfInterval = m_buttonToInterButtonDistance + m_interButtonSwitchPadding;
	}
	
	// Update is called once per frame
	void Update () {
		float angleFromReference = 0;
		if(m_cardboardHead.transform.rotation.eulerAngles.y > 360-m_interButtonHalfInterval)
			angleFromReference = 360 - m_cardboardHead.transform.rotation.eulerAngles.y;
		else
			angleFromReference = m_referenceQuaternion.eulerAngles.y - m_cardboardHead.transform.rotation.eulerAngles.y;

		if (angleFromReference > m_interButtonHalfInterval || angleFromReference < -m_interButtonHalfInterval ) 
		{
			m_interbuttonPanel.transform.RotateAround(m_cardboardMain.transform.position, Vector3.up, 2*m_buttonToInterButtonDistance);
			m_referenceQuaternion = m_interbuttonPanel.transform.rotation;
		}
	}
}
