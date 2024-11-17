using Assets.Scripts.Controller.Attachments;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Progression;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CreeperController : MonoBehaviour
    {
        [SerializeField] private float fadeDistance = 3;
        [SerializeField] private float baseOpacity = 0.1f;
        [SerializeField] private float fadeRate = 0.01f;
        [SerializeField] private EnemyController enemyController;

        private SpriteRenderer spriteRenderer;

        private float currentOpacity;

        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            enemyController.FireReady = false;
        }
        private void setSpriteAlpha()
        {
            Color col = spriteRenderer.color;
            col.a = currentOpacity;
            spriteRenderer.color = col;
        }

        private void Update()
        {   
            if (enemyController.Target == null)
                return;

            if (Vector3.Distance(enemyController.Target.transform.position, transform.position) > fadeDistance)
            {
                if (currentOpacity > baseOpacity) currentOpacity -= fadeRate;
                enemyController.FireReady = false;

            }
            else // within range
            {
                if (currentOpacity < 1) currentOpacity += fadeRate;
                else
                {
                    
                    currentOpacity = 1;
                    enemyController.FireReady = true;
                }
            }
            setSpriteAlpha();

        }
    }
}