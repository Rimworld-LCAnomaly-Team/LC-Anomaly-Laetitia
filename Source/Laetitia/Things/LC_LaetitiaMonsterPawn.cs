using LCAnomalyLibrary.Comp;
using System.Collections.Generic;
using Verse;

namespace Laetitia.Things
{
    public class LC_LaetitiaMonsterPawn : LC_EntityBasePawn
    {
        public LC_LaetitiaMonsterPawn() 
        { 

        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach(var gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            yield return new Command_Action
            {
                defaultLabel = "walk",
                action = delegate
                {
                    Log.Warning($"Dev：walk");
                    this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Walk);
                }
            };

            yield return new Command_Action
            {
                defaultLabel = "Idle",
                action = delegate
                {
                    Log.Warning($"Dev：Idle");
                    this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Idle);
                }
            };

            yield return new Command_Action
            {
                defaultLabel = "AttackR",
                action = delegate
                {
                    Log.Warning($"Dev：AttackR");
                    this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_AttackR);
                }
            };

            yield return new Command_Action
            {
                defaultLabel = "AttackL",
                action = delegate
                {
                    Log.Warning($"Dev：AttackL");
                    this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_AttackL);
                }
            };

            yield return new Command_Action
            {
                defaultLabel = "Spawn",
                action = delegate
                {
                    Log.Warning($"Dev：Spawn");
                    this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Spawn);
                }
            };

            yield return new Command_Action
            {
                defaultLabel = "Dead",
                action = delegate
                {
                    Log.Warning($"Dev：Dead");
                    this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Dead);
                }
            };
        }
    }
}
