using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "Tutorial/Tutorial")]
public class Tutorial : ScriptableObject
{
    public GeneralEvent TutorialStartEvent;
    public GeneralEvent TutorialEndEvent;
    public bool PauseTime;

    public List<string> TutorialTexts;

}
