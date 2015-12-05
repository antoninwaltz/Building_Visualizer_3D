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
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo = new RaycastHit();
		Debug.Log (m_gazePointer.transform.position + " / "+ m_interactableObjects.ElementAt(0).transform.position);

		Ray ray = m_camera.ScreenPointToRay (m_gazePointer.transform.position);
		Debug.DrawRay (ray.origin, ray.direction, Color.green);
		//if (Physics.Raycast(ray, out hitInfo)) 
		RaycastHit[] raycasts = Physics.RaycastAll(ray, Vector3.Distance(m_camera.transform.position, m_gazePointer.transform.position));
		string s = "";
		foreach (RaycastHit hit in raycasts) 
		{
			s += " | "+ hit.transform.gameObject.name;
		}
		Debug.Log (s);

		foreach(RaycastHit hit in raycasts)
		{
			if (hit.transform.gameObject.tag == "interactable_object")
			{
				GameObject interactableObject = hitInfo.transform.gameObject;

				float distance = hitInfo.distance;

				Debug.Log(distance);
			}
		}
	}
}
