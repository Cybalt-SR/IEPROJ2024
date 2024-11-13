using Assets.Scripts.Data;
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
    public class PlayerController : UnitController, IPlayerInputReceiver, IOnLevelLoad
    {
        public override string BroadcastId => "player_" + PlayerId;
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
        private PlayerStateHandler stateHandler;
        private GunStatModifierHandler statModder = new();

        public GunStatModifierHandler StatModder { get => statModder; }

        protected override GunData DoOnGet(GunData data)
        {
            return statModder.ApplyStatMods(data);
        }

        protected override void Start()
        {
            base.Start();
            stateHandler = GetComponent<PlayerStateHandler>();
        }

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
            if (!stateHandler.canMove)
                return;

            if (callback.phase == InputActionPhase.Performed)
                MoveDir = Vector2.zero;

            var dir2 = callback.ReadValue<Vector2>();

            MoveDir = transform.right * dir2.x + transform.forward * dir2.y;

            ActionWaiter.Broadcast("player_move", this.transform, out _);
        }

        void IPlayerInputReceiver.Aim(InputAction.CallbackContext callback)
        {
            late_aimpos = callback.ReadValue<Vector2>();
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
            statModder.Update(Time.deltaTime);
            if ((this as IPlayerInputReceiver).IsFire && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if(base.Fire())
                    ActionWaiter.Broadcast("player_shoot", this.transform, out _);
            }

            if (aiming_blanket.Raycast(CameraController.Instance.Camera.ScreenPointToRay(late_aimpos), out RaycastHit hitinfo, float.PositiveInfinity))
            {
                AimAt(hitinfo.point);
            }

            //Temp
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                statModder.DebugStats();
        }

        void IPlayerInputReceiver.Pickup(InputAction.CallbackContext callback)
        {
            PickupManager.Pickup(this.PlayerId);
        }

        void IOnLevelLoad.OnLevelLoad(GameObject newLevel)
        {
            transform.position = new Vector3(0, 1, 0);
        }

        void IOnLevelLoad.OnLevelExit(GameObject curLevel)
        {
        }
    }
}