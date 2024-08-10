using RimWorld;
using Verse;

namespace Laetitia.Def
{
    [DefOf]
    public class AnimationDefOf
    {
        public static AnimationDef LaetitiaMonster_Walk;

        public static AnimationDef LaetitiaMonster_Idle;

        public static AnimationDef LaetitiaMonster_AttackR;
        public static AnimationDef LaetitiaMonster_AttackL;

        public static AnimationDef LaetitiaMonster_Spawn;

        public static AnimationDef LaetitiaMonster_Dead;

        static AnimationDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(AnimationDefOf));
        }
    }
}