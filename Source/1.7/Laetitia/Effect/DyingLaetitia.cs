using Laetitia.Comp;
using LCAnomalyCore.Comp;
using LCAnomalyCore.Misc;
using RimWorld;
using Verse;

namespace Laetitia.Effect
{
    /// <summary>
    /// 蕾蒂希娅死亡特效对象
    /// </summary>
    public class DyingLaetitia : LC_FX_Standard
    {
        public override void InitWith(Pawn targetPawn)
        {
            var comp = targetPawn.TryGetComp<CompLaetitia>();

            //传递生物特征，播放effecter特效，记录动画播放完的时间
            bioSignature = comp.biosignature;

            EffecterDef effecterDef = ModsConfig.AnomalyActive ? EffecterDefOf.MeatExplosionExtraLarge : null;
            if (effecterDef != null)
            {
                Effecter effecter = effecterDef.SpawnMaintained(base.Position, base.Map);
                completeTick = base.TickSpawned + effecter.ticksLeft + 60;
            }
            else
            {
                completeTick = base.TickSpawned + 60;
            }

            hasInited = true;
        }

        public override void Complete()
        {
            if (!hasInited)
            {
                //Log.Warning($"特效：在未初始化时尝试生成特效对象{Def.ThingDefOf.DyingMeatLantern.label.Translate()}，对象即将被销毁以避免错误。");
                Destroy();
                return;
            }
            //生成扭曲血肉脏污
            ThingDef filthDef = ModsConfig.AnomalyActive
                ? RimWorld.ThingDefOf.Filth_TwistedFlesh
                : RimWorld.ThingDefOf.Filth_Blood;
            if (filthDef != null && FilthMaker.TryMakeFilth(base.PositionHeld, base.Map, filthDef))
            {
                //清除1格以内的植物
                foreach (IntVec3 item in CellRect.CenteredOn(base.PositionHeld, 1))
                {
                    Plant plant = item.GetPlant(base.Map);
                    if (plant != null && plant.MaxHitPoints < 100)
                    {
                        plant.Destroy();
                    }
                }
            }

            //生成蛋，销毁自己
            Thing thing = ThingMaker.MakeThing(Def.ThingDefOf.LaetitiaEgg);
            CompAbnormality abnormalityComp = thing.TryGetComp<CompAbnormality>();
            if (abnormalityComp != null)
            {
                abnormalityComp.biosignature = bioSignature;
            }
            GenSpawn.Spawn(thing, base.PositionHeld, base.MapHeld);
            Destroy();
        }
    }
}
