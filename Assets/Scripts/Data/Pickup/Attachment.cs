using UnityEngine;

namespace Assets.Scripts.Data.ScriptableObjects
{
    public class Attachment : ScriptableObject
    {
        public enum Part
        {
            Mag,
            Core,
            Scope,
            Stock,
            Foregrip,
            SecondaryBarrel,
            Muzzle
        }

        [Header("Basic")]
        public int damage_added = 0;
        public int shots_per_second_added = 0;
        public int clip_size_added = 0;
        [Range(0, 1)]public float reload_time_percent_deduction = 0;
        [Header("Accuracy")]
        public float projectile_speed_added = 0;
        public float error_angle_percent_deduction = 0;
        [Header("Spread")]
        public int projectiles_per_shot_added = 0;
        [Range(0, 1)] public float spread_angle_percent_difference = 0;
        [Header("Collision")]
        public int bounce_count_added = 0;
        public int split_count_added = 0;
        public int pierce_count_added = 0;
    }
}