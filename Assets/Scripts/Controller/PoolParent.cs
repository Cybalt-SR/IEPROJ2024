using Assets.Scripts.Library;
using UnityEngine;

public class PoolParent : MonoBehaviour, ISingleton<PoolParent>
{
    private void Awake()
    {
        ISingleton<PoolParent>.Instance = this;
    }
}
