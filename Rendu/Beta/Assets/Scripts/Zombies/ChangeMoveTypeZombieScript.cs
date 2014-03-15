using UnityEngine;
using System.Collections;

public class ChangeMoveTypeZombieScript : MonoBehaviour {
	[SerializeField]
	private MoveManagerZombieScript m_manager;

	void OnTriggerEnter(Collider col)
	{
		//m_manager.Follow(col.transform);
	}

	void OnTriggerExit(Collider col)
	{
		//m_manager.UnFollow();
	}
}
