using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : Manager_Base<AreaManager>
{
    class AutonextConditionTracker
    {
        public string condition_name;
        public int count;
        public int threshold;
    }

    [SerializeField] private List<AutonextConditionTracker> tracked_conditions = new();

    public static void Next()
    {
        Instance.tracked_conditions.Clear();

        Debug.Log("TEMP CODE");
        SceneManager.LoadScene("win");
    }

    public static bool TrackOrCreate(string condition_name, int threshold)
    {
        var tracked_condition = Instance.tracked_conditions.Find(condition => condition.condition_name == condition_name);

        if (tracked_condition == null)
        {
            Instance.tracked_conditions.Add(new()
            {
                condition_name= condition_name,
                count = 1,
                threshold = threshold
            });
            return true;
        }

        tracked_condition.count++;
        return false;
    }

    public static bool Track(string condition_name)
    {
        var tracked_condition = Instance.tracked_conditions.Find(condition => condition.condition_name == condition_name);

        if (tracked_condition == null)
            return false;

        tracked_condition.count++;
        return true;
    }

    private void Update()
    {
        foreach (var condition in tracked_conditions)
        {
            if(condition.count >= condition.threshold)
            {
                Next();
            }
        }
    }
}
