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
            Find.LetterStack.ReceiveLetter("LetterLabelLaetitiaKilled".Translate()
                , "LetterLaetitiaKilled".Translate()
                , LetterDefOf.PositiveEvent
                , new LookTargets(pawn.PositionHeld, pawn.MapHeld));

            ((DyingLaetitia)GenSpawn
                .Spawn(Def.ThingDefOf.DyingLaetitia, pawn.Position, map))
                .InitWith(pawn);
        }
    }
}