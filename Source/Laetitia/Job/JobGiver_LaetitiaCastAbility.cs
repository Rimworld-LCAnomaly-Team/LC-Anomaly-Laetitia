using RimWorld;
using Verse;
using Verse.AI;

namespace Laetitia.Job
{
    public class JobGiver_LaetitiaCastAbility : ThinkNode_JobGiver
    {
        protected Verse.AI.Job MeleeAttack(Pawn user)
        {
            Ability ability = user.abilities.GetAbility(Def.AbilityDefOf.LaetitiaMonster_Attack, false);
            if (ability == null || !ability.CanCast || ability.Casting)
            {
                return null;
            }

            LocalTargetInfo targetInfo = user.mindState.meleeThreat;
            if(targetInfo == null || !targetInfo.HasThing || !ability.CanApplyOn(targetInfo))
            {
                return null;
            }

            //Log.Message("cast");

            Verse.AI.Job job = JobMaker.MakeJob(JobDefOf.CastAbilityOnThing);
            job.ability = ability;
            job.verbToUse = ability.verb;
            job.targetA = targetInfo;
            job.count = 1;
            return job;
        }

        protected override Verse.AI.Job TryGiveJob(Pawn user)
        {
            if(user.kindDef == Def.PawnKindDefOf.LaetitiaMonster)
                return this.MeleeAttack(user);

            return null;
        }
    }
}
