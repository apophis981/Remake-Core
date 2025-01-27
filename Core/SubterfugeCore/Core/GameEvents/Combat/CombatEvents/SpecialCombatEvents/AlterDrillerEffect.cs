﻿using Subterfuge.Remake.Core.Entities;
using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.GameEvents.Base;
using Subterfuge.Remake.Core.Timing;

namespace Subterfuge.Remake.Core.GameEvents.Combat.CombatEvents
{
    public class AlterDrillerEffect : PositionalGameEvent
    {
        private IEntity _friendly;
        private IEntity _enemy;

        private int _friendlyDrillerDelta;
        private int _enemyDrillerDelta;
        
        public AlterDrillerEffect(
            CombatEvent combatEvent,
            IEntity friendly,
            int friendlyDrillerDelta,
            int enemyDrillerDelta
        ) : base(combatEvent.OccursAt, Priority.SPECIALIST_DRILLER_EFFECT, friendly)
        {
            _friendly = friendly;
            _enemy = combatEvent._combatant1 == _friendly ? combatEvent._combatant2 : combatEvent._combatant1;
            _friendlyDrillerDelta = friendlyDrillerDelta;
            _enemyDrillerDelta = enemyDrillerDelta;
        }

        public override bool ForwardAction(TimeMachine timeMachine)
        {
            _friendlyDrillerDelta = _friendly.GetComponent<DrillerCarrier>().AlterDrillers(_friendlyDrillerDelta);
            _enemyDrillerDelta = _enemy.GetComponent<DrillerCarrier>().AlterDrillers(_enemyDrillerDelta);
            return true;
        }

        public override bool BackwardAction(TimeMachine timeMachine)
        {
            _friendlyDrillerDelta = _friendly.GetComponent<DrillerCarrier>().AlterDrillers(_friendlyDrillerDelta * -1);
            _enemyDrillerDelta = _enemy.GetComponent<DrillerCarrier>().AlterDrillers(_enemyDrillerDelta * -1);
            return true;
        }

        public override bool WasEventSuccessful()
        {
            return true;
        }
    }
}