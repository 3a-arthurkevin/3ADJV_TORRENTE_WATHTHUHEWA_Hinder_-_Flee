using UnityEngine;
using System.Collections;

public class ConnexionScript : MonoBehaviour {

    [SerializeField]
    private bool m_isBuildingServer = true;

    [SerializeField]
    private int portNumber = 9090;

	// Use this for initialization
	void Start () 
    {
        Application.runInBackground = true;

        if (m_isBuildingServer)
        {
            Network.InitializeSecurity();
            Network.InitializeServer(1, portNumber, true);
        }
        else
        {
            Network.Connect("127.0.0.1", portNumber);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
