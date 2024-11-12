using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Manager
{
    public class Manager_Base<T> : MonoSingleton<T> where T : MonoBehaviour
    {
    }
}