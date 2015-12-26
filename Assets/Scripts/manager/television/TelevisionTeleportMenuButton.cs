using UnityEngine;
using System.Collections;

public class TelevisionTeleportMenuButton : TelevisionButton {

	private GameObject m_panelFollowing;
	private GameObject m_panelCheckpoint;

	private GameObject m_followingCamera;
	private GameObject m_checkpointContainer;

	protected override void Initialize()
	{
		m_panelFollowing = transform.parent.gameObject;
		m_panelCheckpoint = transform.parent.parent.GetChild (2).gameObject;

		m_followingCamera = GameObject.Find ("following_camera");
		m_checkpointContainer = GameObject.Find ("checkpoints");

		m_type = TelevisionButtonType.TeleportMenu;
	}
		
	public override void LaunchFunction()
	{
		m_panelFollowing.SetActive(false);
		m_panelCheckpoint.SetActive(true);
		
		m_televisionMenuHandler.ResetCheckpointIndex ();

		Vector3 checkpoint1Coords = m_checkpointContainer.transform.GetChild (0).position;
		m_followingCamera.transform.position = new Vector3 (checkpoint1Coords.x, m_followingCamera.transform.position.y, checkpoint1Coords.z);
	}
}
