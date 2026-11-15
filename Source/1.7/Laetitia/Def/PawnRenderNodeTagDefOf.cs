using RimWorld;
using Verse;

namespace Laetitia.Def
{
    [DefOf]
    public class PawnRenderNodeTagDefOf
    {
        public static PawnRenderNodeTagDef Laetitia_BodyUpper;
        public static PawnRenderNodeTagDef Laetitia_EyeLeft;
        public static PawnRenderNodeTagDef Laetitia_EyeRight;
        public static PawnRenderNodeTagDef Laetitia_Head;
        public static PawnRenderNodeTagDef Laetitia_ArmLeft;
        public static PawnRenderNodeTagDef Laetitia_ArmRight;
        public static PawnRenderNodeTagDef Laetitia_Skirt;
        public static PawnRenderNodeTagDef Laetitia_LegLeft;
        public static PawnRenderNodeTagDef Laetitia_LegRight;

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
