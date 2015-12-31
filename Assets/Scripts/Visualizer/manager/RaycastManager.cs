using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RaycastManager : MonoBehaviour {

	public Camera m_camera;
	public GameObject m_gazePointer;
	private int m_maskWithoutUI;

	// Use this for initialization
	void Start () {
		m_maskWithoutUI = m_camera.cullingMask;
		m_maskWithoutUI &= ~(1 << LayerMask.NameToLayer ("UI"));
	}

	void LateUpdate () {
		RaycastHit hitInfo = new RaycastHit ();
		Ray ray = new Ray (m_camera.transform.position, m_gazePointer.transform.position - m_camera.transform.position); 

		RaycastHit[] raycasts = Physics.RaycastAll (ray, Vector3.Distance (m_camera.transform.position, m_gazePointer.transform.position), m_maskWithoutUI);

		foreach (RaycastHit hit in raycasts) {
			//Debug.Log(hit.transform.gameObject.name);
			GameObject interactableObject = hit.transform.gameObject;
			float distance = 0;
			if (interactableObject.tag.Equals("interactable_object") || interactableObject.tag.Equals("interactable_object_child"))
			{
				if(interactableObject.tag.Equals("interactable_object_child"))
					while(!interactableObject.tag.Equals("interactable_object"))
						interactableObject = interactableObject.transform.parent.gameObject;

				distance = Vector3.Distance(m_camera.transform.position, interactableObject.transform.position);
				
				//Debug.Log (distance);
				//interactableObject.getComponent<AssociatedAction>().HasToInteract(distance);
				//Debug.Log("=> "+interactableObject.name);
			}
		}
	}
}
