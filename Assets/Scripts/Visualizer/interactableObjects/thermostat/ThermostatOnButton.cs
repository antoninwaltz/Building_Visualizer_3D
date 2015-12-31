using UnityEngine;
using System.Collections;

public class ThermostatOnButton : ThermostatButton {
	
	protected override void Initialize ()
	{
		m_type = ThermostatButtonType.OnButton;
	}
	
	public override void LaunchFunction ()
	{
		
	}
}
