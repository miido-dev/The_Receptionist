using System;
using UnityEngine;

public class DepedencyInjector : MonoBehaviour
{
    [SerializeField] private DialogueController DialogueController;
    [SerializeField] private DayController _dayController;

    private void Awake()
    {
        DialogueController.DayController = _dayController;
    }
}