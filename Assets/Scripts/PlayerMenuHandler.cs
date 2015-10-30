using UnityEngine;
using System.Collections;

public class PlayerMenuHandler : MonoBehaviour {
	
	private BodyMovement m_bodyMovement;
	
	public Camera m_leftUICamera;
	public Camera m_rightUICamera;

	public GameObject m_leftCursor;
	
	public GameObject m_standLeftOption;
	public GameObject m_walkLeftOption;
	public GameObject m_runLeftOption;
	public GameObject m_standRightOption;
	public GameObject m_walkRightOption;
	public GameObject m_runRightOption;
	private Vector3 m_optionSize;
	
	private GameObject m_previousPointedOption;
	private float m_selectTimer;
	private bool m_resetPreviousPointerOption;
	
	public static float s_optionHoverTimeLimit;
	
	// Use this for initialization
	void Start () {
		m_bodyMovement = GetComponent<BodyMovement> ();
		m_optionSize = Vector3.Scale(m_standLeftOption.GetComponent<SpriteRenderer>().sprite.bounds.size, m_standLeftOption.transform.localScale);
		m_selectTimer = 0F;
		PlayerMenuHandler.s_optionHoverTimeLimit = 1.5F;
		m_standLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
		m_standRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
	}
	
	public void HandleMenuPointer()
	{
		Vector3 cursorPosition = m_leftUICamera.WorldToScreenPoint(m_leftCursor.transform.position);
		HandleStandOptionHover (cursorPosition);
		HandleWalkOptionHover (cursorPosition);
		HandleRunOptionHover (cursorPosition);
		
		if (m_resetPreviousPointerOption == true)
			m_previousPointedOption = null;
	}
	
	private void HandleStandOptionHover(Vector3 _cursorPosition)
	{			
		Bounds spriteBounds = new Bounds(m_leftUICamera.WorldToScreenPoint(m_standLeftOption.transform.position), m_optionSize);

		if (!m_bodyMovement.IsStanding() && spriteBounds.min.x <= _cursorPosition.x && spriteBounds.min.y <= _cursorPosition.y && spriteBounds.max.x >= _cursorPosition.x && spriteBounds.max.y >= _cursorPosition.y)
		{
			if (m_previousPointedOption == null || m_previousPointedOption != m_standLeftOption)
			{
				m_previousPointedOption = m_standLeftOption;
				m_selectTimer = 0F;
			}
			else
				m_selectTimer += Time.deltaTime;
			
			if (m_selectTimer >= PlayerMenuHandler.s_optionHoverTimeLimit)
			{
				m_bodyMovement.StopMoving ();
				m_standLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
				m_standRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
				m_walkLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_walkRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_runLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_runRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
			}
			
			m_resetPreviousPointerOption = false;
		}
		else
			m_resetPreviousPointerOption = true;
	}
	
	private void HandleWalkOptionHover(Vector3 _cursorPosition)
	{
		Bounds spriteBounds = new Bounds(m_leftUICamera.WorldToScreenPoint(m_walkLeftOption.transform.position), m_optionSize);

		if(m_resetPreviousPointerOption == true && !m_bodyMovement.IsWalking() && spriteBounds.min.x <= _cursorPosition.x && spriteBounds.min.y <= _cursorPosition.y && spriteBounds.max.x >= _cursorPosition.x && spriteBounds.max.y >= _cursorPosition.y)
		{
			if (m_previousPointedOption == null || m_previousPointedOption != m_walkLeftOption)
			{
				m_previousPointedOption = m_walkLeftOption;
				m_selectTimer = 0F;
			}
			else
				m_selectTimer += Time.deltaTime;
			
			if (m_selectTimer >= PlayerMenuHandler.s_optionHoverTimeLimit)
			{
				m_bodyMovement.StartWalking();
				m_standLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_standRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_walkLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
				m_walkRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
				m_runLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_runRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
			}
			
			m_resetPreviousPointerOption = false;
		}
	}
	
	private void HandleRunOptionHover(Vector3 _cursorPosition)
	{
		Bounds spriteBounds = new Bounds(m_leftUICamera.WorldToScreenPoint(m_runLeftOption.transform.position), m_optionSize);

		if(m_resetPreviousPointerOption == true && !m_bodyMovement.IsRunning() && spriteBounds.min.x <= _cursorPosition.x && spriteBounds.min.y <= _cursorPosition.y && spriteBounds.max.x >= _cursorPosition.x && spriteBounds.max.y >= _cursorPosition.y)
		{
			if (m_previousPointedOption == null || m_previousPointedOption != m_runLeftOption)
			{
				m_previousPointedOption = m_runLeftOption;
				m_selectTimer = 0F;
			}	
			else
				m_selectTimer += Time.deltaTime;
			
			if (m_selectTimer >= PlayerMenuHandler.s_optionHoverTimeLimit)
			{
				m_bodyMovement.StartRunning();
				m_standLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_standRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_walkLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_walkRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				m_runLeftOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
				m_runRightOption.GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.gray);
			}
			
			m_resetPreviousPointerOption = false;
		}
	}
}
