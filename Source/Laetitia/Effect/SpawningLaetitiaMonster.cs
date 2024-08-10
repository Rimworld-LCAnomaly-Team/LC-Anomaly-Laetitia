using LCAnomalyLibrary.Misc;
using RimWorld;
using Verse;

namespace Laetitia.Effect
{
    public class SpawningLaetitiaMonster : LC_FX_Standard
    {
        public void Init()
        {
            DoDefaultFX();
        }

        private void DoDefaultFX()
        {
            //播放血肉特效，记录动画播放完的时间
            Effecter effecter = EffecterDefOf.MeatExplosionExtraLarge.SpawnMaintained(base.Position, base.Map);
            completeTick = base.TickSpawned + effecter.ticksLeft + 120;

            hasInited = true;
        }

        public override void Complete()
        {
            if (!hasInited)
            {
                Destroy();
                return;
            }

            //生成蕾蒂希娅的朋友
            Pawn monster = PawnGenerator.GeneratePawn(Def.PawnKindDefOf.LaetitiaMonster, Faction.OfEntities);
            GenSpawn.Spawn(monster, base.Position, base.MapHeld);

            //销毁自己
            Destroy();
        }
    }
}