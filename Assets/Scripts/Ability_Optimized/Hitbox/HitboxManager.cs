using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AbilityOP
{
    public class HitboxManager : Manager_Base<HitboxManager>, IMediator<Hitbox>
    {
        private Dictionary<string, Hitbox> m_hitboxes = new();
        private List<Hitbox> m_hitbox_pool = new();
        private List<Hitbox> m_hitbox_active = new();

        public void Notify(string notification, Hitbox notifier)
        {
            if (notification == Notification.HITBOX_NOTIFICATIONS.RETURN_TO_POOL)
            {
                notifier.gameObject.SetActive(false);
                m_hitbox_active.Remove(notifier);
                m_hitbox_pool.Add(notifier);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Object[] o = Resources.LoadAll("Hitboxes", typeof(GameObject));
            foreach (Object obj in o)
            {
                GameObject go = obj as GameObject;
                Hitbox hitbox = go.GetComponent<Hitbox>();

                if(hitbox != null)
                    m_hitboxes[go.name] = hitbox;
            }
        }

        public Hitbox RequestHitbox(string hitbox_type, List<string> filters = null)
        {
            if (!m_hitboxes.ContainsKey(hitbox_type))
                return null;

            List<Hitbox> hitbox_of_type = m_hitbox_pool.Where(hitbox => hitbox.name == hitbox_type).ToList();

            if (hitbox_of_type.Count > 0)
            {
                Hitbox to_return = hitbox_of_type[0];
                m_hitbox_pool.RemoveAt(0);
                to_return.SetFilters(filters);
                m_hitbox_active.Add(to_return);
                return to_return;
            }

            GameObject new_hitbox_gameobject = Instantiate(m_hitboxes[hitbox_type].gameObject);
            Hitbox new_hitbox = new_hitbox_gameobject.GetComponent<Hitbox>();
            new_hitbox.m_hitbox_mediator = this;
            new_hitbox.SetFilters(filters);
            new_hitbox.GetComponent<Collider>().isTrigger = true;   
            m_hitbox_active.Add(new_hitbox);

            return new_hitbox;

        }

    }
}
