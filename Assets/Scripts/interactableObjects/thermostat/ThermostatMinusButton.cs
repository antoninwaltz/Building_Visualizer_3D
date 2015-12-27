using UnityEngine;
using System.Collections;

public class ThermostatMinusButton : ThermostatButton {
	
	protected override void Initialize ()
	{
		m_type = ThermostatButtonType.MinusButton;
	}
	
	public override void LaunchFunction ()
	{
		
	}
}
