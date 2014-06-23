using UnityEngine;
using System.Collections;

public abstract class DirectUseItemScript : ItemBaseScript 
{
    public DirectUseItemScript() 
        :base()
    {
    }

    protected override abstract void init();

    public override void useItem(NetworkView networkView,  Vector3 clickPostion)
    {
        if (Network.isServer)
        {
            
            //utilisation objet --> affecation direct effet sur le perso (pv ++, soin mutation, vitess++ ....)
        }
    }
}
