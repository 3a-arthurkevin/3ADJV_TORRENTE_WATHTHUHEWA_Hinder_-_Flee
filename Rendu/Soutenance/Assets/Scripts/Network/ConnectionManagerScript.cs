using UnityEngine;
using System.Collections;

public class ConnectionManagerScript : MonoBehaviour
{
    /* attribut for GUI */
    private Rect m_ipAddressLabel = new Rect(10, 10, 80, 20);
    private Rect m_ipAddressField = new Rect(90, 10, 70, 20);
    private Rect m_buildServerToggle = new Rect(10, 40, 100, 20);
    private Rect m_maxPlayerLabel = new Rect(110, 40, 100, 20);
    private Rect m_maxPlayerField = new Rect(190, 40, 30, 20);
    private Rect m_chooseLevelLabel = new Rect(10, 80, 120, 20);
    private Rect m_chooseLevelField = new Rect(150, 80, 30, 20);
    private Rect m_LaunchButton = new Rect(10, 120, 70, 20);



    static public bool m_buildServer = false;
    static public int m_portNumber = 9090;
    static public string m_idAdress = "127.0.0.1";
    static public int m_maxPlayers = 2;
    static public int m_levelChoose = 1;


    void OnGUI()
    {
        GUI.Label(m_ipAddressLabel, "Ip Address : ");
        ConnectionManagerScript.m_idAdress = GUI.TextField(m_ipAddressField, ConnectionManagerScript.m_idAdress);
        ConnectionManagerScript.m_buildServer = GUI.Toggle(m_buildServerToggle, ConnectionManagerScript.m_buildServer, "Build server");

        if (m_buildServer)
        {
            GUI.Label(m_maxPlayerLabel, "Max player : ");
            string value = GUI.TextField(m_maxPlayerField, ConnectionManagerScript.m_maxPlayers.ToString());

            try
            {
                ConnectionManagerScript.m_maxPlayers = int.Parse(value);
            }
            catch (System.FormatException)
            {
                ConnectionManagerScript.m_maxPlayers = 0;
            }
        }

        GUI.Label(m_chooseLevelLabel, "Choose level");
        string val = GUI.TextField(m_chooseLevelField, ConnectionManagerScript.m_levelChoose.ToString());

        try
        {
            ConnectionManagerScript.m_levelChoose = int.Parse(val);
        }
        catch (System.FormatException)
        {
            ConnectionManagerScript.m_levelChoose = 1;
        }

        if (GUI.Button(m_LaunchButton, "Launch"))
        {
            if (ConnectionManagerScript.m_levelChoose != 1 && ConnectionManagerScript.m_levelChoose != 2)
                return;

            Application.LoadLevel("Level_" + ConnectionManagerScript.m_levelChoose.ToString());
        }
    }
}
