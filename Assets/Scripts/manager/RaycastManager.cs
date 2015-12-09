using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RaycastManager : MonoBehaviour {

	private List<GameObject> m_interactableObjects;
	public Camera m_camera;
	public GameObject m_gazePointer;

	// Use this for initialization
	void Start () {
		m_interactableObjects = GameObject.FindGameObjectsWithTag ("interactable_object").Cast<GameObject>().ToList();
		foreach (GameObject o in m_interactableObjects)
			Debug.Log (o.name);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		RaycastHit hitInfo = new RaycastHit ();
		Ray ray = new Ray (m_camera.transform.position, m_gazePointer.transform.position - m_camera.transform.position); 
			//m_camera.ScreenPointToRay(m_gazePointer.transform.position);

		RaycastHit[] raycasts = Physics.RaycastAll (ray, Vector3.Distance (m_camera.transform.position, m_gazePointer.transform.position));
		foreach (RaycastHit hit in raycasts) 
		//if (Physics.Raycast(ray, out hitInfo)) 
		{
			if (hit.transform.gameObject.tag == "interactable_object") {
				GameObject interactableObject = hit.transform.gameObject;
				float distance = hit.distance;

				Debug.Log (distance);
			}
		}/*
		else
		{
			Debug.Log("No objects");
		}*/
	}
}
