using LCAnomalyLibrary.Comp;
using LCAnomalyLibrary.Comp.Pawns;
using LCAnomalyLibrary.Util;
using RimWorld;
using Verse;

namespace Laetitia.Comp
{
    public class CompLaetitia : LC_CompEntity
    {
        #region 变量

        public new CompProperties_Laetitia Props => (CompProperties_Laetitia)props;

        #endregion 变量

        #region 触发事件

        public override void Notify_Killed(Map prevMap, DamageInfo? dinfo = null)
        {
            Util.EffectUtil.OnLaetitiaDeath((Pawn)parent, prevMap);
        }

        public override void Notify_Escaped()
        {
            parent.Kill();
        }

        /// <summary>
        /// 绑到收容平台上后的操作
        /// </summary>
        public override void Notify_Holded()
        {
            CheckIsDiscovered();
        }

        #endregion 触发事件

        #region 研究与图鉴

        protected override float StudySuccessRateCalculate(CompPawnStatus studier, EAnomalyWorkType workType)
        {
            float baseRate = base.StudySuccessRateCalculate(studier, workType);
            float workTypeRate = 0;
            float finalRate = 0;

            switch (workType)
            {
                case EAnomalyWorkType.Instinct:
                    //本能：I级40%，II级45%，别的50%
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
                    //洞察：40%
                    switch (studier.GetPawnStatusELevel(EPawnStatus.Prudence))
                    {
                        default:
                            workTypeRate = 0.4f;
                            break;
                    }
                    break;
                case EAnomalyWorkType.Attachment:
                    //沟通：I~III级：60%，别的65%
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
                    //压迫：0%
                    switch (studier.GetPawnStatusELevel(EPawnStatus.Justice))
                    {
                        default:
                            workTypeRate = 0;
                            break;
                    }
                    break;
            }

            finalRate = baseRate + workTypeRate;

            //成功率不能超过95%
            if (finalRate > 0.95f)
                finalRate = 0.95f;

            return finalRate;
        }


        public override bool CheckStudierSkillRequire(Pawn studier)
        {
            if (studier.skills.GetSkill(SkillDefOf.Intellectual).Level < 4)
            {
                //Log.Message($"工作：{studier.Name}的技能{SkillDefOf.Intellectual.label.Translate()}等级不足4，工作固定无法成功");
                return false;
            }

            return true;
        }

        protected override void StudyEvent_Normal(Pawn studier)
        {
            var hediff = studier?.health.GetOrAddHediff(Def.HediffDefOf.LaetitiaGift);
            if (hediff != null)
                hediff.Severity = 1;

            base.StudyEvent_Normal(studier);
        }

        /// <summary>
        /// 检查是否已在图鉴中被解锁
        /// </summary>
        private void CheckIsDiscovered()
        {
            if (AnomalyUtility.ShouldNotifyCodex((Pawn)parent, EntityDiscoveryType.BecameVisible, out var entries))
            {
                Find.EntityCodex.SetDiscovered(entries, Def.PawnKindDefOf.Laetitia.race, (Pawn)parent);
            }
        }

        #endregion 研究与图鉴
    }
}