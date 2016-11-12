namespace AFKHero.Core.Event
{
    public static class Events
    {
        public static readonly string EXPERIENCE_GAIN = "experience";
        public static readonly string GOLD_GAIN = "gold";

        public static readonly string HEAL = "heal";
        public static readonly string DROP = "drop";
        public static readonly string DUST = "dust";

        public static class Attack
        {
            public static readonly string DAMAGE = "attack.damage";
            public static readonly string COMPUTE = "attack.compute";
        }

        public static class GearStat
        {
            public static readonly string ATTACK = "gearstat.attack";
            public static readonly string DEFENSE = "gearstat.defense";

        }

        public static class Level
        {
            public static readonly string UPDATE = "level.update";
            public static readonly string UP = "level.up";
        }

        public static class UI
        {
            public static readonly string EXPERIENCE = "experience.ui";
            public static readonly string STAT_INCREASE = "ui.stat.increase";
            public static readonly string STAT_UPDATED = "ui.stat.updated";
            public static readonly string GOLD_UPDATED = "ui.gold";
        }

        public static class Stat
        {
            public static readonly string STAT_COMPUTE_BASE = "stat.compute.";

            public static class Health
            {
                public static readonly string FULL_HEAL = "health.fullHeal";
            }

            public static class Points
            {
                public static readonly string UPDATED = "stat.points.updated";
            }

            public static class Movespeed
            {
                public static readonly string BONUS = "movespeed.bonus";
            }

            public static class Regen
            {
                public static readonly string BONUS = "regen.bonus";
            }

            public static class Vitality
            {
                public static readonly string COMPUTE = "stat.compute.vitality";
            }
        }

        public static class Gear
        {
            public static readonly string MODIFIED = "gear.modified";
        }

        public static class Boss
        {
            public static readonly string KILLED = "boss.killed";
        }

        public static class Movement
        {
            public static readonly string ENABLED = "movement.enabled";
            public static readonly string MOVED = "movement.moved";
        }
    }
}