using UnityEngine;
using System.Collections;

public class MoveManagerZombieScriptOld : MonoBehaviour {
	[SerializeField]
	private FollowMoveZombieScript m_followScript;

	[SerializeField]
	private RandomMoveZombieScript m_randomScript;

    [SerializeField]
    private int m_atIsFloor;

    public int AtIsFloor
    {
        get
        {
            return m_atIsFloor;
        }
        set
        {
            m_atIsFloor = value;
        }
    }

	void Awake()
	{
		m_followScript.Follow = null;
		m_followScript.enabled = false;
		m_randomScript.enabled = true;
	}

	public void Follow(Transform t)
	{
		m_followScript.Follow = t;
		m_followScript.enabled = !m_followScript.enabled;
		m_randomScript.enabled = !m_randomScript.enabled;
	}

	public void UnFollow()
	{
		m_followScript.Follow = null;
		m_followScript.enabled = !m_followScript.enabled;
		m_randomScript.enabled = !m_randomScript.enabled;
	}
}
