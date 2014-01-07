using UnityEngine;
using System.Collections;

public class FollowMoveZombieScript : MonoBehaviour {
	[SerializeField]
	private Transform m_transform;

	[SerializeField]
	private Transform m_follow;

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

	void Start ()
	{
	
	}
	

	void Update ()
	{

	}

	void FixedUpdate()
	{//Follow Transform m_follow
	}


}
