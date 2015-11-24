using UnityEngine;
using System.Collections;
using Assets.Core.Achievements;

public class LoadAchievements : MonoBehaviour
{

    private AchievementManager _am;

    void Start()
    {
        _am = AchievementManager.Instance;    
    }

    public void Show()
    {
        _am.ShowAchievements();
    }
}
