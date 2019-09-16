﻿using System;

namespace SubterfugeCore.Timing
{
    public class GameTick
    {
        public const int MINUTES_PER_TICK = 15;
        private DateTime startTime;
        private int tickNumber;

        public GameTick(DateTime startTime, int tickNumber) {
            this.startTime = startTime;
            this.tickNumber = tickNumber;
        }
        
        public static GameTick fromDate(DateTime startDate, DateTime dateTime)
        {
            // Determine the delta
            TimeSpan dateDelta = dateTime.Subtract(startDate);
            // Get seconds elapsed
            double minutesElapsed = dateDelta.TotalMinutes;
            // Determine the number of ticks past
            int ticksElapsed = (int)Math.Ceiling(minutesElapsed / GameTick.MINUTES_PER_TICK);

            return new GameTick(dateTime, ticksElapsed);
        }

        public GameTick fromTickNumber(DateTime startDate, int tickNumber)
        {
            return new GameTick(startDate.AddMinutes(tickNumber * GameTick.MINUTES_PER_TICK), tickNumber);
        }

        public GameTick getNextTick()
        {
            return new GameTick(this.startTime.AddMinutes(MINUTES_PER_TICK), this.tickNumber + 1);
        }

        public GameTick getPreviousTick()
        {
            return new GameTick(this.startTime.AddMinutes(-MINUTES_PER_TICK), this.tickNumber - 1);
        }

        public GameTick advance(int ticks)
        {
            return new GameTick(this.startTime.AddMinutes(ticks * MINUTES_PER_TICK), this.tickNumber + ticks);
        }

        public GameTick rewind(int ticks)
        {
            if (this.tickNumber >= ticks)
            {
                return new GameTick(this.startTime.AddMinutes(ticks * -MINUTES_PER_TICK), this.tickNumber - ticks);
            }
            return null;
        }

        public DateTime getDate()
        {
            return this.startTime;
        }

        public int getTick()
        {
            return this.tickNumber;
        }

        public static bool operator > (GameTick firstArg, GameTick secondArg)
        {
            return firstArg.getTick() > secondArg.getTick();
        }

        public static bool operator <(GameTick firstArg, GameTick secondArg)
        {
            return firstArg.getTick() < secondArg.getTick();
        }

        public static int operator -(GameTick firstArg, GameTick secondArg)
        {
            return firstArg.getTick() - secondArg.getTick();
        }

        public static int operator +(GameTick firstArg, GameTick secondArg)
        {
            return firstArg.getTick() + secondArg.getTick();
        }

    }
}
