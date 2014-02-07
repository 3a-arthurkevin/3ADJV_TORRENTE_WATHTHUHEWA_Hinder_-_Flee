using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    // Variable debut
    private RaycastHit hit;

    public string typeOfPlayer;

    public string myOriginator;
    


    // variable fin

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(hit.transform.tag == "survivant")
        {

        }
	}
}
