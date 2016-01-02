using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThermostatCanvasTextUpdater : MonoBehaviour {

	private Text m_roomName;
	private Text m_roomTemperature;
	private Text m_objectiveTemperature;
	private Text m_actionnerIndex;
	private Text m_actionnerNumber;
	private Text m_actionnerName;
	private Text m_actionnerType;
	private Text m_actionnerMinHeat;
	private Text m_actionnerMaxHeat;
	private Text m_actionnerTemperature;
	private Text m_actionnerState;

	private ThermostatHandler m_thermostatHandler;

	// Use this for initialization
	void Start () {
		m_roomName = transform.GetChild (0).GetChild (0).GetChild (1).GetComponent<Text> ();
		m_roomTemperature = transform.GetChild (0).GetChild (0).GetChild (2).GetComponent<Text> ();
		m_objectiveTemperature = transform.GetChild (0).GetChild (0).GetChild (6).GetComponent<Text> ();
		m_actionnerIndex = transform.GetChild (0).GetChild (1).GetChild (0).GetChild (1).GetComponent<Text> ();
		m_actionnerNumber = transform.GetChild (0).GetChild (1).GetChild (0).GetChild (3).GetComponent<Text> ();
		m_actionnerName = transform.GetChild (0).GetChild (1).GetChild (1).GetChild (1).GetComponent<Text> ();
		m_actionnerType = transform.GetChild (0).GetChild (1).GetChild (2).GetChild (1).GetComponent<Text> ();
		m_actionnerMinHeat = transform.GetChild (0).GetChild (1).GetChild (3).GetChild (2).GetComponent<Text> ();
		m_actionnerMaxHeat = transform.GetChild (0).GetChild (1).GetChild (3).GetChild (4).GetComponent<Text> ();
		m_actionnerTemperature = transform.GetChild (0).GetChild (1).GetChild (5).GetChild (1).GetComponent<Text> ();
		m_actionnerState = transform.GetChild (0).GetChild (1).GetChild (6).GetChild (1).GetComponent<Text> ();

		m_thermostatHandler = transform.parent.parent.GetComponent<ThermostatHandler> ();
	}
	
	// Update is called once per frame
	void Update () {

		/* room name */
		m_roomName.text = m_thermostatHandler.GetThermostat().GetRoomContainer().name;

		/* room temperature */
		m_roomTemperature.text = m_thermostatHandler.GetThermostat().GetRoomContainer().CurrentTemperature.ToString();

		/* desired room temperature */
		m_objectiveTemperature.text = m_thermostatHandler.GetThermostat().GetRoomContainer().ObjectiveTemperature.ToString ();

		/* actionner index */
		m_actionnerIndex.text = (m_thermostatHandler.GetThermostat().GetActionnerIndex()+1).ToString();

		/* nb actionners */
		m_actionnerNumber.text = m_thermostatHandler.GetThermostat().GetCommandableActionnerList().Count.ToString();

		/* actionner name */
		m_actionnerName.text = m_thermostatHandler.GetThermostat().GetSelectedActionner().name;

		/* actionner type */
		m_actionnerType.text = m_thermostatHandler.GetThermostat().GetSelectedActionner().m_properties.ToString();

		/* actionner heat range*/
		m_actionnerMinHeat.text = m_thermostatHandler.GetThermostat().GetSelectedActionner().MinDeliveredEnergy.ToString();
		m_actionnerMaxHeat.text = m_thermostatHandler.GetThermostat().GetSelectedActionner().MaxDeliveredEnergy.ToString();

		// TODO cost per hour

		/* current temperature delivered */
		m_actionnerTemperature.text = m_thermostatHandler.GetThermostat().GetSelectedActionner().CurrentTemperature.ToString();

		/* current state */
		m_actionnerState.text = (m_thermostatHandler.GetThermostat().GetSelectedActionner().isTurnedOn)?"ON":"OFF";
	}
}
