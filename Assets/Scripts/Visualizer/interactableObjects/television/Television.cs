using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Television : InteractableObject {

	private bool m_isWatching;
	private NavMeshAgent m_navMeshAgent;
	private BodyMovement m_bodyMovement;
	private InteractionAgent m_interactionAgent;
	private Transform m_cardboardTransform;
	private PlayerMenuHandler m_playerMenuHandler;
	private TelevisionsInteractionDispatcher m_dispatcher;
	private GameObject m_playerMenuCanvas;

	private float m_timer;
	public float m_timerLimit;
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
		m_dispatcher = GameObject.Find ("TelevisionInteractionDispatcher").GetComponent<TelevisionsInteractionDispatcher> ();
		m_playerMenuCanvas = m_navMeshAgent.transform.GetChild (0).gameObject;

		m_watchingDestination = transform.GetChild (1).position;
	}

	public override void Interaction(GameObject _player)
	{
		m_dispatcher.SetInteractingTelevision (this);
		HandleSameFloorTelevision ();
	}

	private void HandleSameFloorTelevision()
	{
		float distance = Vector2.Distance (new Vector2 (m_player.transform.position.x, m_player.transform.position.z), new Vector2 (transform.position.x, transform.position.z));
		if (distance <= m_interactionDistance) 
		{
			m_interactionAgent.SetInteractableObject (this);

			HandleInteractableObject ();
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
				HandleStartingToWatchTelevision ();
		}
		else 
		{
			HandleWatchingTelevision ();
		}
	}

	private void HandleStartingToWatchTelevision()
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
		//m_cardboardTransform.LookAt (transform.position);
	}

	private void HandleWatchingTelevision()
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
}
