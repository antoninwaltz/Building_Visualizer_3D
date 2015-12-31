using UnityEngine;
using System.Collections;

public enum InteractableObjectType {Door = 0, Window = 1, Thermostat = 2, LightSwitch = 3, Television = 4}

public abstract class InteractableObject : MonoBehaviour {

	protected InteractableObjectType m_type;
	protected GameObject m_player;

	public float m_interactionDistance;

	private void Start()
	{
		m_player = GameObject.Find("player");
		Initialize ();
	}

	protected abstract void Initialize ();

	public abstract void Interaction(GameObject _player);

	public abstract void HandleInteractableObject();
}
