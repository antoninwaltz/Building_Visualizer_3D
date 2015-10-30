using UnityEngine;
using System.Collections;

public class PlayerMenuHandler : MonoBehaviour {
	
	public BodyMovement m_bodyMovement;
	
	public Camera m_leftUICamera;
	public Camera m_rightUICamera;

	public GameObject m_leftCursor;
	public GameObject m_rightCursor;
	
	public GameObject m_standOption;
	public GameObject m_walkOption;
	public GameObject m_runOption;
	private Vector3 m_optionSize;
	
	private GameObject m_previousPointedOption;
	private float m_selectTimer;
	private bool m_resetPreviousPointerOption;
	
	public static float s_optionHoverTimeLimit;
	
	// Use this for initialization
	void Start () {
		m_optionSize = Vector3.Scale(m_standOption.GetComponent<SpriteRenderer>().sprite.bounds.size, m_standOption.transform.localScale);
		m_selectTimer = 0F;
		PlayerMenuHandler.s_optionHoverTimeLimit = 1500F;
	}
	
	public void HandleMenuPointer()
	{
		m_resetPreviousPointerOption = false;

		Vector3 cursorPosition = m_leftUICamera.WorldToScreenPoint(m_leftCursor.transform.position);
		HandleStandOptionHover (cursorPosition);
		HandleWalkOptionHover (cursorPosition);
		HandleRunOptionHover (cursorPosition);
		
		if (m_resetPreviousPointerOption == true)
			m_previousPointedOption = null;

		/*
		Debug.Log ("---------------------");
		Debug.Log (m_previousPointedOption);
		Debug.Log (m_selectTimer);*/
	}
	
	private void HandleStandOptionHover(Vector3 _cursorPosition)
	{
		/*
		Bounds bo = new Bounds (m_leftUICamera.WorldToScreenPoint(b.transform.position), Vector3.Scale(b.GetComponent<SpriteRenderer>().sprite.bounds.size, b.transform.localScale));
		Debug.Log ("*************************");
		Debug.Log (bo.min.x + "/" + bo.max.x + " - " + bo.min.y + "/" + bo.max.y);
		Debug.Log (c);
		if (bo.min.x <= c.x && bo.min.y <= c.y && bo.max.x >= c.x && bo.max.y >= c.y)
		{
			Debug.Log("staaaaaaaaaand");
		}*/
			
		Bounds spriteBounds = new Bounds(m_leftUICamera.WorldToScreenPoint(m_standOption.transform.position), m_optionSize);

		if (spriteBounds.min.x <= _cursorPosition.x && spriteBounds.min.y <= _cursorPosition.y && spriteBounds.max.x >= _cursorPosition.x && spriteBounds.max.y >= _cursorPosition.y)
		{
			Debug.Log ("stand OK !");
			if (m_previousPointedOption == null || m_previousPointedOption != m_standOption)
			{
				m_previousPointedOption = m_standOption;
				m_selectTimer = 0F;
			}
			else
				m_selectTimer += Time.deltaTime;
			
			if (m_selectTimer >= PlayerMenuHandler.s_optionHoverTimeLimit)
			{
				m_bodyMovement.StopMoving ();
				//m_standOption.GetComponent<SpriteRenderer>().color.b = 
			}
			
			m_resetPreviousPointerOption |= false;
		}
		else
			m_resetPreviousPointerOption |= true;
	}
	
	private void HandleWalkOptionHover(Vector3 _cursorPosition)
	{
		Bounds spriteBounds = new Bounds(m_leftUICamera.WorldToScreenPoint(m_walkOption.transform.position), m_optionSize);

		if(m_resetPreviousPointerOption == true && spriteBounds.min.x <= _cursorPosition.x && spriteBounds.min.y <= _cursorPosition.y && spriteBounds.max.x >= _cursorPosition.x && spriteBounds.max.y >= _cursorPosition.y)
		{
			Debug.Log("walk OK !");
			if (m_previousPointedOption == null || m_previousPointedOption != m_walkOption)
			{
				m_previousPointedOption = m_walkOption;
				m_selectTimer = 0F;
			}
			else
				m_selectTimer += Time.deltaTime;
			
			if (m_selectTimer >= PlayerMenuHandler.s_optionHoverTimeLimit)
				m_bodyMovement.StartWalking();
			
			m_resetPreviousPointerOption |= false;
		}
		else
			m_resetPreviousPointerOption |= true;
	}
	
	private void HandleRunOptionHover(Vector3 _cursorPosition)
	{
		Bounds spriteBounds = new Bounds(m_leftUICamera.WorldToScreenPoint(m_runOption.transform.position), m_optionSize);

		if(m_resetPreviousPointerOption == true && spriteBounds.min.x <= _cursorPosition.x && spriteBounds.min.y <= _cursorPosition.y && spriteBounds.max.x >= _cursorPosition.x && spriteBounds.max.y >= _cursorPosition.y)
		{
			Debug.Log("run OK !");
			if (m_previousPointedOption == null || m_previousPointedOption != m_runOption)
			{
				m_previousPointedOption = m_runOption;
				m_selectTimer = 0F;
			}	
			else
				m_selectTimer += Time.deltaTime;
			
			if (m_selectTimer >= PlayerMenuHandler.s_optionHoverTimeLimit)
				m_bodyMovement.StartRunning();
			
			m_resetPreviousPointerOption |= false;
		}
		else
			m_resetPreviousPointerOption |= true;
	}
}
