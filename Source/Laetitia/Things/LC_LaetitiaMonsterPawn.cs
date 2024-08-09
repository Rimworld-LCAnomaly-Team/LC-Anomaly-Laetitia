using Laetitia.Effect;
using LCAnomalyLibrary.Comp;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Laetitia.Things
{
    public class LC_LaetitiaMonsterPawn : LC_EntityBasePawn
    {
        private Vector3 cachedPos = Vector3.zero;

        private bool isMoving => pather.Moving;

        private bool isSpawning = false;

        private bool isAttacking = false;

        private bool isDying = false;
        private bool shouldDie = false;

        private ELaetitiaMonsterAttackDir attackDir;

        public Pawn CurTarget;


        public LC_LaetitiaMonsterPawn() 
        {
            
        }

        public override void Tick()
        {
            Log.Warning(isAttacking.ToString());

            if(isSpawning)
            {
                if (this.Drawer.renderer.CurAnimation == Def.AnimationDefOf.LaetitiaMonster_Spawn
                    && this.Drawer.renderer.renderTree.AnimationTick == 24)
                {
                    isSpawning = false;
                }
            }
            else if (isDying)
            {
                if (this.Drawer.renderer.CurAnimation == Def.AnimationDefOf.LaetitiaMonster_Dead
                    && this.Drawer.renderer.renderTree.AnimationTick == 44)
                {
                    shouldDie = true;
                    ((DyingLaetitiaMonster)GenSpawn.Spawn(Def.ThingDefOf.DyingLaetitiaMonster, this.PositionHeld, this.MapHeld)).InitWith(this);
                    Kill(null);
                }
            }
            else
            {
                base.Tick();

                if (isAttacking)
                {
                    if (this.Drawer.renderer.CurAnimation == Def.AnimationDefOf.LaetitiaMonster_AttackL)
                    {
                        var time = this.Drawer.renderer.renderTree.AnimationTick;
                        if (time == 26)
                        {
                            Log.Warning("触发了攻击关键帧（左侧）");
                            AttackDirect(CurTarget);
                        }
                        else if (time == 39)
                        {
                            isAttacking = false;
                        }
                    }
                    else if (this.Drawer.renderer.CurAnimation == Def.AnimationDefOf.LaetitiaMonster_AttackR)
                    {
                        var time = this.Drawer.renderer.renderTree.AnimationTick;
                        if (time == 23)
                        {
                            Log.Warning("触发了攻击关键帧（右侧）");
                            AttackDirect(CurTarget);
                        }
                        else if (time == 39)
                        {
                            isAttacking = false;
                        }
                    }
                }
                else if (isMoving)
                {
                    if (this.Drawer.renderer.CurAnimation != Def.AnimationDefOf.LaetitiaMonster_Walk)
                    {
                        this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Walk);
                    }
                }
                else
                {
                    if (this.Drawer.renderer.CurAnimation != Def.AnimationDefOf.LaetitiaMonster_Idle)
                    {
                        this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Idle);
                    }
                }
            }



            //Log.Warning((this.Drawer.renderer.renderTree.AnimationTick).ToString());
        }

        public override void Kill(DamageInfo? dinfo, Hediff exactCulprit = null)
        {
            if(!shouldDie)
            {
                isDying = true;
                this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Dead);
            }
            else
            {
                base.Kill(dinfo, exactCulprit);
            }
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            isSpawning = true;
            Def.SoundDefOf.LaetitiaMonster_Spawn.PlayOneShotOnCamera(this.MapHeld);
            this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_Spawn);

            //LordMaker.MakeNewLord(Faction.OfEntities, new LordJob_LaetitiaMonsterAssault(), this.MapHeld, new List<Pawn> { this });
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

        public void SetTargetAndDir(Pawn target)
        {
            isAttacking = true;
            CurTarget = target;

            var pos = target.Position - this.Position;
            if (pos.x >= 0)
            {
                Log.Warning("触发了右侧攻击技能");

                attackDir = ELaetitiaMonsterAttackDir.Right;
                this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_AttackR);
            }

            else
            {
                Log.Warning("触发了左侧攻击技能");

                attackDir = ELaetitiaMonsterAttackDir.Left;
                this.Drawer.renderer.SetAnimation(Def.AnimationDefOf.LaetitiaMonster_AttackL);
            }
        }

        private void AttackDirect(Pawn target)
        {
            DamageInfo dinfo1 = new DamageInfo(DamageDefOf.Cut, 10, 0.25f, -1f, this);
            DamageInfo dinfo2 = new DamageInfo(DamageDefOf.Stun, 2, 0, -1f, this);
            if (!target.Dead)
            {
                EffecterDefOf.MeatExplosionSmall.SpawnMaintained(target.PositionHeld, target.MapHeld);
                target.TakeDamage(dinfo2);
                target.TakeDamage(dinfo1);
            }
        }
    }

    public enum ELaetitiaMonsterAttackDir
    {
        Left = 0,
        Right = 1,
    }
}
