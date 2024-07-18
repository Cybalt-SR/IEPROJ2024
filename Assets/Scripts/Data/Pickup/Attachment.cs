using UnityEngine;

namespace Assets.Scripts.Data.Pickup
{
    [CreateAssetMenu(menuName = "Game/Item/Attachment")]
    public class Attachment : Pickup
    {
        public enum Part
        {
            Mag,
            Core1,
            Core2,
            Scope,
            Stock,
            Foregrip,
            SecondaryBarrel,
            Muzzle
        }

        public Part part;

        [Header("Sprite")]
        public Sprite attachment_icon = null;
        public string attachment_name;
        [TextArea(1, 5)]
        public string attachment_description;

        [Header("Basic")]
        public int damage_added = 0;
        public int shots_per_second_added = 0;
        public int clip_size_added = 0;
        [Range(-1, 1)]public float reload_time_percent_deduction = 0;
        [Header("Accuracy")]
        public float projectile_speed_added = 0;
        [Range(-1, 1)] public float error_angle_percent_deduction = 0;
        [Header("Spread")]
        public int projectiles_per_shot_added = 0;
        [Range(-1, 1)] public float spread_angle_percent_difference = 0;
        [Header("Collision")]
        public int bounce_count_added = 0;
        public int split_count_added = 0;
        public int pierce_count_added = 0;

        public void ApplyThis(ref GunData data)
        {
            data.damage += damage_added;
            data.shots_per_second += shots_per_second_added;
            data.clip_size += clip_size_added;
            data.clip_size = Mathf.Max(1, data.clip_size);

            data.reload_time *= 1.0f - reload_time_percent_deduction;

            data.projectile_speed += projectile_speed_added;
            data.error_angle *= 1.0f - error_angle_percent_deduction;

            data.projectiles_per_shot += projectiles_per_shot_added;
            data.spread_angle *= 1.0f + spread_angle_percent_difference;

            data.bounce_count += bounce_count_added;
            data.split_count += split_count_added;
            data.pierce_count += pierce_count_added;
        }
    }
}