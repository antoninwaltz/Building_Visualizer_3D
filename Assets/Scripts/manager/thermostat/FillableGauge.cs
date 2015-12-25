using UnityEngine;
using System.Collections;

public class FillableGauge : MonoBehaviour {

	private GameObject m_parentObject;
	private GameObject m_gaugesPanel;
	private RectTransform m_fgRectTransform;
	private float m_maxHeight;

	// Use this for initialization
	void Start () {
		m_gaugesPanel = transform.GetChild(0).gameObject;
		m_fgRectTransform = m_gaugesPanel.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
		m_maxHeight = m_gaugesPanel.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
		gameObject.SetActive (false);
	}
	
	public void UpdateTimerGauge(float _timer, float _limit)
	{
		float updatedHeight = m_maxHeight - (_timer * m_maxHeight / _limit);
		m_fgRectTransform.offsetMax = new Vector2 (m_fgRectTransform.offsetMax.x, -updatedHeight);
	}

	public void ResetTimerGauge()
	{
		m_fgRectTransform.offsetMax = new Vector2 (m_fgRectTransform.offsetMax.x, 0);
	}

	public void ActivateTimerGauge ()
	{
		gameObject.SetActive (true);
	}

	public void DeactivateTimerGauge()
	{
		gameObject.SetActive (false);
	}
}
