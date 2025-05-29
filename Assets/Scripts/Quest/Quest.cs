using UnityEngine;

public enum QuestType
{
    Delivery,
    KillCount
}

public enum QuestStatus
{
    New,
    InProgess,
    Finish,
    Reject
}

[System.Serializable]
public class Quest
{
    [SerializeField]
    private int questId;
    public int QuestID {  get { return questId; } }

    [SerializeField]
    private QuestType type;
    public QuestType Type { get { return type; } }

    [SerializeField]
    private QuestStatus status;
    public QuestStatus Status { get { return status; } set { status = value; } }

    [SerializeField]
    private string questName;
    public string QuestName { get { return questName; } }

    [SerializeField]
    private string questDetail;
    public string QuestDetail { get { return questDetail; } }

    [SerializeField]
    private int questItemId;
    public int QuestItemId { get { return questItemId; } }

    [SerializeField]
    private string[] questDialogue;
    public string[] QuestDialogue { get { return questDialogue; } }

    [SerializeField]
    private string[] answerNext;
    public string[] AnswerNext { get { return answerNext; } }

    [SerializeField]
    private string answerAccecpt;
    public string AnswerAccept { get { return answerAccecpt; } }

    [SerializeField]
    private string answerReject;
    public string AnswerReject { get { return answerReject; } }

    [SerializeField]
    private int rewardItemId;
    public int RewardItemId { get {return rewardItemId; } }

    [SerializeField]
    private int rewardExp;
    public int RewardExp { get { return rewardExp; } }

    [SerializeField]
    private string questionInProgress;
    public string QuestionInProgress { get { return questionInProgress; } }

    [SerializeField]
    private string answerFinish;
    public string AnswerFinish { get { return answerFinish; } }

    [SerializeField]
    private string answerNotFinish;
    public string AnswerNotFinish { get {return answerNotFinish; } }

    public Quest(QuestData data)
    {
        questId = data.questId;
        type = data.type;
        status = data.status;
        questName = data.questName;
        questDetail = data.questDetail;
        questItemId = data.questItemId;
        questDialogue = data.questDialogue;
        answerNext = data.answerNext;
        answerAccecpt = data.answerAccept;
        answerReject = data.answerReject;
        rewardItemId = data.rewardItemId;
        rewardExp = data.rewardExp;
        questionInProgress = data.questionInProgress;
        answerFinish = data.answerFinish;
        answerNotFinish = data.answerNotFinish;
    }
}
