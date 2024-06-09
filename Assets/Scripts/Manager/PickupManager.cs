using Assets.Scripts.Data.Pickup;
using Assets.Scripts.Gameplay.Manager;
using System.Collections.Generic;

namespace Assets.Scripts.Manager
{
    public class PickupManager : Manager_Base<PickupManager>
    {
        private List<Pickup> nearbypickups = new();
        public int GetNearbyCount { get { return nearbypickups.Count;} }
        public Pickup GetNearby(int index) => nearbypickups[index];

        public void SetNearby(Pickup pickup, bool nearby)
        {
            if (nearbypickups.Contains(pickup))
            {
                if (nearby)
                    return;

                nearbypickups.Remove(pickup);
                return;
            }

            if(nearby)
                nearbypickups.Add(pickup);
        }
    }
}