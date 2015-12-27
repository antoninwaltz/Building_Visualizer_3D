using UnityEngine;
using System.Collections;

public class ThermostatLeftButton : ThermostatButton {
	
	protected override void Initialize ()
	{
		m_type = ThermostatButtonType.LeftButton;
	}
	
	public override void LaunchFunction ()
	{
		
	}
}
