using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class GunData
    {
        [Header("Basic")]
        public int damage = 1;
        public int shots_per_second = 3;
        public int clip_size = 10;
        public float reload_time = 2;
        [Header("Accuracy")]
        public float projectile_speed = 20;
        public float error_angle = 15;
        [Header("Spread")]
        public int projectiles_per_shot = 1;
        public float spread_angle = 45;
        [Header("Collision")]
        public int bounce_count = 0;
        public int split_count = 0;
        public int pierce_count = 0;
    }
}