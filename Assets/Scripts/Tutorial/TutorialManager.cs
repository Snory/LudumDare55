using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public Dictionary<Tutorial, bool> TutorialSeen;

    private Tutorial _currentTutorial;
    private int _currentTextIndex = 0;

    private void Start()
    {
        TutorialSeen = new Dictionary<Tutorial, bool>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(PlayerPrefs.HasKey(_currentTutorial.name))
            {
                PlayerPrefs.DeleteKey(_currentTutorial.name);
                RequestTutorial(_currentTutorial);
                _currentTextIndex = 0;
            }
        }
    }

    public void RequestTutorial(Tutorial tutorial)
    {
        // check if the tutorial has been seen before based on playerprefs
        _currentTutorial = tutorial;

        if (PlayerPrefs.HasKey(tutorial.name))
        {
            return;
        }

        tutorial.TutorialStartEvent.Raise();

        Time.timeScale = 0;

        //store info in playerprefs to know that this tutorial has been seen
        PlayerPrefs.SetInt(tutorial.name, 1);
    }

    public string NextText()
    {

        if(_currentTextIndex < _currentTutorial.TutorialTexts.Count)
        {
            var text = _currentTutorial.TutorialTexts[_currentTextIndex];
            _currentTextIndex++;

            return text;
        } else
        {
            CurrentTutorialEnded();
            return null;
        }
    }

    public void CurrentTutorialEnded()
    {
        UnityEngine.Time.timeScale = 1;
        _currentTutorial.TutorialEndEvent.Raise();
    }
}
