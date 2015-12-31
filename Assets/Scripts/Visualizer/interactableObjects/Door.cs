using UnityEngine;
using System.Collections;

public class Door : InteractableObject{

    private Animator m_animator;
    private bool m_doorOpen;

    protected override void Initialize()
    {
		m_type = InteractableObjectType.Door;
		m_doorOpen = false;
		m_animator = GetComponent<Animator>();
		//m_animator.speed *= -1.0F;
    }

	public override void Interaction(GameObject _player)
    {
		float distance = Vector3.Distance(_player.transform.position, transform.position);

		if (distance < m_interactionDistance && !m_doorOpen)
        {
			m_doorOpen = true;
			Debug.Log("Open");
            DoorControl("Open");
        }
		if (distance >= m_interactionDistance && m_doorOpen)
        {
			m_doorOpen = false;
			Debug.Log("Close");
            DoorControl("Close");
        }

    }

    private void DoorControl(string direction)
    {
		m_animator.SetTrigger(direction);
    }

	public override void HandleInteractableObject()
	{
	}
}
