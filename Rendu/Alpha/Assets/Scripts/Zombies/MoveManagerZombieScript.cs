using UnityEngine;
using System.Collections;

public class MoveManagerZombieScript : MonoBehaviour {
	[SerializeField]
	private FollowMoveZombieScript m_followScript;

	[SerializeField]
	private RandomMoveZombieScript m_randomScript;

    [SerializeField]
    private int m_atIsStair;

    public int AtIsStair
    {
        get
        {
            return m_atIsStair;
        }
        set
        {
            m_atIsStair = value;
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
