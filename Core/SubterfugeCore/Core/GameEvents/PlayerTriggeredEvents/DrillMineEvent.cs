﻿using System;
using System.Collections.Generic;
using System.Text;
using GameEventModels;
using Google.Protobuf;
using SubterfugeCore.Core.Components;
using SubterfugeCore.Core.Config;
using SubterfugeCore.Core.Entities;
using SubterfugeCore.Core.Entities.Positions;
using SubterfugeCore.Core.GameEvents.NaturalGameEvents.outpost;
using SubterfugeCore.Core.Interfaces;
using SubterfugeCore.Core.Timing;
using SubterfugeRemakeService;

namespace SubterfugeCore.Core.GameEvents.PlayerTriggeredEvents
{
	public class DrillMineEvent : PlayerTriggeredEvent
	{
		private Outpost _original;
		private Mine _drilledMine;

		public DrillMineEvent(GameEventModel miningData) : base(miningData)
		{
		}

		public DrillMineEventData GetEventData()
		{
			return DrillMineEventData.Parser.ParseFrom(model.EventData);
		}

		public override bool ForwardAction(TimeMachine timeMachine, GameState state)
		{
			Entity drillLocation = state.GetEntity(GetEventData().SourceId);
			if (drillLocation != null && drillLocation is Outpost && !(drillLocation is Mine) && !((Outpost)drillLocation).GetComponent<DrillerCarrier>().IsDestroyed())
			{
				_original = (Outpost)drillLocation;
				var drillerCarrier = drillLocation.GetComponent<DrillerCarrier>();
				if (state.GetOutposts().Contains(_original) && !drillerCarrier.GetOwner().IsEliminated() && drillerCarrier.GetDrillerCount() >= drillerCarrier.GetOwner().GetRequiredDrillersToMine())
				{
					_drilledMine = new Mine(_original);
					if (state.ReplaceOutpost(_original, _drilledMine))
					{
						drillerCarrier.RemoveDrillers(drillerCarrier.GetOwner().GetRequiredDrillersToMine());
						drillerCarrier.GetOwner().AlterMinesDrilled(1);
						timeMachine.AddEvent(new NeptuniumProductionEvent(_drilledMine, GetOccursAt().Advance(Mine.TICKS_PER_PRODUCTION_PER_MINE / state.GetPlayerOutposts(drillerCarrier.GetOwner()).Count)));
						EventSuccess = true;
					}
				}
			}
			else
			{
				EventSuccess = false;
			}
			return EventSuccess;
		}

		public override bool BackwardAction(TimeMachine timeMachine, GameState state)
		{
			if (EventSuccess)
			{
				var drillerCarrier = _drilledMine.GetComponent<DrillerCarrier>();
				state.ReplaceOutpost(_drilledMine, _original);
				drillerCarrier.GetOwner().AlterMinesDrilled(-1);
				drillerCarrier.AddDrillers(drillerCarrier.GetOwner().GetRequiredDrillersToMine());
			}
			return EventSuccess;
		}

		public override GameEventModel ToGameEventModel()
		{
			GameEventModel baseModel = GetBaseGameEventModel();
			baseModel.EventData = GetEventData().ToByteString();
			return baseModel;
		}

		public override bool WasEventSuccessful()
		{
			return EventSuccess;
		}
	}
}
