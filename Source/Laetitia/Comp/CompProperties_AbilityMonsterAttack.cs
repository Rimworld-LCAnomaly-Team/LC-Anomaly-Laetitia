using RimWorld;

namespace Laetitia.Comp
{
    public class CompProperties_AbilityMonsterAttack : CompProperties_AbilityEffect
    {
        public string attackDir = "right";

        public CompProperties_AbilityMonsterAttack()
        {
            compClass = typeof(CompAbilityEffect_MonsterAttack);
        }
    }
}
