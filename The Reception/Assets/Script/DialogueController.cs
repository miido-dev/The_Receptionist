using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueController : MonoBehaviour
{
    #region Variables

    public int currentDialogueIndex;
    [Range(0,4)] public int dayIndex;
    public DialogueActor[] dialogueActor;
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI[] OptionButtonText;
    public Button[] OptionButton;
    public DialogueObject DialogueDataObjectData = new();
    public TextAsset jsonData;
    public int QuestionIndex = -1;

    private bool isLastQuestion;
    private int currentQuestionIndex;
    private int[] DoneQuestionNo;
    private int questionCount;
    public IDayController DayController;
    #endregion
    private void Awake()
    {
        EventManager.OnStartTrigger += OnStart;
        EventManager.OnQuestionChangeTweenTrigger += OnQuestionUpdate;
        EventManager.GetLostFoundItemPlacementListTrigger += ()=> LostAndFoundController.GenerateRandomNumbers(4, 0, 4);
    }

    private void Start()
    {
        //day count increase after 3 questions
        //option tween back
        //character tween forward
        //question update
        
        JsonDataDeserialization();
        
        OptionButton[0].onClick.AddListener(() =>
        {
            StartCoroutine(TypeWriter(DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Reactions[0],true));
            TurnOffButton();
            DayController.IncreaseDayCount();
        });
        OptionButton[1].onClick.AddListener(() =>
        {
            StartCoroutine(TypeWriter(DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Reactions[1],true));
            TurnOffButton();
            DayController.IncreaseDayCount();

        });
        OptionButton[2].onClick.AddListener(() =>
        {
            StartCoroutine(TypeWriter(DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Reactions[2],true));
            TurnOffButton();
            DayController.IncreaseDayCount();

        });
    }

    private void TurnOffButton()
    {
        OptionButton[0].interactable = false;
        OptionButton[1].interactable = false;
        OptionButton[2].interactable = false;
    }

    private void OnStart()
    {
        OptionButton[0].interactable = true;
        OptionButton[1].interactable = true;
        OptionButton[2].interactable = true;
        // DoneQuestionNo = new int[DialogueDataObjectData.DialogueDataList.Length];
        currentQuestionIndex = QuestionIndex == -1 ? Random.Range(0, DialogueDataObjectData.DialogueDataList.Length) : QuestionIndex;
        // DoneQuestionNo[0] = currentQuestionIndex;
        StartCoroutine(TypeWriter(DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Dialogue,false));

        OptionButtonText[0].text = DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Options[0];
        OptionButtonText[1].text = DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Options[1];
        OptionButtonText[2].text = DialogueDataObjectData.DialogueDataList[currentQuestionIndex].Options[2];
    }

    private IEnumerator TypeWriter(string textToWrite,bool isEnd)
    {
        DialogueText.text = "";
        foreach (var letter in textToWrite.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        if (isEnd)
        {
            EventManager.OnQuestionChangeTweenTrigger.Invoke();
        }

        if (isLastQuestion)
        {
            EventManager.FadeInTrigger.Invoke();
        }
        EventManager.OptionTweenTrigger?.Invoke();
    }

    private void OnQuestionUpdate()
    {
        questionCount++;
        currentQuestionIndex = Random.Range(0, DialogueDataObjectData.DialogueDataList.Length);
    }

    private void JsonDataDeserialization()
    {
        // jsonData = Resources.Load<TextAsset>($"DialoguesData.txt");
        DialogueDataObjectData = JsonUtility.FromJson<DialogueObject>(jsonData.text);
    }
}

[Serializable]
public struct DialogueActor
{
    [FormerlySerializedAs("dialogues")] [FoldoutGroup("$Dialogues")] [TextArea(2,5)] public string Dialogues;
    [FoldoutGroup("$Dialogues")] public bool isEnd;
    [FoldoutGroup("$Dialogues")] public bool isQuestion;
    [FoldoutGroup("$Dialogues")] [TextArea(2,5)] public string[] Option;
    [FoldoutGroup("$Dialogues")] [TextArea(2,5)] public string[] Reaction;
    [FoldoutGroup("$Dialogues")] [Range(0,5)]public int[] TimelinessCount;
    [FoldoutGroup("$Dialogues")] [Range(0,5)]public int[] CurrentEmotion;
    [FoldoutGroup("$Dialogues")] [Range(0,5)]public int[] CurrentTiredness;
    [FoldoutGroup("$Dialogues")] public UnityEvent onDialogueStart;
    [FoldoutGroup("$Dialogues")] public UnityEvent onDialogueEnd;
}
[Serializable]
public struct DialogueData
{
    [FoldoutGroup("$Dialogue")] [TextArea(2,5)] public string Dialogue;
    [FoldoutGroup("$Dialogue")] [TextArea(2,5)] public string[] Options;
    [FoldoutGroup("$Dialogue")] [TextArea(2,5)] public string[] Reactions;
    [FoldoutGroup("$Dialogue")] [Range(0,5)]public int[] Greatness;
}

[Serializable]
public class DialogueObject
{
    public DialogueData[] DialogueDataList;
}
