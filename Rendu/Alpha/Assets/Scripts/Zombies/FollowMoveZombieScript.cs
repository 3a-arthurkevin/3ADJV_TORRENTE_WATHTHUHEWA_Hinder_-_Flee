using UnityEngine;
using System.Collections;

public class FollowMoveZombieScript : MonoBehaviour {
	[SerializeField]
	private Transform m_zombiePos;

	[SerializeField]
	private Rigidbody m_rigidbody;

	[SerializeField]
	private Transform m_follow;

	[SerializeField]
	private float minMove = 2;

	[SerializeField]
	private float velocity = 2;

	public Transform Follow
	{
		get
		{
			return m_follow;
		}
		set
		{
			m_follow = value;
		}
	}

	void FixedUpdate()
	{//Follow Transform m_follow
		if(m_follow != null)
		{
			var direction = m_follow.position - m_zombiePos.position;
			direction = new Vector3(direction.x, 0, direction.z);

			if(direction.sqrMagnitude > minMove)
				m_rigidbody.velocity = direction.normalized * velocity;
		}
	}


}
