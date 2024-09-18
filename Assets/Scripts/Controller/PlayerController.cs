using Assets.Scripts.Data.Progression;
using Assets.Scripts.Input;
using Assets.Scripts.Library;
using Assets.Scripts.Manager;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controller
{
    public class PlayerController : UnitController, IPlayerInputReceiver
    {
        public static PlayerController GetFirst()
        {
            return FindObjectsByType<PlayerController>(FindObjectsSortMode.None).FirstOrDefault();
        }

        public static void SetGunChanged(string id)
        {
           var player = FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Where(player => player.playerId == id).FirstOrDefault();

            if (player == null) return;

            player.GunChanged = true;
        }

        [SerializeField] private string playerId;
        [SerializeField] private Collider aiming_blanket;
        public string PlayerId { get { return playerId; } }

        bool IPlayerInputReceiver.IsFire { get; set; }

        //input
        private Vector2 late_aimpos;
        private Vector2 screenpos;

        protected override void UpdateFinalGun()
        {
            base.UpdateFinalGun();

            foreach (var item in IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerId))
            {
                item.Value.ApplyThis(ref mFinalGun);
            }
        }

        void IPlayerInputReceiver.Move(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
                MoveDir = Vector2.zero;

            var dir2 = callback.ReadValue<Vector2>();

            MoveDir = transform.right * dir2.x + transform.forward * dir2.y;

            bool received = false;
            ActionBroadcaster.Broadcast("player_move", this.transform, ref received);
        }

        void IPlayerInputReceiver.Aim(InputAction.CallbackContext callback)
        {
            late_aimpos = callback.ReadValue<Vector2>();

            if (aiming_blanket.Raycast(CameraController.Instance.Camera.ScreenPointToRay(late_aimpos), out RaycastHit hitinfo, float.PositiveInfinity))
            {
                AimAt(hitinfo.point);
            }
            else
            {
                screenpos = (Vector2)CameraController.Instance.Camera.WorldToScreenPoint(transform.position);

                var delta2 = late_aimpos - screenpos;
                var deltaConverted = Vector3.zero;
                deltaConverted.x = delta2.x;
                deltaConverted.z = delta2.y;

                AimAt(transform.position + deltaConverted);
            }
        }

        void IPlayerInputReceiver.Look(InputAction.CallbackContext callback)
        {
        }

        void IPlayerInputReceiver.Fire(InputAction.CallbackContext callback)
        {
        }

        protected override void Update()
        {
            base.Update();

            if ((this as IPlayerInputReceiver).IsFire && EventSystem.current.IsPointerOverGameObject() == false)
            {
                bool received = false;
                ActionBroadcaster.Broadcast("player_shoot", this.transform, ref received);
                base.Fire();
            }
        }

        void IPlayerInputReceiver.Pickup(InputAction.CallbackContext callback)
        {
            PickupManager.Pickup(this.PlayerId);
        }
    }
}