using RimWorld;

namespace Laetitia.Comp
{
    public class CompProperties_AbilitySendTheGift : CompProperties_AbilityEffect
    {
        public CompProperties_AbilitySendTheGift()
        {
            compClass = typeof(CompAbilityEffect_SendTheGift);
        }
    }
}