using UnityEngine;
using System.Collections;

public enum DimensionFit { Width = 0, Height = 1, Depth = 2 }

public class FillableGauge : MonoBehaviour {

	private GameObject m_parentObject;
	private GameObject m_gaugesPanel;
	private RectTransform m_fgRectTransform;
	private float m_maxSize;

	public DimensionFit m_dimensionFit;

	// Use this for initialization
	void Start () {
		m_gaugesPanel = transform.gameObject;
		m_fgRectTransform = m_gaugesPanel.transform.GetChild(1).gameObject.GetComponent<RectTransform>();

		switch(m_dimensionFit)
		{
			case DimensionFit.Width:
				m_maxSize = m_gaugesPanel.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
				break;
			case DimensionFit.Height:
				m_maxSize = m_gaugesPanel.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
				break;
			case DimensionFit.Depth:
				//
				break;
		}

		gameObject.SetActive (false);
	}
	
	public void UpdateTimerGauge(float _timer, float _limit)
	{
		float updatedHeight = m_maxSize - (_timer * m_maxSize / _limit);
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
