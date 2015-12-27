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
			m_currentHoveredButton.GetFillableGauge().UpdateTimerGauge(m_hoveredTime, m_timer);
			if(m_hoveredTime > m_timer)
			{
				HandleTimerReached();
			}
		}
	}

	public void SetHoveredButton(ThermostatButton _button)
	{
		if (m_currentHoveredButton != null)
			HoveredButtonEnd ();

		m_currentHoveredButton = _button;
		m_isHovering = true;
		_button.GetFillableGauge ().ActivateTimerGauge ();
	}

	public void HoveredButtonEnd()
	{
		m_isHovering = false;
		m_hoveredTime = 0.0F;
		m_currentHoveredButton.GetFillableGauge ().ResetTimerGauge ();
		m_currentHoveredButton.GetFillableGauge ().DeactivateTimerGauge ();
		m_currentHoveredButton = null;
	}

	private void HandleTimerReached()
	{
		m_hoveredTime = 0.0F;
		m_currentHoveredButton.GetFillableGauge ().ResetTimerGauge ();

		m_currentHoveredButton.LaunchFunction();
	}
}
