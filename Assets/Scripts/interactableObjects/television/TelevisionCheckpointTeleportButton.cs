using UnityEngine;
using System.Collections;

public class TelevisionCheckpointTeleportButton : TelevisionButton {

	private GameObject m_panelFollowing;
	private GameObject m_panelCheckpoint;

	private GameObject m_followingCamera;
	private GameObject m_checkpointContainer;
	private GameObject m_player;
	private NavMeshAgent m_navMeshAgent;

	protected override void Initialize()
	{
		m_panelFollowing = transform.parent.gameObject;
		m_panelCheckpoint = transform.parent.parent.GetChild (2).gameObject;

		m_followingCamera = GameObject.Find ("following_camera");
		m_checkpointContainer = GameObject.Find ("checkpoints");
		m_player = GameObject.Find ("player");
		m_navMeshAgent = m_player.GetComponent<NavMeshAgent> ();

		m_type = TelevisionButtonType.CheckpointTeleport;
	}
		
	public override void LaunchFunction()
	{
		m_panelFollowing.SetActive(true);
		m_panelCheckpoint.SetActive(false);

		int checkpointIndex = m_televisionMenuHandler.GetCheckpointIndex();

		Debug.Log ("teleport to : "+checkpointIndex + m_checkpointContainer.transform.GetChild(checkpointIndex).transform.position);

		Vector3 checkpointCoords = m_checkpointContainer.transform.GetChild (checkpointIndex).position;
		m_navMeshAgent.Stop ();
		m_navMeshAgent.enabled = false;
		m_navMeshAgent.Warp(new Vector3 (checkpointCoords.x, checkpointCoords.y, checkpointCoords.z));
		m_navMeshAgent.enabled = true;
		m_followingCamera.transform.position = new Vector3 (m_player.transform.position.x, m_followingCamera.transform.position.y, m_player.transform.position.z);

		ButtonHoveredEnd ();

		m_player.transform.GetChild(0).gameObject.SetActive(true);
	}
}
