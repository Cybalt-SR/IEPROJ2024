using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Data
{
    [Serializable]
    public struct GunData
    {
        [Header("Basic")]
        public int damage;
        public int shots_per_second;
        public int clip_size;
        public float reload_time;
        [Header("Accuracy")]
        public float projectile_speed;
        public float error_angle;
        [Header("Spread")]
        public int projectiles_per_shot;
        public float spread_angle;
        [Header("Collision")]
        public int bounce_count;
        public int split_count;
        public int pierce_count;
        [Header("Visuals")]
        public string projectile_id;
    }
}