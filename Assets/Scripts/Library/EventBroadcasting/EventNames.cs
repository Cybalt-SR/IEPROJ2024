using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNames
{

    public class ABILITY_EVENTS
    {
        public const string ON_ABILITY_ACTIVATION = "ON_ABILITY_ACTIVATION";
    }

    public class COOLDOWN_EVENTS
    {
        public const string ON_COOLDOWN_RESET = "ON_COOLDOWN_RESET";
        public const string ON_COOLDOWN_BEGIN = "ON_COOLDOWN_BEGIN";
        public const string ON_COOLDOWN_TICK = "ON_COOLDOWN_TICK";
        public const string ON_COOLDOWN_ENDED = "ON_COOLDOWN_ENDED";
    }
}
