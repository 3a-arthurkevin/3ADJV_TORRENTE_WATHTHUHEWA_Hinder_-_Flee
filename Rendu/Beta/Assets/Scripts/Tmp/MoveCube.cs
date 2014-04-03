using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour {
	
    void FixedUpdate ()
    {
        transform.position += Vector3.right * 2 * Time.deltaTime;
	}
}
