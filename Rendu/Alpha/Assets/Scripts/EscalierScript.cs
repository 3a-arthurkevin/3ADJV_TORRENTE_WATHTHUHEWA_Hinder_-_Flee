using UnityEngine;
using System.Collections;

public class EscalierScript : MonoBehaviour {

    private TextAsset m_image;
    private Texture2D m_newCursor;

    void Start ()
    {
        m_newCursor = new Texture2D(32, 32);
	    m_newCursor.LoadImage(m_image.bytes);
    }
	
	void Update ()
    {
	
	}
}
