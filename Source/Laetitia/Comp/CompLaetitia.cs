using LCAnomalyLibrary.Comp;
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

        protected override LC_StudyResult CheckFinalStudyQuality(Pawn studier, EAnomalyWorkType workType)
        {
            //每级智力提供5%成功率，10级智力提供50%成功率
            float successRate_Intellectual = studier.skills.GetSkill(SkillDefOf.Intellectual).Level * 0.05f;
            //叠加基础成功率，此处是50%，叠加完应是100%
            float finalSuccessRate = successRate_Intellectual + Props.studySucessRateBase;

            //本能，沟通和洞察+10%成功率，压迫-50%成功率
            switch (workType)
            {
                case EAnomalyWorkType.Instinct:
                    finalSuccessRate += 0.1f;
                    break;

                case EAnomalyWorkType.Insight:
                    finalSuccessRate += 0.1f;
                    break;

                case EAnomalyWorkType.Attachment:
                    finalSuccessRate += 0.1f;
                    break;

                case EAnomalyWorkType.Repression:
                    finalSuccessRate -= 0.5f;
                    break;
            }

            //成功率不能超过90%
            if (finalSuccessRate >= 1f)
                finalSuccessRate = 0.9f;

            return Rand.Chance(finalSuccessRate) ? LC_StudyResult.Good : LC_StudyResult.Normal;
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