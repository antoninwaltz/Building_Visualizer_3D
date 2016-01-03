using UnityEngine;
using System.Collections;

public class BuildingPositionUpdater : MonoBehaviour {

	public int FloorIndex { get; private set;}

	public RoomContainer RoomContainer {get; private set;}

	void Start () {
		FloorIndex = 0;	
		RoomContainer = null;
	}


	public void UpdateRoomContainer(RoomContainer _rc)
	{
		RoomContainer = _rc;

		if (_rc != null) {
			Transform parentTransform = _rc.transform.parent.parent;
			int index = 0;
			int i = 0;
			while (!parentTransform.parent.GetChild (i).Equals (parentTransform)) {
				if (parentTransform.parent.GetChild (i).tag.Equals ("floor"))
					++index;
				++i;
			}
			UpdateFloor (index);
		} 
		else 
		{
			FloorIndex = -1;
		}
	}

	public void UpdateFloor(int _floorIndex)
	{
		FloorIndex = _floorIndex;
	}
}
