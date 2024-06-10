using Assets.Scripts.Controller;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Gameplay.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class PickupManager : Manager_Base<PickupManager>
    {
        private Dictionary<string, List<PickupController>> nearby_per_player = new();
        private Dictionary<string, Pickup> nearest_per_player = new();
        public Pickup Nearest(string player)
        {
            if (nearest_per_player.ContainsKey(player) == false)
                return null;

            return nearest_per_player[player];
        }

        public void SetNearby(PlayerController player, PickupController pickup, bool nearby)
        {
            if (nearby_per_player.ContainsKey(player.PlayerId) == false)
                nearby_per_player.Add(player.PlayerId, new());

            var nearby_list = nearby_per_player[player.PlayerId];

            if (nearby_list.Contains(pickup))
            {
                if (nearby)
                    return;

                nearby_list.Remove(pickup);
            }
            else if(nearby)
                nearby_list.Add(pickup);

            float nearest_distance = float.PositiveInfinity;
            Pickup nearest = null;

            foreach (var nearby_object in nearby_list)
            {
                var cur_dist = Vector3.SqrMagnitude(nearby_object.transform.position - player.transform.position);
                if (cur_dist < nearest_distance)
                {
                    nearest_distance = cur_dist;
                    nearest = nearby_object.Pickup;
                }
            }

            if (nearest_per_player.ContainsKey(player.PlayerId) == false)
                nearest_per_player.Add(player.PlayerId, nearest);
            else
                nearest_per_player[player.PlayerId] = nearest;
        }
    }
}