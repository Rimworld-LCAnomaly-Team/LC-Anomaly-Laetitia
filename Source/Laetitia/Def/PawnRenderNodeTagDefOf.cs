using RimWorld;
using Verse;

namespace Laetitia.Def
{
    [DefOf]
    public class PawnRenderNodeTagDefOf
    {
        public static PawnRenderNodeTagDef Bone2;

        public static PawnRenderNodeTagDef Bone3;

        public static PawnRenderNodeTagDef r3_1;
        public static PawnRenderNodeTagDef r3_2;

        public static PawnRenderNodeTagDef r4_1;
        public static PawnRenderNodeTagDef r4_2;
        public static PawnRenderNodeTagDef r4_3;

        public static PawnRenderNodeTagDef r5_1;
        public static PawnRenderNodeTagDef r5_2;

        public static PawnRenderNodeTagDef r1_1;
        public static PawnRenderNodeTagDef r1_2;

        public static PawnRenderNodeTagDef r2_1;
        public static PawnRenderNodeTagDef r2_2;

        static PawnRenderNodeTagDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnRenderNodeTagDefOf));
        }
    }
}