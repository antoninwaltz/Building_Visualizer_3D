using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class TelevisionButton : MonoBehaviour {

	protected TelevisionMenuHandler m_televisionMenuHandler;
	protected TelevisionButtonType m_type;
	protected FillableGauge m_fillableGauge;

	// Use this for initialization
	void Start () {
		m_televisionMenuHandler = transform.parent.parent.parent.GetComponent<TelevisionMenuHandler> ();
		m_fillableGauge = transform.GetChild (0).GetComponent<FillableGauge> ();	
		Initialize ();
	}
	
	protected abstract void Initialize ();
		
	public void ButtonHovered()
	{
		m_televisionMenuHandler.SetHoveredButton(this);
	}
	
	
	public void ButtonHoveredEnd()
	{
		m_televisionMenuHandler.HoveredButtonEnd ();
	}
	
	public FillableGauge GetFillableGauge()
	{
		return m_fillableGauge;
	}

	public abstract void LaunchFunction();
}
