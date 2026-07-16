using Laetitia.Things;
using LCAnomalyCore.Comp;
using LCAnomalyCore.Comp.Pawns;
using LCAnomalyCore.Util;
using RimWorld;
using Verse;

namespace Laetitia.Comp
{
    public class CompLaetitia : CompAbnormality
    {
        public new CompProperties_Laetitia Props => (CompProperties_Laetitia)props;

        public override void Notify_Killed(Map prevMap, DamageInfo? dinfo = null)
        {
            Util.EffectUtil.OnLaetitiaDeath((Pawn)parent, prevMap);
        }

        public override void Notify_Escaped()
        {
            parent.Kill();
        }

        /// <summary>
        /// 绑定到收容平台后恢复机械待机动画。
        /// </summary>
        public override void Notify_Holded()
        {
            (parent as LC_LaetitiaPawn)?.SetIdleAnimation();
        }

        public override void Notify_Studying(Pawn studier)
        {
            base.Notify_Studying(studier);
            (parent as LC_LaetitiaPawn)?.SetWorkAnimation();
        }

        public override void Notify_Studied(Pawn studier, bool interrupted = false)
        {
            base.Notify_Studied(studier, interrupted);

            LC_LaetitiaPawn laetitia = parent as LC_LaetitiaPawn;
            if (laetitia != null && !laetitia.GiftAnimationPlaying)
            {
                laetitia.SetIdleAnimation();
            }
        }

        protected override float StudySuccessRateCalculate(CompPawnStatus studier, EAnomalyWorkType workType)
        {
            float baseRate = base.StudySuccessRateCalculate(studier, workType);
            float workTypeRate;

            switch (workType)
            {
                case EAnomalyWorkType.Instinct:
                    switch (studier.GetPawnStatusELevel(EPawnStatus.Fortitude))
                    {
                        case EPawnLevel.I:
                            workTypeRate = 0.4f;
                            break;
                        case EPawnLevel.II:
                            workTypeRate = 0.45f;
                            break;
                        default:
                            workTypeRate = 0.5f;
                            break;
                    }
                    break;

                case EAnomalyWorkType.Insight:
                    workTypeRate = 0.4f;
                    break;

                case EAnomalyWorkType.Attachment:
                    switch (studier.GetPawnStatusELevel(EPawnStatus.Temperance))
                    {
                        case EPawnLevel.I:
                        case EPawnLevel.II:
                        case EPawnLevel.III:
                            workTypeRate = 0.6f;
                            break;
                        default:
                            workTypeRate = 0.65f;
                            break;
                    }
                    break;

                case EAnomalyWorkType.Repression:
                default:
                    workTypeRate = 0f;
                    break;
            }

            return System.Math.Min(baseRate + workTypeRate, 0.95f);
        }

        protected override void StudyEvent_Normal(Pawn studier)
        {
            Hediff hediff = studier?.health.GetOrAddHediff(Def.HediffDefOf.LaetitiaGift);
            if (hediff != null)
            {
                hediff.Severity = 1f;
            }

            (parent as LC_LaetitiaPawn)?.PlayGiftAnimation();
            base.StudyEvent_Normal(studier);
        }
    }
}
