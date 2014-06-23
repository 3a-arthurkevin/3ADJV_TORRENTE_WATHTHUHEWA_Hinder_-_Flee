using UnityEngine;
using System.Collections;

public class PiegeLoupScript : AfterAimingItemUseScript 
{
    protected override void init()
    {
        m_name = "Piège à loup";
        m_description = "Piège qui ralentit toute personne tombant dedans";

    }
}
