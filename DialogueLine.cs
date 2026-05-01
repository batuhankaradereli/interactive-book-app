[System.Serializable]
public class DialogueLine
{
    public string character;
    public string text;
    public string portrait;
    public string background;
}

[System.Serializable]
public class DialogueData
{
    public DialogueLine[] Items;
}


[System.Serializable]
public class DialogueLineAlt
{
    public string speaker;
    public string text;
    public string emotion;
    public string background;
}

[System.Serializable]
public class DialogueDataAlt
{
    public DialogueLineAlt[] lines;
}
