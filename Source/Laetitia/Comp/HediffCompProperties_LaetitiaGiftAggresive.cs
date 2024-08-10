using Verse;

namespace Laetitia.Comp
{
    public class HediffCompProperties_LaetitiaGiftAggresive : HediffCompProperties
    {
        public IntRange spawnAmount = new IntRange(1, 2);

        public HediffCompProperties_LaetitiaGiftAggresive()
        {
            compClass = typeof(HediffComp_LaetitiaGiftAggresive);
        }
    }
}