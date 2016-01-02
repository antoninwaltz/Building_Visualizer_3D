using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Thermostat : InteractableObject {

	private bool m_isWatching;
	private NavMeshAgent m_navMeshAgent;
	private BodyMovement m_bodyMovement;
	private InteractionAgent m_interactionAgent;
	private Transform m_cardboardTransform;
	private PlayerMenuHandler m_playerMenuHandler;
	private GameObject m_playerMenuCanvas;
	private GameObject m_thermostatScreenCanvas;

	private RoomContainer m_roomContainer;

	private float m_timer;
	public float m_timerLimit;

	private List<Actionner> m_commandableActionners;
	private int m_actionnerIndex;

	private Vector3 m_watchingDestination;

	protected override void Initialize()
	{
		m_isWatching = false;
		m_timer = 0.0F;

		m_navMeshAgent = m_player.gameObject.GetComponent<NavMeshAgent> ();
		m_navMeshAgent.updateRotation = false;
		m_bodyMovement = m_player.gameObject.GetComponent<BodyMovement> ();
		m_interactionAgent = m_player.gameObject.GetComponent<InteractionAgent> ();
		m_cardboardTransform = m_bodyMovement.m_cardboardHead.transform.parent;

		m_playerMenuHandler = GameObject.Find ("PlayerMenuManager").GetComponent<PlayerMenuHandler>();
		m_playerMenuCanvas = m_navMeshAgent.transform.GetChild (0).gameObject;
		m_thermostatScreenCanvas = transform.GetChild (6).GetChild(0).gameObject;

		m_watchingDestination = transform.GetChild (7).position;

		m_commandableActionners = new List<Actionner> ();
		for (int i = 0; i < transform.parent.childCount; ++i) 
		{
			Transform t = transform.parent.GetChild (i);
			if (t.tag.Equals ("commandableObject"))
				m_commandableActionners.Add(t.GetComponent<Actionner>());
		}
		m_roomContainer = transform.parent.parent.GetComponent<RoomContainer> ();
		m_actionnerIndex = 0;
		//InitializeUI();
	}

	private void InitializeUI()
	{
		/* room name */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (0).GetChild (1).GetComponent<Text> ().text = m_roomContainer.name;

		/* room temperature */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (0).GetChild (2).GetComponent<Text> ().text = m_roomContainer.CurrentTemperature.ToString();

		/* desired room temperature */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (0).GetChild (6).GetComponent<Text> ().text = m_roomContainer.ObjectiveTemperature.ToString ();

		/* actionner index */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (1).GetComponent<Text> ().text = (m_actionnerIndex + 1).ToString();

		/* nb actionners */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (3).GetComponent<Text> ().text = m_commandableActionners.Count.ToString();

		/* actionner name */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (1).GetChild (1).GetComponent<Text> ().text = m_commandableActionners.ToArray () [0].name;

		/* actionner type */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (2).GetChild (1).GetComponent<Text> ().text = m_commandableActionners.ToArray () [0].m_properties.ToString();

		/* actionner heat range*/
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (3).GetChild (2).GetComponent<Text> ().text = m_commandableActionners.ToArray () [0].MinDeliveredEnergy.ToString();
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (3).GetChild (4).GetComponent<Text> ().text = m_commandableActionners.ToArray () [0].MaxDeliveredEnergy.ToString();

		// TODO cost per hour

		/* current temperature delivered */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (5).GetChild (1).GetComponent<Text> ().text = m_commandableActionners.ToArray () [0].CurrentTemperature.ToString();

		/* current state */
		m_thermostatScreenCanvas.transform.GetChild (0).GetChild (1).GetChild (6).GetChild (1).GetComponent<Text> ().text = (m_commandableActionners.ToArray () [0].isTurnedOn)?"ON":"OFF";

	}

	public override void Interaction(GameObject _player)
	{
		float yDistance = Mathf.Abs((m_player.transform.position - transform.position).y);
		if (yDistance < 4) 
		{
			HandleSameFloorThermostat ();
		}
	}

	private void HandleSameFloorThermostat()
	{
		float distance = Vector2.Distance (new Vector2 (m_player.transform.position.x, m_player.transform.position.z), new Vector2 (transform.position.x, transform.position.z));
		if (distance <= m_interactionDistance) 
		{
			m_interactionAgent.SetInteractableObject (this);

			HandleInteractableObject();
		}
		else
		{
			ResetInteraction ();
		}
	}

	public override void HandleInteractableObject()
	{
		if(!m_isWatching)
		{
			m_timer += Time.deltaTime;
			if (m_timer > m_timerLimit) 
				HandleStartingToWatchThermostat ();
		}
		else
		{
			HandleWatchingThermostat ();
		}
	}

	private void HandleStartingToWatchThermostat()
	{
		m_playerMenuHandler.ExitedMenuOption ();

		m_playerMenuCanvas.SetActive (false);

		m_isWatching = true;
		m_bodyMovement.StopMoving ();
		m_playerMenuHandler.m_runOption.GetComponent<Button> ().interactable = true;
		m_playerMenuHandler.m_walkOption.GetComponent<Button> ().interactable = true;
		m_playerMenuHandler.m_standOption.GetComponent<Button> ().interactable = false;

		Vector3 destinationPath = new Vector3 (m_watchingDestination.x, m_player.transform.position.y, m_watchingDestination.z);
		m_navMeshAgent.SetDestination (m_watchingDestination);
		m_cardboardTransform.LookAt (transform.position);
	}

	private void HandleWatchingThermostat()
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (m_cardboardTransform.GetChild(0).GetChild(0).GetComponent<Camera> ());
		bool isRendered = GeometryUtility.TestPlanesAABB (planes, GetComponent<MeshRenderer> ().bounds);
		if (isRendered)
			m_playerMenuCanvas.SetActive (false);
		else
			m_playerMenuCanvas.SetActive (true);

		if (!m_bodyMovement.IsStanding ()) 
		{
			ResetInteraction ();
		}
	}

	private void ResetInteraction ()
	{
		m_timer = 0.0F;
		m_isWatching = false;

		m_playerMenuCanvas.SetActive (true);

		m_navMeshAgent.ResetPath ();
		m_interactionAgent.SetInteractableObject (null);
	}

	public int GetActionnerIndex()
	{
		return m_actionnerIndex;
	}

	public Actionner GetSelectedActionner()
	{
		return m_commandableActionners.ToArray () [m_actionnerIndex];
	}

	public RoomContainer GetRoomContainer()
	{
		return m_roomContainer;
	}

	public void ChooseNextActionner()
	{
		m_actionnerIndex = (m_actionnerIndex+1)%m_commandableActionners.Count;
	}

	public void ChoosePreviousActionner()
	{
		--m_actionnerIndex;
		if (m_actionnerIndex == -1)
			m_actionnerIndex = m_commandableActionners.Count - 1;
	}

	public List<Actionner> GetCommandableActionnerList()
	{
		return m_commandableActionners;
	}
}
