using UnityEngine;
using System.Collections;

public abstract class ThermostatButton : MonoBehaviour {
	
	protected ThermostatHandler m_thermostatHandler;
	protected ThermostatButtonType m_type;

	// Use this for initialization
	void Start () {
		m_thermostatHandler = transform.parent.gameObject.GetComponent<ThermostatHandler> ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonHovered()
	{
		m_thermostatHandler.SetHoveredButton(this);
	}
	
	
	public void ButtonHoveredEnd()
	{
		m_thermostatHandler.HoveredButtonEnd ();
	}
}
