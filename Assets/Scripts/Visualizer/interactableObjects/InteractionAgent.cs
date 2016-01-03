using UnityEngine;
using System.Collections;

public class InteractionAgent : MonoBehaviour {

	private InteractableObject m_interactableObject;

	// Use this for initialization
	void Start () {
		m_interactableObject = null;
	}

	public void SetInteractableObject(InteractableObject _interactableObject)
	{
		m_interactableObject = _interactableObject;
	}

	public InteractableObject GetInteractableObject()
	{
		return m_interactableObject;
	}
}
