using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterCustomizer : MonoBehaviour
{
    public Image FaceImage;
    public Image ShirtImage;

    private void Awake()
    {
        EventManager.OnCharacterCustomizerTrigger += CustomizeCharacter;
    }

    public void CustomizeCharacter()
    {
        FaceImage.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        ShirtImage.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
