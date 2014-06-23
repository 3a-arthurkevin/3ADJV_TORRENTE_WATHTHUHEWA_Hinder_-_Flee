using UnityEngine;
using System.Collections;

public abstract class AfterAimingItemUseScript  : ItemBaseScript 
{
    private float m_range;
    private float m_scaleX;
    private float m_scaleZ;

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
            if (Input.GetButtonDown("LaunchSkill"))
            {
                Ray ray = m_playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Ground")))
                {
                    if (Vector3.Distance(m_playerTransform.position, hit.point) < m_range)
                    {
                        //créer le gameObject et le placer sur la map
                    }
                    else
                    {
                        Debug.LogError("Click out of range to put item, try putting closer");
                        //Lancer un son indiquant que le clique is not ok
                    }
                }
                else if (Input.anyKeyDown)
                    m_networkView.RPC("StopItemUse", RPCMode.All);
                //Exit mode de visé pour poser objet
            }   
        }
    }
}
