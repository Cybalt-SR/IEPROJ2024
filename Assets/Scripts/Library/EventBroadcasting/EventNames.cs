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

    public class SECONDARY_EVENTS
    {
        public const string ON_SECONDARY_EQUIP = "ON_SECONDARY_EQUIP";
        public const string ON_SECONDARY_SHOT = "ON_SECONDARY_SHOT";
        public const string ON_SECONDARY_ABILITY = "ON_SECONDARY_ABILITY";
    }


    public class PLAYER_EVENTS
    {
        public const string ON_OVERLOAD_CHANGED = "ON_OVERLOAD_CHANGED";
       
    }

    public class ENEMY_EVENTS
    {
        public const string ON_ENEMY_KILLED = "ON_ENEMY_KILLED";
    }

    public class UI_EVENTS
    {

        //remove these
        public const string ON_SECONDARY_CLICKED = "ON_SECONDARY_CLICKED";
        public const string ON_SECONDARY_EQUIP = "ON_SECONDARY_EQUIP";

        public const string ON_INVISIBILITY_INVOKED = "ON_INVISIBILITY_INVOKED";


    }
}
