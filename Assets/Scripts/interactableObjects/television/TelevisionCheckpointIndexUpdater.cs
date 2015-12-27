using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TelevisionCheckpointIndexUpdater : MonoBehaviour {

	private Text m_text;

	private string m_display;

	// Use this for initialization
	void Start () {
		m_text = GetComponent<Text> ();
		m_display = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_text.text = m_display;
	}

	public void SetDisplay(string _display)
	{
		m_display = _display;
	}
}
