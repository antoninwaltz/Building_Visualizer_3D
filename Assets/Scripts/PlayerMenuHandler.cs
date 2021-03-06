﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum PlayerMenuOption {Stand = 0, Walk = 1, Run = 2, ShortestPath = 3, Volume = 4, ScientificMode = 5};

public class PlayerMenuHandler : MonoBehaviour {
	
	public BodyMovement m_bodyMovement;
	
	private Dictionary<PlayerMenuOption, GameObject> m_menuOptions;
	public GameObject m_standOption;
	public GameObject m_walkOption;
	public GameObject m_runOption;
	public GameObject m_shPathOption;
	public GameObject m_volumeOnOption;
	public GameObject m_volumeOffOption;
	public GameObject m_scientificModeOption;
	
	private GameObject m_currentPointedOption;
	
	private bool m_isHovering;
	private float m_selectTimer;
	
	public float m_optionHoverTimeLimit;
	
	// Use this for initialization
	void Start () {
		m_selectTimer = 0F;
		m_currentPointedOption = null;
		
		m_menuOptions = new Dictionary<PlayerMenuOption, GameObject> ();
		m_menuOptions.Add(PlayerMenuOption.Stand,m_standOption);
		m_menuOptions.Add(PlayerMenuOption.Walk,m_walkOption);
		m_menuOptions.Add(PlayerMenuOption.Run,m_runOption);
		m_menuOptions.Add(PlayerMenuOption.ShortestPath,m_shPathOption);
		m_menuOptions.Add(PlayerMenuOption.Volume,m_volumeOnOption);
		m_menuOptions.Add(PlayerMenuOption.ScientificMode,m_scientificModeOption);
		
		m_standOption.GetComponent<Button> ().interactable = false;
	}
	
	public void EnteredMenuOption(int _option)
	{
		m_menuOptions.TryGetValue ((PlayerMenuOption)_option, out m_currentPointedOption);
		m_isHovering = true;
		Debug.Log( m_currentPointedOption.name);
		m_currentPointedOption.transform.GetChild (1).gameObject.SetActive (true);
	}
	
	public void ExitedMenuOption()
	{
		resetTimerGauge();
		m_isHovering = false;
		m_selectTimer = 0F;
		m_currentPointedOption = null;
	}
	
	private void resetTimerGauge()
	{
		GameObject gaugePanel = m_currentPointedOption.transform.GetChild (1).gameObject;
		RectTransform fgRectTransform = gaugePanel.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
		RectTransform bgRectTransform = gaugePanel.transform.GetChild (0).gameObject.GetComponent<RectTransform>();
		fgRectTransform.sizeDelta = new Vector2 (0, gaugePanel.transform.parent.gameObject.GetComponent<RectTransform>().rect.height);
		gaugePanel.SetActive (false);
	}
	
	private void LateUpdate()
	{
		if (m_isHovering && m_currentPointedOption.GetComponent<Button>().IsInteractable()) 
		{
			m_selectTimer += Time.deltaTime;

			updateTimerGauge();

			if(m_selectTimer >= m_optionHoverTimeLimit)
			{
				handleTimerReached();
				m_selectTimer = 0F;
			}
		}
	}
	
	private void updateTimerGauge()
	{
		GameObject gaugePanel = m_currentPointedOption.transform.GetChild(1).gameObject;
		RectTransform fgRectTransform = gaugePanel.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
		float maxHeight = gaugePanel.transform.parent.gameObject.GetComponent<RectTransform> ().rect.height;
		float updatedHeight = maxHeight - (m_selectTimer * maxHeight / m_optionHoverTimeLimit);
		//fgRectTransform.sizeDelta = new Vector2 (0, updatedHeight);
		fgRectTransform.offsetMax = new Vector2 (fgRectTransform.offsetMax.x, -updatedHeight);
	}

	private void handleTimerReached()
	{
		if (m_currentPointedOption.Equals (m_standOption))
		{
			m_standOption.GetComponent<Button> ().interactable = false;
			m_walkOption.GetComponent<Button> ().interactable = true;
			m_runOption.GetComponent<Button> ().interactable = true;
			
			m_bodyMovement.StopMoving();
		}
		else if(m_currentPointedOption.Equals (m_walkOption)) 
		{
			m_standOption.GetComponent<Button> ().interactable = true;
			m_walkOption.GetComponent<Button> ().interactable = false;
			m_runOption.GetComponent<Button> ().interactable = true;
			
			m_bodyMovement.StartWalking();
		}
		else if(m_currentPointedOption.Equals (m_runOption))
		{
			m_standOption.GetComponent<Button> ().interactable = true;
			m_walkOption.GetComponent<Button> ().interactable = true;
			m_runOption.GetComponent<Button> ().interactable = false;
			
			m_bodyMovement.StartRunning();
		}
		else if (m_currentPointedOption.Equals (m_shPathOption)) 
		{
		} 
		else if (m_currentPointedOption.Equals (m_volumeOnOption)) 
		{
			m_volumeOnOption.SetActive(false);
			m_volumeOffOption.SetActive(true);
			m_currentPointedOption = m_volumeOffOption;
		} 
		else if (m_currentPointedOption.Equals (m_volumeOffOption)) 
		{
			m_volumeOnOption.SetActive(true);
			m_volumeOffOption.SetActive(false);
			m_currentPointedOption = m_volumeOnOption;
		} 
		else if (m_currentPointedOption.Equals (m_scientificModeOption)) 
		{
		}
	}
}
