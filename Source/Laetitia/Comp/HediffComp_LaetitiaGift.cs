using Laetitia.Effect;
using LCAnomalyCore.Jobs;
using Verse;

namespace Laetitia.Comp
{
    public class HediffComp_LaetitiaGift : HediffComp
    {
        public HediffCompProperties_LaetitiaGift Props => (HediffCompProperties_LaetitiaGift)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            //Log.Warning(Pawn.BodySize.ToString());

            //Util.GraphicUtil.LaetitiaGift_GetCachedTopGraphic()[1].Draw(Pawn.PositionHeld.ToVector3(), Rot4.South, Pawn);

            if(Pawn.CurJob.def == LCAnomalyCore.Defs.JobDefOf.LC_StudyInteract)
            {
                if(Pawn.CurJob.GetCachedDriver(Pawn) is JobDriver_LC_StudyInteract driver)
                {
                    //Log.Warning("携带蕾蒂希娅礼物的Pawn正在进行LC研究");

                    var target = driver.ThingToStudy as Pawn;
                    if (target != null && target.kindDef != Laetitia.Def.PawnKindDefOf.Laetitia)
                    {
                        var dis = (Pawn.Position - target.PositionHeld).ToVector3().magnitude;

                        if (dis < Props.deathDistance)
                            TurnIntoMonster();

                        //Log.Warning($"携带蕾蒂希娅礼物的Pawn距离非蕾蒂希娅的目标实体距离变化为：{dis}");
                    }
                }
            }
        }

        private void TurnIntoMonster()
        {
            var thing = (SpawningLaetitiaMonster)GenSpawn.Spawn(Laetitia.Def.ThingDefOf.SpawningLaetitiaMonster, Pawn.PositionHeld, Pawn.MapHeld);
            thing?.Init();

            Pawn.Kill(null);
        }
    }
}
