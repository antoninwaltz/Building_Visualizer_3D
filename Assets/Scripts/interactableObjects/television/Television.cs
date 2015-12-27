using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Television : InteractableObject {

	private bool m_isWatching;
	private NavMeshAgent m_navMeshAgent;
	private BodyMovement m_bodyMovement;
	private Transform m_cameraTransform;
	private Vector3 m_agentWatchingDestination;
	private PlayerMenuHandler m_playerMenuHandler;
	private GameObject m_playerMenuCanvas;

	private float m_timer;
	public float m_timerLimit;

	protected override void Initialize()
	{
		m_isWatching = false;
		m_timer = 0.0F;

		m_navMeshAgent = m_player.gameObject.GetComponent<NavMeshAgent> ();
		m_navMeshAgent.updateRotation = false;
		m_bodyMovement = m_player.gameObject.GetComponent<BodyMovement> ();
		m_cameraTransform = m_bodyMovement.m_mainCamera.transform;

		m_playerMenuHandler = GameObject.Find ("PlayerMenuManager").GetComponent<PlayerMenuHandler>();
		m_playerMenuCanvas = m_navMeshAgent.transform.GetChild (0).gameObject;
		m_agentWatchingDestination = new Vector3 (transform.position.x, transform.position.y, (transform.position.z - 1.5F));
	}

	private void Update()
	{
		float yDistance = Mathf.Abs((m_player.transform.position - transform.position).y);
		if (yDistance < 4) 
		{
			HandleSameFloorTelevision ();
		}
	}

	private void HandleSameFloorTelevision()
	{
		float distance = Vector2.Distance (new Vector2 (m_player.transform.position.x, m_player.transform.position.z), new Vector2 (transform.position.x, transform.position.z));
		Debug.Log (distance + " <=? " + m_interactionDistance);
		if (distance <= m_interactionDistance) 
		{
			m_timer += Time.deltaTime;
			HandleNearbyTelevision ();
		}
	}

	private void HandleNearbyTelevision()
	{
		if (!m_isWatching && m_timer > m_timerLimit) 
		{
			HandleStartingToWatchTelevision ();
		}

		if (m_isWatching) 
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

		Vector3 destinationPath = new Vector3 (m_agentWatchingDestination.x, m_player.transform.position.y, m_agentWatchingDestination.z);
		m_navMeshAgent.SetDestination (m_agentWatchingDestination);
		m_cameraTransform.LookAt (transform.position);
	}

	private void HandleWatchingTelevision()
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (m_cameraTransform.GetComponent<Camera> ());
		bool isRendered = GeometryUtility.TestPlanesAABB (planes, GetComponent<MeshRenderer> ().bounds);
		if (isRendered)
			m_playerMenuCanvas.SetActive (false);
		else
			m_playerMenuCanvas.SetActive (true);

		if (!m_bodyMovement.IsStanding ()) 
		{
			Debug.Log ("moooooooooooooooooooooooooooooooving");
			m_timer = 0.0F;
			m_isWatching = false;

			m_playerMenuCanvas.SetActive (true);

			m_navMeshAgent.ResetPath ();
		}
	}
}
