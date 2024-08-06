using Assets.Scripts.Gameplay.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private UnityEvent<string> update_text;

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

        if (tracked_condition.count >= tracked_condition.threshold)
        {
            Next();
        }

        Instance.UpdateText();

        return true;
    }

    private void UpdateText()
    {
        var ret = "";

        foreach (var condition in tracked_conditions)
        {
            ret += condition.condition_name + " = " + condition.count + "/" + condition.threshold + System.Environment.NewLine;
        }

        update_text.Invoke(ret);
    }
}
