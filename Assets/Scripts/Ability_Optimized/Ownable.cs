using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ownable
{
    protected GameObject m_owner;
    protected List<Transform> m_ownable_assets;

    protected Action<GameObject> OnOwnerSet;
    protected Action<GameObject> OnOwnerRemoving;
    protected Action<GameObject, GameObject> OnOwnerChanged;

    public GameObject Owner
    {
        get => m_owner;
    }

    public virtual bool SetOwner(GameObject owner, bool transfer_assets = true)
    {
        if (owner && owner != m_owner)
        {
            if (m_owner)
                OnOwnerChanged?.Invoke(m_owner, owner);
            m_owner = owner;
            OnOwnerSet?.Invoke(owner);

            return true;
        }
        return false;
    }

    public virtual bool RemoveOwner()
    {
        if (!m_owner)
            return false;

        OnOwnerRemoving?.Invoke(m_owner);
        m_owner = null;

        return true;
    }



}
