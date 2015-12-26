using UnityEngine;
using System.Collections;

public class TelevisionNextButton : TelevisionButton {

	private GameObject m_followingCamera;
	private GameObject m_checkpointContainer;

	protected override void Initialize()
	{
		m_followingCamera = GameObject.Find ("following_camera");
		m_checkpointContainer = GameObject.Find ("checkpoints");

		m_type = TelevisionButtonType.NextCheckpoint;
	}
		
	public override void LaunchFunction()
	{
		int newIndex = (m_televisionMenuHandler.GetCheckpointIndex () + 1) % m_checkpointContainer.transform.childCount;
		Debug.Log (newIndex);
		Vector3 newCheckpointCoords = m_checkpointContainer.transform.GetChild(newIndex).position;
		m_followingCamera.transform.position = new Vector3 (newCheckpointCoords.x, m_followingCamera.transform.position.y, newCheckpointCoords.z);

		m_televisionMenuHandler.SetCheckpointIndex (newIndex);
	}
}
