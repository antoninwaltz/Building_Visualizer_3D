using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public RoomContainer m_roomContainer;
	public int FloorIndex {get; private set;}

	void Start () {
		Transform parentTransform = m_roomContainer.transform.parent.parent;
		int index = 0;
		int i = 0;
		while (!parentTransform.parent.GetChild (i).Equals (parentTransform)) 
		{
			if(parentTransform.parent.GetChild(i).tag.Equals("floor"))
				++index;
			++i;
		}
		FloorIndex = index;
	}
}
