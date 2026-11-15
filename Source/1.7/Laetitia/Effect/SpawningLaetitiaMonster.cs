using LCAnomalyCore.Misc;
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
            LCAnomalyCore.Util.AbnormalityUtility.Burst(Position, Map, 2f);
            completeTick = base.TickSpawned + 120;

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
            Pawn monster = PawnGenerator.GeneratePawn(Def.PawnKindDefOf.LaetitiaMonster, LCAnomalyCore.Util.LCContainmentUtility.AbnormalityFaction);
            GenSpawn.Spawn(monster, base.Position, base.MapHeld);

            //销毁自己
            Destroy();
        }
    }
}
