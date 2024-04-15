using System;

public class TutorialRequestArgs : EventArgs
{
    public Tutorial TutorialIdentificator;
    public GeneralEvent TutorialStartEvent;

    public TutorialRequestArgs(Tutorial tutorialIdentificator, GeneralEvent tutorialStartEvent)
    {
        TutorialIdentificator = tutorialIdentificator;
        TutorialStartEvent = tutorialStartEvent;
    }
}
