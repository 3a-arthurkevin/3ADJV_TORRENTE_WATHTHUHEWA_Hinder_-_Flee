using UnityEngine;
using System.Collections;

public abstract class AfterAimingItemUseScript  : ItemBaseScript 
{
    private float m_range;

    public AfterAimingItemUseScript()
        :base()
    {
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public float Range
    {
        get { return m_range; }
        set { m_range = value; }
    }

    protected override abstract void init();

    public override void useItem(NetworkView networkView, Vector3 clickPosition)
    {
        if (Network.isClient && networkView.owner == Network.player)
        {
            
        }
    }
}
