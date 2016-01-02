using UnityEngine;
using System.Collections;

public class ThermostatPlusButton : ThermostatButton {

	
	protected override void Initialize ()
	{
		m_type = ThermostatButtonType.PlusButton;
	}
	
	public override void LaunchFunction ()
	{
		++m_thermostatHandler.GetThermostat ().GetRoomContainer ().ObjectiveTemperature;
	}
}
