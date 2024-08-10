using RimWorld;
using Verse;

namespace Laetitia.Comp
{
    public class CompProperties_AbilityMonsterAttack : CompProperties_AbilityEffect
    {
        public FloatRange damageRange = new FloatRange(10.0f, 15.0f);
        public FloatRange armorPenetrationRange = new FloatRange(0.10f, 0.15f);

        public FloatRange stunTimeRange = new FloatRange(1.0f, 1.5f);

        public CompProperties_AbilityMonsterAttack()
        {
            compClass = typeof(CompAbilityEffect_MonsterAttack);
        }
    }
}
