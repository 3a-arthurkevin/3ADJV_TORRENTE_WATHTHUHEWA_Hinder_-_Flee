using UnityEngine;
using System.Collections;

public static class SkillFactory
{
    public static ISkill getSkill(int skillId)
    {
        ISkill skill = null;
        IEffect effect = null;
        
        switch (skillId)
        {
            case 0:
                skill = new SingleTargetSkill();
                skill.Name = "Petite BaBalle";
                skill.CoolDownDuration = 12f;
                skill.Range = 10f;

                effect = EffectsFactory.getEffect(0);
                effect.SetParam("Damage", "10");
                skill.addSurvivorEffect(effect);

                effect = EffectsFactory.getEffect(0);
                effect.SetParam("Damage", "20");
                skill.addZombieEffect(effect);
                break;

            case 1:
                AOESkill aoeSkill = new AOESkill();
                aoeSkill.Name = "AOE";
                aoeSkill.CoolDownDuration = 12f;
                aoeSkill.AoeRange = 2f;
                
                skill = aoeSkill;
                break;

            default:
                Debug.Log("Skill not found");
                break;
        }

        return skill;
    }
}
