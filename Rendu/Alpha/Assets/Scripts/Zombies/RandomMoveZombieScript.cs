using UnityEngine;
using System.Collections;

public class RandomMoveZombieScript : MonoBehaviour {

    [SerializeField]
    private Transform m_direction;

    public Transform Direction
    {
        get
        {
            return m_direction;
        }
        set
        {
            m_direction = value;
        }
    }

	void Start ()
	{
	}

	void FixedUpdate()
	{
        if (m_direction)
        {

        }
	}
}
