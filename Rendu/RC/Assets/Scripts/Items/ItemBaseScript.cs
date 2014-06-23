using UnityEngine;
using System.Collections;

public abstract class ItemBaseScript : MonoBehaviour 
{
    protected string m_name;
    protected string m_description;
    protected NetworkView m_networkView;
    protected Camera m_playerCamera;
    protected Transform m_playerTransform;
    //private MeshRenderer m_itemMeshRenderer;
    //private MeshFilter m_ItemMeshFilter;


    public ItemBaseScript()
    {
    }

    public string Name
    {
        get{ return m_name; }
        set{ m_name = value; }
    }

    public string Description
    {
        get { return m_description; }
        set { m_description = value; }
    }

    protected abstract void init();

    public abstract void useItem(NetworkView networkView, Vector3 postion);
}