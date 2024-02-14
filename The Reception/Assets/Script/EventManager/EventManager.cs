
using System;
using System.Collections.Generic;

public partial class EventManager
{
  public static Action OnStartTrigger;
  public static Action OptionTweenTrigger;
  public static Action OnCharacterCustomizerTrigger;
  public static Action FadeInTrigger;
  public static Action OnQuestionChangeTweenTrigger;
  public static Action ItemsPlacedCheckTrigger;
  public static Func<List<int>> GetLostFoundItemPlacementListTrigger;
}
