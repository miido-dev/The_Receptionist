using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TweenController : MonoBehaviour
{
    public RectTransform MeterPanelTransform;
    public RectTransform DayPanelTransform;
    public RectTransform CharacterTransform;
    public RectTransform BellTransform;
    public GameObject BellEffect;
    public RectTransform SpeechBubbleTransform;
    public RectTransform OptionTransform;
    public AudioSource SFXAudio;
    public GameObject FadeInObject;
    public GameObject FadeOutObject;

    [SerializeField] private float MeterPanelPosition = 1060;
    [SerializeField] private float DayPanelPosition = 1080;
    [SerializeField] private float CharacterPosition;
    [SerializeField] private float OptionPosition;

    private void Awake()
    {
        EventManager.OptionTweenTrigger += OptionTween;
        EventManager.FadeInTrigger += FadeIn;
        EventManager.OnQuestionChangeTweenTrigger += OnQuestionUpdate;
    }

    private void Start()
    {
        DOTween.Sequence()
            .Append(MeterPanelTransform.DOLocalMoveY(MeterPanelPosition, 1f))
            .Append(DayPanelTransform.DOLocalMoveY(DayPanelPosition, 1f))
            
            .AppendCallback(() => EventManager.OnCharacterCustomizerTrigger?.Invoke())
            .Append(CharacterTransform.DOLocalMoveX(CharacterPosition, 2f))
            
            .AppendCallback(() => SFXAudio.Play())
            .AppendCallback(() => BellEffect.SetActive(true))
            
            .Append(BellTransform.DOScale(new Vector3(2f, 2f,2f), 0.125f))
            .Append(BellTransform.DOScale(new Vector3(1,1,1), 0.125f))
            
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,25f), 0.1f))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,-25f), 0.1f))
            
            .AppendCallback(() => BellEffect.SetActive(false))
            
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,0), 0.1f))
            .Append(SpeechBubbleTransform.DOScale(new Vector3(1, 1,1), 0.15f))
            
            .AppendCallback(() => EventManager.OnStartTrigger?.Invoke());
    }

    [Button]
    public void RestartTween()
    {
        ResetTween()
            .Append(MeterPanelTransform.DOMoveY(MeterPanelPosition, 1f))
            .Append(DayPanelTransform.DOMoveY(DayPanelPosition, 1f))
            
            .AppendCallback(() => EventManager.OnCharacterCustomizerTrigger?.Invoke())
            .Append(CharacterTransform.DOLocalMoveX(CharacterPosition, 2f))
            
            .AppendCallback(() => SFXAudio.Play())
            .AppendCallback(() => BellEffect.SetActive(true))
            .Append(BellTransform.DOScale(new Vector3(2f, 2f,2f), 0.125f))
            .Append(BellTransform.DOScale(new Vector3(1,1,1), 0.125f))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,25f), 0.1f))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,-25f), 0.1f))
            .AppendCallback(() => BellEffect.SetActive(false))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,0), 0.1f))
            .Append(SpeechBubbleTransform.DOScale(new Vector3(1, 1,1), 0.15f))
            .AppendCallback(() => EventManager.OnStartTrigger?.Invoke());
    }

    public void OptionTween()
    {
        OptionTransform.DOLocalMoveY(OptionPosition, 0.35f);
    }

    public Sequence ResetTween()
    {
        var seq = DOTween.Sequence()
            .Append(MeterPanelTransform.DOMoveY(2060, 1f))
            .Append(DayPanelTransform.DOMoveY(2080, 1f))
            .Append(SpeechBubbleTransform.DOScale(new Vector3(0, 0, 0), 0.15f))
            
            .Append(CharacterTransform.DOLocalMoveX(5000, 2.5f))
            .Append(CharacterTransform.DOLocalMoveX(-5000, 0f))
            
            .Append(OptionTransform.DOMoveY(-1500, 0.35f))
            .AppendCallback(() => EventManager.OnStartTrigger?.Invoke());
        return seq;
    }

    public void OnQuestionUpdate()
    {
        DOTween.Sequence()
            .Append(SpeechBubbleTransform.DOScale(new Vector3(0, 0, 0), 0.15f))
            
            .Append(CharacterTransform.DOLocalMoveX(5000, 2.5f))
            .Append(CharacterTransform.DOLocalMoveX(-5000, 0f))
            
            .Append(OptionTransform.DOMoveY(-1500, 0.35f))

            .Append(CharacterTransform.DOLocalMoveX(CharacterPosition, 2f))
            
            .AppendCallback(() =>
            {
                SFXAudio.pitch = Random.Range(1, 1.35f);
                SFXAudio.Play();
            })
            .AppendCallback(() => BellEffect.SetActive(true))
            .Append(BellTransform.DOScale(new Vector3(2f, 2f,2f), 0.125f))
            .Append(BellTransform.DOScale(new Vector3(1,1,1), 0.125f))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,25f), 0.1f))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,-25f), 0.1f))
            .AppendCallback(() => BellEffect.SetActive(false))
            .Append(BellTransform.DOLocalRotate(new Vector3(0, 0,0), 0.1f))
            .AppendCallback(() => EventManager.OnStartTrigger?.Invoke()).SetDelay(0.1f)
            .Append(SpeechBubbleTransform.DOScale(new Vector3(1, 1, 1), 0.15f));


    }

    private void FadeIn()
    {
        StartCoroutine(FadeInOutEffect());
    }

    private IEnumerator FadeInOutEffect()
    {
        FadeInObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        FadeInObject.SetActive(false);
        FadeOutObject.SetActive(true);
        yield return new WaitForSeconds(1);
        FadeOutObject.SetActive(false);
    }
}
