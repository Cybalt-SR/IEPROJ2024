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
        public bool use_stats_as_desc = true;
        [TextArea(1, 5)]
        [SerializeField] string _attachment_description;
        public string attachment_description
        {
            get
            {
                if (use_stats_as_desc)
                    return this.ToString();
                else
                    return _attachment_description;
            }
        }

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

        public override string ToString()
        {
            var ret = "";

            if (damage_added != 0)
                ret += "+(" + damage_added + ") damage" + System.Environment.NewLine;
            if (shots_per_second_added != 0)
                ret += "+(" + shots_per_second_added + ") shots/sec" + System.Environment.NewLine;
            if (clip_size_added != 0)
                ret += "+(" + clip_size_added + ") clip size" + System.Environment.NewLine;
            if (reload_time_percent_deduction != 0)
                ret += "(" + Mathf.RoundToInt(reload_time_percent_deduction * 100) + "%) reload speed" + System.Environment.NewLine;
            
            if (projectile_speed_added != 0)
                ret += "+(" + projectile_speed_added + ") fly speed" + System.Environment.NewLine;
            if (error_angle_percent_deduction != 0)
                ret += "(" + Mathf.RoundToInt(error_angle_percent_deduction * 100) + "%) accuracy" + System.Environment.NewLine;

            if (projectiles_per_shot_added != 0)
                ret += "+(" + projectiles_per_shot_added + ") bullets/shot" + System.Environment.NewLine;
            if (spread_angle_percent_difference != 0)
                ret += "(" + Mathf.RoundToInt(spread_angle_percent_difference * 100) + "%) spread" + System.Environment.NewLine;

            if (bounce_count_added != 0)
                ret += "+(" + bounce_count_added + ") bounces" + System.Environment.NewLine;
            if (split_count_added != 0)
                ret += "+(" + split_count_added + ") split" + System.Environment.NewLine;
            if (pierce_count_added != 0)
                ret += "+(" + pierce_count_added + ") pierce" + System.Environment.NewLine;

            return ret;
        }
    }
}