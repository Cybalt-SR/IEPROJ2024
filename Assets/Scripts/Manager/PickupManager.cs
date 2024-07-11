using Assets.Scripts.Controller;
using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Gameplay.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Manager
{
    public class PickupManager : Manager_Base<PickupManager>
    {
        [SerializeField] private SerializedDictionary<string, List<PickupController>> nearby_per_player = new();
        [SerializeField] private SerializedDictionary<string, Pickup> nearest_per_player = new();
        public static Pickup Nearest(string player)
        {
            if (Instance.nearest_per_player.ContainsKey(player) == false)
                return null;

            return Instance.nearest_per_player[player];
        }

        public static void Pickup(string player)
        {
            if (Instance.nearby_per_player.ContainsKey(player) == false)
                return;

            var pickup = Instance.nearest_per_player[player];

            if (pickup == null)
                return;

            switch (pickup.pickup_type)
            {
                case Data.Pickup.Pickup.PickupType.attachment:
                    EquipmentManager.PickupAttachment(player, pickup.data_attachment);
                    break;
                case Data.Pickup.Pickup.PickupType.secondary:
                    break;
                default:
                    break;
            }

            Instance.nearby_per_player[player].Find(nearby => nearby.Pickup == pickup).PickedUp();
            Instance.nearby_per_player[player].RemoveAll(x => x == null);
            Instance.nearest_per_player[player] = null;
        }

        public static void SetNearby(PlayerController player, PickupController pickup, bool nearby)
        {
            if (Instance.nearby_per_player.ContainsKey(player.PlayerId) == false)
                Instance.nearby_per_player.Add(player.PlayerId, new());

            var nearby_list = Instance.nearby_per_player[player.PlayerId];

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

            if (Instance.nearest_per_player.ContainsKey(player.PlayerId) == false)
                Instance.nearest_per_player.Add(player.PlayerId, nearest);
            else
                Instance.nearest_per_player[player.PlayerId] = nearest;
        }
    }
}