using Laetitia.Comp;
using LCAnomalyLibrary.Comp;

namespace Laetitia.Things
{
    public class LC_LaetitiaPawn : LC_EntityBasePawn
    {
        public LC_LaetitiaPawn()
        {
        }

        public override void Tick()
        {
            //收容状态下丢下就出逃
            if (CarriedBy == null)
            {
                GetComp<CompLaetitia>()?.Notify_Escaped();
            }

            base.Tick();
        }
    }
}