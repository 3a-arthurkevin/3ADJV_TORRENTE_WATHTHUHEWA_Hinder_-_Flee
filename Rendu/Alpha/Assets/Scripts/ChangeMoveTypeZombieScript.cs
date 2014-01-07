using UnityEngine;
using System.Collections;

public class ChangeMoveTypeZombieScript : MonoBehaviour {
	[SerializeField]
	private MoveManagerZombie m_manager;

	private bool underFollow;

	void OnTriggerEnter(Collider col)
	{
		m_manager.Follow(col.transform);
	}

	void OnTriggerExit(Collider col)
	{
		m_manager.Unfollow();
		underFollow = false;
	}
}
