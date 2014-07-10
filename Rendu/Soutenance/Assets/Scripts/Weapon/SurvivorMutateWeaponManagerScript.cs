using UnityEngine;
using System.Collections;

public class SurvivorMutateWeaponManagerScript : BaseSurvivorWeaponManagerScript
{
    protected override void initSkill()
    {
        CoupDePoingZombie cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[0] = cz;

        cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[1] = cz;

        cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[2] = cz;

        /*cz = new CoupDePoingZombie();
        cz.WeaponManager = this;
        m_skills[0] = cz;*/
    }

    [RPC]
    public void setConfig(NetworkViewID survivorId)
    {
        if (Network.isServer)
            m_networkView.RPC("setConfig", RPCMode.OthersBuffered, survivorId);

        NetworkView survivorNetworkView = NetworkView.Find(survivorId);

        Transform survivorTrans = survivorNetworkView.transform;

        InputManagerMoveSurvivorScript inManager = survivorTrans.GetComponent<InputManagerMoveSurvivorScript>();

        transform.parent = survivorTrans;
        transform.position = Vector3.zero;

        inManager.SetPlayer(inManager.getNetworkPlayer());
        inManager.setCameraTransform(inManager.getCharacterCamera());

    }
}
