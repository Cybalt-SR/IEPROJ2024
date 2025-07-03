using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AbilityOP;

public abstract class Ownable
{
    protected GameObject m_owner;
    protected List<Transform> m_ownable_assets = new();
    public GameObject Owner
    {
        get => m_owner;
    }

    public virtual bool SetOwner(GameObject owner, bool transfer_assets = true)
    {
        if (owner && owner != m_owner)
        {
            if (m_owner)
                OnOwnerChanging(m_owner, owner);
            m_owner = owner;
            OnOwnerSetting(owner);

            return true;
        }
        return false;
    }

    public virtual bool RemoveOwner()
    {
        if (!m_owner)
            return false;

        OnOwnerRemoving(m_owner);
        m_owner = null;

        return true;
    }

    protected virtual void OnOwnerSetting(GameObject owner)
    {
        foreach (Transform t in m_ownable_assets)
        {
            t.parent = Owner.transform;
        }
    }

    protected virtual void OnOwnerRemoving(GameObject owner)
    {
        foreach (Transform ownable in m_ownable_assets)
        {
            AssetRequester.DepositAsset(ownable.gameObject.name, ownable.gameObject);
        }
    }

    protected virtual void OnOwnerChanging(GameObject old_owner, GameObject new_owner) 
    { 

    }

    public virtual void Update(float deltaTime) { }


}
