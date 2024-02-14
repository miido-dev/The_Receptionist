using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayController : MonoBehaviour,IDayController
{
    public int DayCount = 1;
    public int CountTimer;
    public TextMeshProUGUI DayText;

    public void IncreaseDayCount()
    {
        if (CountTimer > 2)
        {
            CountTimer = 0;
            DayCount++;
        }
        CountTimer++;
        DayText.text = $"Day {DayCount}";
    }
    
    public void ResetDayCount()
    {
        DayCount = 1;
        DayText.text = DayCount.ToString();
    }
}
