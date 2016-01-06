using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum TelevisionButtonType {TeleportMenu = 0, Back = 1, NextCheckpoint = 2, PreviousCheckpoint = 3, CheckpointTeleport = 4}

public class TelevisionMenuHandler : MonoBehaviour {
	
	private TelevisionButton m_currentHoveredButton;
	public float m_timer;
	private float m_hoveredTime;
	private bool m_isHovering;

	public int m_originalCurrentCheckpointIndex;
	private int m_currentCheckpointIndex;
	public TelevisionCheckpointIndexUpdater m_checkpointIndexUpdater;
	
	// Use this for initialization
	void Start () {
		m_isHovering = false;
		m_hoveredTime = 0.0F;
		m_currentCheckpointIndex = m_originalCurrentCheckpointIndex;
		m_checkpointIndexUpdater.SetDisplay (m_originalCurrentCheckpointIndex.ToString());
	}

	// Update is called once per frame
	public void Interaction () {
		if(m_isHovering)
		{
			m_hoveredTime += Time.deltaTime;
			m_currentHoveredButton.GetFillableGauge().UpdateTimerGauge(m_hoveredTime, m_timer);
			if(m_hoveredTime > m_timer)
			{
				HandleTimerReached();
			}
		}
	}

	public void SetHoveredButton(TelevisionButton _button)
	{
		if (m_currentHoveredButton != null)
			HoveredButtonEnd ();
		
		m_currentHoveredButton = _button;
		m_isHovering = true;
		_button.GetFillableGauge ().ActivateTimerGauge ();
	}
	
	public void HoveredButtonEnd()
	{
		m_isHovering = false;
		m_hoveredTime = 0.0F;
		m_currentHoveredButton.GetFillableGauge ().ResetTimerGauge ();
		m_currentHoveredButton.GetFillableGauge ().DeactivateTimerGauge ();
		m_currentHoveredButton = null;
	}
	
	private void HandleTimerReached()
	{
		m_hoveredTime = 0.0F;
		m_currentHoveredButton.GetFillableGauge ().ResetTimerGauge ();
		
		m_currentHoveredButton.LaunchFunction();
	}

	public int GetCheckpointIndex()
	{
		return m_currentCheckpointIndex;
	}

	public void SetCheckpointIndex(int _index)
	{
		m_currentCheckpointIndex = _index;
		m_checkpointIndexUpdater.SetDisplay (m_currentCheckpointIndex.ToString ());
	}

	public void ResetCheckpointIndex()
	{
		SetCheckpointIndex (m_originalCurrentCheckpointIndex);
	}
}
