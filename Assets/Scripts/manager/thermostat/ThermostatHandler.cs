using UnityEngine;
using System.Collections;

public enum ThermostatButtonType {RightButton = 0, LeftButton = 1, PlusButton = 2, MinusButton = 3, OnButton = 4, OffButton = 5}

public class ThermostatHandler : MonoBehaviour {

	private ThermostatButton m_currentHoveredButton;
	public float m_timer;
	private float m_hoveredTime;
	private bool m_isHovering;

	// Use this for initialization
	void Start () {
		m_isHovering = false;
		m_hoveredTime = 0.0F;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_isHovering)
		{
			m_hoveredTime += Time.deltaTime;
			if(m_hoveredTime > m_timer)
			{
				HandleTimerReached();
			}
		}
	}

	public void SetHoveredButton(ThermostatButton _button)
	{
		m_currentHoveredButton = _button;
		m_isHovering = true;
	}

	public void HoveredButtonEnd()
	{
		m_isHovering = false;
		m_hoveredTime = 0.0F;
	}

	private void HandleTimerReached()
	{
		Debug.Log ("timer reached !");
		m_hoveredTime = 0.0F;
	}
}
