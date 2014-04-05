using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour {
    private CharacterController m_controler;
    public float m_speed = 2f;

    void Start()
    {
        m_controler = GetComponent<CharacterController>();
    }

    void Update ()
    {
        m_controler.Move(Vector3.right * m_speed * Time.deltaTime);
	}
}
