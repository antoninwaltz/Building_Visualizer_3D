using UnityEngine;
using System.Collections;

public abstract class ThermostatButton : MonoBehaviour {
	
	protected ThermostatHandler m_thermostatHandler;
	protected ThermostatButtonType m_type;
	protected FillableGauge m_fillableGauge;

	// Use this for initialization
	void Start () {
		m_thermostatHandler = transform.parent.gameObject.GetComponent<ThermostatHandler> ();
		m_fillableGauge = transform.GetChild (0).GetChild(0).GetComponent<FillableGauge> ();	
		Initialize ();
	}

	protected abstract void Initialize ();

	public void ButtonHovered()
	{
		m_thermostatHandler.SetHoveredButton(this);
	}
		
	public void ButtonHoveredEnd()
	{
		m_thermostatHandler.HoveredButtonEnd ();
	}

	public FillableGauge GetFillableGauge()
	{
		return m_fillableGauge;
	}

	public abstract void LaunchFunction ();
}
