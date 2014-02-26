using UnityEngine;
using System.Collections;

public class CameraLimitDeplacement : MonoBehaviour {

    [SerializeField]
    private Transform m_planeMap;

    [SerializeField]
    private float m_limitX;

    [SerializeField]
    private float m_limitZ;


    public void setFloar(Transform floar)
    {
        m_planeMap = floar;
        m_limitX = m_planeMap.position.x + (m_planeMap.localScale.x * 4.5f);
        m_limitZ = m_planeMap.position.z /*+ (m_planeMap.localScale.z * 6f)*/;
    }

    void Start()
    {
        m_limitX = m_planeMap.position.x + (m_planeMap.localScale.x * 4.5f);
        m_limitZ = m_planeMap.position.z /*+ (m_planeMap.localScale.z * 6f)*/;
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

        if (cameraTransform.position.z >= m_limitZ + (m_planeMap.localScale.z * 3f))
            typeOfBlockZ = 1;
        else if (cameraTransform.position.z <= -(m_limitZ + (m_planeMap.localScale.z * 7f)))
            typeOfBlockZ = -1;
        else
            typeOfBlockZ = 0;

        return typeOfBlockZ;
    }
}
