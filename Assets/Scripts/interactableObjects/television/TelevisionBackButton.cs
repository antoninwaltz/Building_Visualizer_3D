using UnityEngine;
using System.Collections;

public class TelevisionBackButton : TelevisionButton {

	private GameObject m_panelFollowing;
	private GameObject m_panelCheckpoint;

	private GameObject m_followingCamera;
	private GameObject m_player;

	protected override void Initialize()
	{
		m_panelCheckpoint = transform.parent.gameObject;
		m_panelFollowing = transform.parent.parent.GetChild (1).gameObject;
		
		m_followingCamera = GameObject.Find ("following_camera");
		m_player = GameObject.Find ("player");

		m_type = TelevisionButtonType.Back;
	}
		
	public override void LaunchFunction()
	{
		m_panelFollowing.SetActive(true);
		m_panelCheckpoint.SetActive(false);

		Vector3 checkpoint1Coords = m_player.transform.position;
		m_followingCamera.transform.position = new Vector3 (checkpoint1Coords.x, m_followingCamera.transform.position.y, checkpoint1Coords.z);
	}
}
