﻿using UnityEngine;
using System.Collections;

public class AOESkill : ISkill
{
    public AOESkill() : base()
    {}

    public override void StartSkill()
    {
        Debug.Log("Start AOE Skill");
    }

    public override void StopSkill()
    {
        Debug.Log("Stop AOE Skill");
    }

    public override bool CheckLaunch(Vector3 hit)
    {//Check range et aute donnée
        Debug.Log("CheckLaunch AOE");
        return true;
    }

    public override void LaunchSkill(Vector3 hit)
    {//Lancement du skill
        base.LaunchSkill(hit);
    }
}
