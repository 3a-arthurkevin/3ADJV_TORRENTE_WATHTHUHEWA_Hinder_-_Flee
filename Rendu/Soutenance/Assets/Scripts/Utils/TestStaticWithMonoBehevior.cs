using UnityEngine;
using System.Collections;

public class TestStaticWithMonoBehevior : MonoBehaviour
{

    public static float i = 0;

    public static float getI()
    {
        return i;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;
    }
}
