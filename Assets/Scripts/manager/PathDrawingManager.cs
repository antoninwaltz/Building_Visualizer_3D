using UnityEngine;
using System.Collections;

public class PathDrawingManager : MonoBehaviour {

	public GameObject m_checkpointContainer;

	private LineRenderer m_lineRenderer;

	private void Start()
	{
		m_lineRenderer = GetComponent<LineRenderer> ();
	}

	public void DrawPath(BodyMovement _bodyMovement)
	{
		int i = 0;
		bool stop = false;
		float minDistance = float.MaxValue;
		NavMeshPath minPath = null;
		
		while(i < m_checkpointContainer.transform.childCount && !stop)
		{
			Transform checkpointPosition = m_checkpointContainer.transform.GetChild(i);
			NavMeshAgent agent = _bodyMovement.gameObject.GetComponent<NavMeshAgent>();
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(checkpointPosition.position, path);
			float distance = 0;
			if(path.corners.Length == 1)
				distance = Vector3.Distance(checkpointPosition.position, _bodyMovement.gameObject.transform.position);
			else
			{
				distance = Vector3.Distance(path.corners[0], _bodyMovement.gameObject.transform.position);
				for(int j = 1; j < path.corners.Length; ++j)
					distance += Vector3.Distance(path.corners[j-1], path.corners[j]);
			}
			
			if(minDistance > distance)
			{
				minPath = path;
				minDistance = distance;
			}
			++i;
		}

	
		m_lineRenderer.SetPosition(0, _bodyMovement.gameObject.transform.position);
		m_lineRenderer.SetVertexCount(minPath.corners.Length);
		for(i = 1; i < minPath.corners.Length; ++i)
			m_lineRenderer.SetPosition(i, minPath.corners[i]);
	}
}
