using UnityEngine;
using System.Collections;

public class ThermostatOffButton : ThermostatButton {
	
	protected override void Initialize ()
	{
		m_type = ThermostatButtonType.OffButton;
	}

	public override void LaunchFunction ()
	{
		m_thermostatHandler.GetThermostat ().GetSelectedActionner ().isTurnedOn = false;
	}
}
