using UnityEngine;
using System.Collections;

public class CameraLimitDeplacementScript : MonoBehaviour
{

    [SerializeField]
    private Transform m_planeLimit;

    [SerializeField]
    private float m_planeSizeLeftAndRight = 4.5f;

    [SerializeField]
    private float m_planeSizeUp = 3f;

    [SerializeField]
    private float m_planeSizeDown = 7f;

    void Start()
    {
        m_planeLimit = GameObject.Find("Floor0").transform.FindChild("CamBorder").transform;
        setPlaneLimit(m_planeLimit);
    }

    public void setPlaneLimit(Transform planeLimit)
    {
        m_planeLimit = planeLimit;
    }

    public int blockMoveX(Transform cameraTransform)
    {
        int typeOfBlockX;

        //Block coté droite
        if (cameraTransform.position.x >= (m_planeLimit.position.x + (m_planeLimit.localScale.x * m_planeSizeLeftAndRight)))
        {
            typeOfBlockX = 1;
        }
        //Block coté gauche
        else if (cameraTransform.position.x <= (m_planeLimit.position.x - (m_planeLimit.localScale.x * m_planeSizeLeftAndRight)))
        {
            typeOfBlockX = -1;
        }
        //Ok
        else
        {
            typeOfBlockX = 0;
        }

        return typeOfBlockX;
    }

    public int blockMoveY(Transform cameraTransform)
    {
        int typeOfBlockZ = 0;

        //Block en haut
        if (cameraTransform.position.z >= (m_planeLimit.position.z + (m_planeLimit.localScale.z * m_planeSizeUp)))
        {
            typeOfBlockZ = 1;
        }
        //Block en bas
        else if (cameraTransform.position.z <= (m_planeLimit.position.z - (m_planeLimit.localScale.z * m_planeSizeDown)))
        {
            typeOfBlockZ = -1;
        }
        //Ok
        else
        {
            typeOfBlockZ = 0;
        }

        return typeOfBlockZ;
    }
}
