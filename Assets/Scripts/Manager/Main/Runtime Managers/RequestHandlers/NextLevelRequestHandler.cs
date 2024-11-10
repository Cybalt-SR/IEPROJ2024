using Assets.Scripts.Data.ActionRequestTypes;
using Assets.Scripts.Input;
using Assets.Scripts.Library.ActionBroadcaster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelRequestHandler : ActionRequestHandler<NextLevelActionRequest>
{
    [SerializeField] private int currentLevel = 0;
    public int CurrentLevel { get { return currentLevel; } }

    protected override bool ProcessRequest(NextLevelActionRequest somerequest)
    {
        LoadingScreen.LoadScreen(() =>
        {
            var oldmap = FindAnyObjectByType<LevelController>().gameObject;
            var allMonoBehaviours = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None);

            foreach (var component in allMonoBehaviours)
            {
                if (component is IOnLevelLoad onLevelLoad)
                    onLevelLoad.OnLevelExit(oldmap);
            }

            Destroy(oldmap);

            currentLevel++;
            if(currentLevel == LevelDictionary.Instance.Count)
            {
                SceneManager.LoadScene("win");
                return;
            }

            var newmap = Instantiate(LevelDictionary.Instance.Get(currentLevel));
            
            allMonoBehaviours = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None);

            foreach (var component in allMonoBehaviours)
            {
                if(component is IOnLevelLoad onLevelLoad)
                    onLevelLoad.OnLevelLoad(newmap);
            }
        });

        return true;
    }
}
