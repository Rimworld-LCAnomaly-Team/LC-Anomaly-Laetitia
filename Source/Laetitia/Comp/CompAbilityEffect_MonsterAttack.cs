using Laetitia.Things;
using RimWorld;
using Verse;

namespace Laetitia.Comp
{
    public class CompAbilityEffect_MonsterAttack : CompAbilityEffect
    {
        public new CompProperties_AbilityMonsterAttack Props => (CompProperties_AbilityMonsterAttack)props;

        public DamageInfo AttackDInfo;
        public DamageInfo StunDInfo;

        public override void Initialize(AbilityCompProperties props)
        {
            base.Initialize(props);

            AttackDInfo = new DamageInfo(DamageDefOf.Cut, Props.damageRange.RandomInRange, Props.armorPenetrationRange.RandomInRange, -1f, parent.pawn);
            StunDInfo = new DamageInfo(DamageDefOf.Stun, Props.stunTimeRange.RandomInRange, 0, -1f, parent.pawn);
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                ((LC_LaetitiaMonsterPawn)parent.pawn).SetTargetAndDir(pawn);
                //MeatLanternUtility.DoBiteOnPawn(pawn, parent.pawn, Props.damageRange, Props.armorPenetration, Props.suckPercent, Props.stunPercent);
            }
        }

        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            return Valid(target);
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            var pawn = target.Pawn;

            if (pawn == null)
            {
                return false;
            }

            return true;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return false;
            }

            return true;
        }
    }
}
