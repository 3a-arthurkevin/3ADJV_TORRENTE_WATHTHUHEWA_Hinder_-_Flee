using UnityEngine;
using System.Collections;

public class FollowMoveZombieScript : MonoBehaviour {

    [SerializeField]
	private Transform m_zombiePos;

	[SerializeField]
	private Transform m_follow;

    [SerializeField]
    private float m_minDistance = 2;

    [SerializeField]
    private float m_speed = 2;

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
			direction.Set(direction.x, 0, direction.z);

            if (direction.sqrMagnitude > m_minDistance)
                m_zombiePos.position += direction.normalized * m_speed * Time.deltaTime;

            else
                m_follow = null;
		}
	}


}
