using UnityEngine;
using System.Collections;

public enum InteractableObjectType {Door = 0, Window = 1, Thermostat = 2, LightSwitch = 3}

public class InteractableObject : MonoBehaviour {

	public InteractableObjectType m_type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
