using Laetitia.Effect;
using RimWorld;
using Verse;

namespace Laetitia.Util
{
    public static class EffectUtil
    {
        /// <summary>
        /// 蕾蒂希娅死后操作
        /// </summary>
        /// <param name="pawn">单位</param>
        /// <param name="map">地图</param>
        public static void OnLaetitiaDeath(Pawn pawn, Map map)
        {
            Map targetMap = map ?? pawn?.MapHeld;
            IntVec3 targetPosition = pawn?.PositionHeld ?? IntVec3.Invalid;
            if (pawn == null || targetMap == null || !targetPosition.InBounds(targetMap))
            {
                Log.Warning($"Unable to create Laetitia death effect for {pawn}: no valid map position.");
                return;
            }

            Find.LetterStack.ReceiveLetter("LetterLabelLaetitiaKilled".Translate()
                , "LetterLaetitiaKilled".Translate()
                , LetterDefOf.PositiveEvent
                , new LookTargets(targetPosition, targetMap));

            DyingLaetitia dyingLaetitia = GenSpawn
                .Spawn(Def.ThingDefOf.DyingLaetitia, targetPosition, targetMap) as DyingLaetitia;
            if (dyingLaetitia == null)
            {
                Log.Error($"Failed to create Laetitia death effect for {pawn} at {targetPosition}.");
                return;
            }

            dyingLaetitia.InitWith(pawn);
        }
    }
}
