using Verse;

namespace Laetitia.Comp
{
    public class HediffCompProperties_LaetitiaGift : HediffCompProperties
    {
        public float deathDistance = 3f;

        public HediffCompProperties_LaetitiaGift()
        {
            compClass = typeof(HediffComp_LaetitiaGift);
        }
    }
}