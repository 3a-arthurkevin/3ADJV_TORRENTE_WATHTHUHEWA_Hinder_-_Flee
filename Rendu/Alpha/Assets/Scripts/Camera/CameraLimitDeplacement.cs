using UnityEngine;
using System.Collections;

public class CameraLimitDeplacement : MonoBehaviour {

    [SerializeField]
    private Transform m_planeMap;
    
    [SerializeField]
    private float m_limitX;

    [SerializeField]
    private float m_limitZ;

    void Start()
    {   
        m_limitX = m_planeMap.localScale.x * 4f;
        m_limitZ = m_planeMap.localScale.z * 5f;
    }

    public int blockMoveX(Transform cameraTransform)
    {
        int typeOfBlockX;

        if (cameraTransform.position.x >= m_limitX)
            typeOfBlockX = 1;
        else if (cameraTransform.position.x <= -m_limitX)
            typeOfBlockX = -1;
        else
            typeOfBlockX = 0;

        return typeOfBlockX;
    }

    public int blockMoveY(Transform cameraTransform)
    {
        int typeOfBlockZ = 0;

        if (cameraTransform.position.z >= m_limitZ)
            typeOfBlockZ = 1;
        else if (cameraTransform.position.z <= -m_limitZ)
            typeOfBlockZ = -1;
        else
            typeOfBlockZ = 0;

        return typeOfBlockZ;
    }
}
