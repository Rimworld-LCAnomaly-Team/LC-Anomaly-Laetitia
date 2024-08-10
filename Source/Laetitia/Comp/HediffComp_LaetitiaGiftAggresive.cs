using Laetitia.Effect;
using Verse;

namespace Laetitia.Comp
{
    public class HediffComp_LaetitiaGiftAggresive : HediffComp
    {
        public HediffCompProperties_LaetitiaGiftAggresive Props => (HediffCompProperties_LaetitiaGiftAggresive)props;

        private bool hasSpawned = false;

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (Pawn.Downed)
                SpawnMonster();
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

            SpawnMonster();
        }

        private void SpawnMonster()
        {
            if (hasSpawned)
                return;
            hasSpawned = true;

            if (Pawn != null)
            {
                int amount = Props.spawnAmount.RandomInRange;
                for (int i = 0; i < amount; i++)
                {
                    var thing = (SpawningLaetitiaMonster)GenSpawn.Spawn(Laetitia.Def.ThingDefOf.SpawningLaetitiaMonster, Pawn.PositionHeld, Pawn.MapHeld);
                    thing?.Init();
                }

                if (!Pawn.Dead)
                    Pawn.Kill(null);
            }
        }
    }
}