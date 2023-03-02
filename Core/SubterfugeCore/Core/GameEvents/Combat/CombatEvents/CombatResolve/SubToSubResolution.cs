﻿using System.Collections.Generic;
using Subterfuge.Remake.Core.Entities;
using Subterfuge.Remake.Core.Entities.Positions;
using Subterfuge.Remake.Core.Entities.Specialists;
using Subterfuge.Remake.Core.GameEvents.Combat.CombatEvents.CombatResolve;
using Subterfuge.Remake.Core.Timing;

namespace Subterfuge.Remake.Core.GameEvents.Combat.CombatEvents
{
    public class SubToSubResolution: CombatResolution
    {
        private Sub _sub1;
        private Sub _sub2;

        private readonly CombatDrillersEffect _drillerCombatDrillers;
        private readonly CombatShieldEffect _shield;
        private readonly CombatSpecialistCaptureEffect _combatSpecialistCapture;
        private readonly CombatRemoveEmptyEntitiesEffect _combatRemoveEmptyEntities;
        
        public SubToSubResolution(
            GameTick occursAt,
            IEntity combatant1,
            IEntity combatant2
        ) : base(occursAt, CombatType.SUB_TO_SUB, combatant1)
        {
            _sub1 = combatant1 as Sub;
            _sub2 = combatant2 as Sub;
            _drillerCombatDrillers = new CombatDrillersEffect(occursAt, _sub1, _sub2);
            _shield = new CombatShieldEffect(occursAt, _sub1, _sub2);
            _combatSpecialistCapture = new CombatSpecialistCaptureEffect(occursAt, _sub1, _sub2);
            _combatRemoveEmptyEntities = new CombatRemoveEmptyEntitiesEffect(occursAt, _sub1, _sub2, this);
        }

        public override bool ForwardAction(TimeMachine timeMachine, GameState state)
        {
            _shield.ForwardAction(timeMachine, state);
            _drillerCombatDrillers.ForwardAction(timeMachine, state);
            _combatSpecialistCapture.ForwardAction(timeMachine, state);
            _combatRemoveEmptyEntities.ForwardAction(timeMachine, state);
            return true;
        }

        public override bool BackwardAction(TimeMachine timeMachine, GameState state)
        {
            _combatRemoveEmptyEntities.BackwardAction(timeMachine, state);
            _combatSpecialistCapture.BackwardAction(timeMachine, state);
            _drillerCombatDrillers.BackwardAction(timeMachine, state);
            _shield.BackwardAction(timeMachine, state);
            return true;
        }

        public override bool WasEventSuccessful()
        {
            return true;
        }
    }
}