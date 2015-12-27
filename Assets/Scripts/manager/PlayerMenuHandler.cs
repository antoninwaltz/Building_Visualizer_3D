using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum PlayerMenuOption {Stand = 0, Walk = 1, Run = 2, ShortestPath = 3, Volume = 4, ScientificMode = 5, RotateRight = 6, RotateLeft = 7};

public class PlayerMenuHandler : MonoBehaviour {
	
	public BodyMovement m_bodyMovement;
	public PathDrawingManager m_pathDrawer;

	private Dictionary<PlayerMenuOption, GameObject> m_menuOptions;
	public GameObject m_standOption;
	public GameObject m_walkOption;
	public GameObject m_runOption;
	public GameObject m_shPathOption;
	public GameObject m_volumeOnOption;
	public GameObject m_volumeOffOption;
	public GameObject m_scientificModeOption;
	public GameObject m_rotateRightOption;
	public GameObject m_rotateLeftOption;
	
	private GameObject m_currentPointedOption;
	private float m_currentHoverTimeLimit;

	private bool m_isHovering;
	private float m_selectTimer;
	
	public float m_optionHoverTimeLimit;
	private float m_interOptionHoverTimeLimit;

	public SimulationManager m_simulationManager;

	// Use this for initialization
	void Start () {
		m_selectTimer = 0F;
		m_currentPointedOption = null;
		m_interOptionHoverTimeLimit = m_optionHoverTimeLimit / 2;
		
		m_menuOptions = new Dictionary<PlayerMenuOption, GameObject> ();
		m_menuOptions.Add(PlayerMenuOption.Stand,m_standOption);
		m_menuOptions.Add(PlayerMenuOption.Walk,m_walkOption);
		m_menuOptions.Add(PlayerMenuOption.Run,m_runOption);
		m_menuOptions.Add(PlayerMenuOption.ShortestPath,m_shPathOption);
		m_menuOptions.Add(PlayerMenuOption.Volume,m_volumeOffOption);
		m_menuOptions.Add(PlayerMenuOption.ScientificMode,m_scientificModeOption);
		m_menuOptions.Add(PlayerMenuOption.RotateRight, m_rotateRightOption);
		m_menuOptions.Add(PlayerMenuOption.RotateLeft, m_rotateLeftOption);

		m_volumeOnOption.SetActive(false);
		m_standOption.GetComponent<Button> ().interactable = false;

		m_bodyMovement.SetMenuOptionDictionnary (m_menuOptions);
		foreach (GameObject o in m_menuOptions.Values) 
			ChangeAlphaFromImage(o.GetComponent<Image>(), 0);
	}
	
	public static void ChangeAlphaFromButton(Button _button, float _alpha)
	{
		Color c = _button.targetGraphic.color;
		c.a = _alpha;
		_button.targetGraphic.color = c;
	}

	
	public static void ChangeAlphaFromImage(Image _image, float _alpha)
	{
		Color c = _image.color;
		c.a = _alpha;
		_image.color = c;
	}

	public void EnteredMenuOption(int _option)
	{
		m_menuOptions.TryGetValue ((PlayerMenuOption)_option, out m_currentPointedOption);
		m_isHovering = true;
		if(m_currentPointedOption.GetComponent<Button>().IsInteractable())
			m_currentPointedOption.transform.GetChild (1).gameObject.SetActive (true);

		if (m_currentPointedOption.Equals (m_rotateLeftOption) || m_currentPointedOption.Equals (m_rotateRightOption))
			m_currentHoverTimeLimit = m_interOptionHoverTimeLimit;
		else
			m_currentHoverTimeLimit = m_optionHoverTimeLimit;
	}
	
	public void ExitedMenuOption()
	{
		if(m_currentPointedOption != null)
			resetTimerGauge(false);
		m_isHovering = false;
		m_selectTimer = 0F;
		m_currentPointedOption = null;
	}
	
	private void resetTimerGauge(bool _stillActive)
	{
		GameObject gaugePanel = m_currentPointedOption.transform.GetChild(1).gameObject;
		RectTransform fgRectTransform = gaugePanel.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
		fgRectTransform.offsetMax = new Vector2 (fgRectTransform.offsetMax.x, 0);
		gaugePanel.SetActive (_stillActive);
	}

	private void LateUpdate()
	{
		if (m_isHovering && m_currentPointedOption.GetComponent<Button>().IsInteractable()) 
		{
			m_selectTimer += Time.deltaTime;

			updateTimerGauge();

			if(m_selectTimer >= m_currentHoverTimeLimit)
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
		float updatedHeight = maxHeight - (m_selectTimer * maxHeight / m_currentHoverTimeLimit);
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
			resetTimerGauge(false);
		}
		else if(m_currentPointedOption.Equals (m_walkOption)) 
		{
			m_standOption.GetComponent<Button> ().interactable = true;
			m_walkOption.GetComponent<Button> ().interactable = false;
			m_runOption.GetComponent<Button> ().interactable = true;
			
			m_bodyMovement.StartWalking();
			resetTimerGauge(false);
		}
		else if(m_currentPointedOption.Equals (m_runOption))
		{
			m_standOption.GetComponent<Button> ().interactable = true;
			m_walkOption.GetComponent<Button> ().interactable = true;
			m_runOption.GetComponent<Button> ().interactable = false;
			
			m_bodyMovement.StartRunning();
			resetTimerGauge(false);
		}
		else if (m_currentPointedOption.Equals(m_shPathOption)) 
		{
			m_pathDrawer.DrawPath(m_bodyMovement);
		} 
		else if (m_currentPointedOption.Equals (m_volumeOnOption)) 
		{
			m_simulationManager.m_isSoundOn = true;

			m_volumeOnOption.SetActive(false);
			m_volumeOffOption.SetActive(true);
			m_currentPointedOption = m_volumeOffOption;
			m_menuOptions.Remove(PlayerMenuOption.Volume);
			m_menuOptions.Add(PlayerMenuOption.Volume, m_volumeOffOption);
			resetTimerGauge(true);
		} 
		else if (m_currentPointedOption.Equals (m_volumeOffOption)) 
		{
			m_simulationManager.m_isSoundOn = false;

			m_volumeOffOption.SetActive(false);
			m_volumeOnOption.SetActive(true);
			m_currentPointedOption = m_volumeOnOption;
			m_menuOptions.Remove(PlayerMenuOption.Volume);
			m_menuOptions.Add(PlayerMenuOption.Volume, m_volumeOnOption);
			resetTimerGauge(true);
		} 
		else if (m_currentPointedOption.Equals (m_scientificModeOption)) 
		{
			resetTimerGauge(true);
		}
		else if (m_currentPointedOption.Equals (m_rotateRightOption)) 
		{
			m_standOption.transform.parent.parent.RotateAround(m_standOption.transform.parent.position, Vector3.up, -60);
			resetTimerGauge(true);
		}
		else if (m_currentPointedOption.Equals (m_rotateLeftOption)) 
		{
			m_standOption.transform.parent.parent.RotateAround(m_standOption.transform.parent.position, Vector3.up, 60);
			resetTimerGauge(true);
		}
	}
}
