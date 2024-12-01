using LCAnomalyCore.Comp;

namespace Laetitia.Comp
{
    public class CompStudiable_Laetitia : LC_CompStudiable
    {
        public override float GetWorkSpeedOffset()
        {
            if (CompStudyUnlocks.Completed)
            {
                return 0.08f;
            }
            else if (CompStudyUnlocks.Progress >= 2)
            {
                return 0.04f;
            }

            return 0;
        }

        public override float GetWorkSuccessRateOffset()
        {
            if (CompStudyUnlocks.Completed)
            {
                return 0.08f;
            }
            else if (CompStudyUnlocks.Progress >= 1)
            {
                return 0.04f;
            }

            return 0;
        }
    }
}