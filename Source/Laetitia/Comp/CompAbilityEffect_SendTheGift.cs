using RimWorld;
using Verse;

namespace Laetitia.Comp
{
    public class CompAbilityEffect_SendTheGift : CompAbilityEffect
    {
        private new CompProperties_AbilitySendTheGift Props => (CompProperties_AbilitySendTheGift)props;

        private Pawn Pawn => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);

            if (target.Pawn != null)
            {
                var hediff = target.Pawn.health.GetOrAddHediff(Def.HediffDefOf.LaetitiaGiftAggresive);
                hediff.Severity = 1;
            }
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            var pawn = target.Pawn;
            if (pawn != null)
            {
                if (pawn.RaceProps.Humanlike)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            if (target.Pawn != null && target.Pawn.Faction != Pawn.Faction)
            {
                return true;
            }

            return false;
        }
    }
}