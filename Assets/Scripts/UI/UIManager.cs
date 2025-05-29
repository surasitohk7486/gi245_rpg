using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    public RectTransform SelectionBox { get { return selectionBox; } }

    [SerializeField]
    private Toggle togglePauseUnpause;

    [SerializeField]
    private Toggle[] toggleMagic;
    public Toggle[] ToggleMagic { get { return toggleMagic; } }

    [SerializeField]
    private int curToggleMagicID = -1;

    [SerializeField]
    private GameObject blackImage;

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject grayImage;

    [SerializeField]
    private GameObject itemDialog;

    [SerializeField]
    private GameObject itemUIPrefabs;

    [SerializeField]
    private GameObject[] slots;

    [SerializeField]
    private ItemDrag curItemDrag;

    [SerializeField]
    private int curSlotId;

    [SerializeField]
    private GameObject downPanel;

    [SerializeField]
    private GameObject npcDialougePanel;

    [SerializeField]
    private Image npcImage;

    [SerializeField]
    private TMP_Text npcNameText;

    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private int index;

    [SerializeField]
    private GameObject btnNext;

    [SerializeField]
    private TMP_Text btnNextText;

    [SerializeField]
    private GameObject btnAccept;

    [SerializeField]
    private TMP_Text btnAcceptText;

    [SerializeField]
    private GameObject btnReject;

    [SerializeField]
    private TMP_Text btnRejectText;

    [SerializeField]
    private GameObject btnFinish;

    [SerializeField]
    private TMP_Text btnFinishText;

    [SerializeField]
    private GameObject btnNotFinish;

    [SerializeField]
    private TMP_Text btnNotFinishText;

    [SerializeField]
    private Toggle[] toggleAvatar;
    public Toggle[] ToggleAvatar { get { return toggleAvatar; } set { toggleAvatar = value; } }

    [SerializeField]
    private GameObject charPanel;

    [SerializeField]
    private TMP_Text charNameText;

    [SerializeField]
    private TMP_Text statText;

    [SerializeField]
    private TMP_Text abilityText;

    [SerializeField]
    private Image heroImage;

    [SerializeField]
    private GameObject partyPanel;

    [SerializeField]
    private Toggle[] toggleRemove;

    [SerializeField]
    private int idToRemove = -1;

    [SerializeField]
    private Button removeButton;

    [SerializeField]
    private GameObject confirmPanel;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitSlots();
        MapToggleAvatar();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            togglePauseUnpause.isOn = !togglePauseUnpause.isOn;
    }

    public void ToggleAI(bool isOn)
    {
        foreach (Characters member in PartyManager.instance.Members)
        {
            AttackAI ai = member.gameObject.GetComponent<AttackAI>();

            if (ai != null)
            {
                ai.enabled = isOn;
            }
        }
    }

    public void SelectAll()
    {
        foreach (Characters member in PartyManager.instance.Members)
        {
            if (member.CurHP > 0)
            {
                member.ToggleRingSelection(true);
                PartyManager.instance.SelectChars.Add(member);

            }
        }
    }

    public void PauseUnpause(bool isOn)
    {
        Time.timeScale = isOn ? 0 : 1;
    }

    public void ShowMagicToggle()
    {
        if (PartyManager.instance.SelectChars.Count <= 0)
        {
            return;
        }

        Characters hero = PartyManager.instance.SelectChars[0];

        for (int i =0; i < hero.MagicSkills.Count; i++)
        {
            toggleMagic[i].interactable = true;
            toggleMagic[i].isOn = false;
            toggleMagic[i].GetComponentInChildren<Text>().text = hero.MagicSkills[i].Name;
            toggleMagic[i].targetGraphic.GetComponent<Image>().sprite = hero.MagicSkills[i].Icon;
        }
    }

    public void SelectMagicSkill(int i)
    {
        curToggleMagicID = i;
        PartyManager.instance.HeroSelectMagicSkill(i);
    }

    public void IsOnCurToggleMagic(bool flag)
    {
        toggleMagic[curToggleMagicID].isOn = flag;
    }

    public void ToggleInventorypanel()
    {
        if(!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(true);
            blackImage.SetActive(true);
            ShowInventory();
        }
        else
        {
            inventoryPanel.SetActive(false);
            blackImage.SetActive(false);
            ClearInventory();
        }
    }

    public void ShowInventory()
    {
        if (PartyManager.instance.SelectChars.Count <= 0)
        {
            return;
        }

        Characters hero = PartyManager.instance.SelectChars[0];

        for (int i = 0; i < InventoryManager.MAXSLOT; i++)
        {
            if (hero.InventoryItems[i] != null)
            {
                GameObject itemObj = Instantiate(itemUIPrefabs, slots[i].transform);
                ItemDrag itemDrag = itemObj.GetComponent<ItemDrag>();

                itemDrag.UIManager = this;

                itemDrag.Item = hero.InventoryItems[i];
                itemDrag.IconParent = slots[i].transform;
                itemDrag.Image.sprite = hero.InventoryItems[i].Icon;
            }

        }
    }

    public void ClearInventory()
    {
        for (int i = 0;i < slots.Length;i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                Transform child = slots[i].transform.GetChild(0);
                Destroy(child.gameObject);
            }
        }
    }

    private void InitSlots()
    {
        for (int i = 0;i < InventoryManager.MAXSLOT; i++)
        {
            slots[i].GetComponent<InventorySlot>().ID = i;
        }
    }

    public void SetCurItemInUse(ItemDrag itemDrag, int index)
    {
        curItemDrag = itemDrag;
        curSlotId = index;
    }

    public void ToggleItemDialog(bool flag)
    {
        grayImage.SetActive(flag);
        itemDialog.SetActive(flag);
    }

    public void DeleteItemIcon()
    {
        Destroy(curItemDrag.gameObject);
    }

    public void ClickDrinkConsumable()
    {
        InventoryManager.instance.DrinkConsumableItem(curItemDrag.Item, curSlotId);
        DeleteItemIcon();
        ToggleItemDialog(false);
    }

    public void ClearDialogueBox()
    {
        npcImage.sprite = null;

        npcNameText.text = "";
        dialogueText.text = "";

        btnNextText.text = "";
        btnNext.SetActive(false);

        btnAcceptText.text = "";
        btnAccept.SetActive(false);

        btnRejectText.text = "";
        btnReject.SetActive(false);

        btnFinishText.text = "";
        btnFinish.SetActive(false);

        btnNotFinishText.text = "";
        btnNotFinish.SetActive(false);
    }

    private void StartQuestDialogue(Quest quest)
    {
        dialogueText.text = quest.QuestDialogue[index];

        btnNext.SetActive(true);
        btnNextText.text = quest.AnswerNext[index];

        btnAccept.SetActive(false);
        btnReject.SetActive(false);
    }

    private void SetupDialoguePanel(Npc npc)
    {
        index = 0;

        npcImage.sprite = npc.AvatarPic;
        npcNameText.text = npc.CharName;

        Quest inProgressQuest = QuestManager.instance.CheckForQuest(npc, QuestStatus.InProgess);

        if(inProgressQuest != null)
        {
            Debug.Log($"in-progress: {inProgressQuest}");
            dialogueText.text = inProgressQuest.QuestionInProgress;

            bool hasItem = QuestManager.instance.CheckIfFinishQuest();
            Debug.Log(hasItem);

            if(hasItem)
            {
                btnFinishText.text = inProgressQuest.AnswerFinish;
                btnFinish.SetActive(true);
            }
            else
            {
                btnNotFinishText.text = inProgressQuest.AnswerNotFinish;
                btnNotFinish.SetActive(true);
            }
        }
        else
        {
            Quest newQuest = QuestManager.instance.CheckForQuest(npc, QuestStatus.New);

            if(newQuest != null)
            {
                StartQuestDialogue(newQuest);
            }
        }
    }

    private void ToggleDialogueBox(bool flag)
    {
        downPanel.SetActive(!flag);
        npcDialougePanel.SetActive(flag);
        togglePauseUnpause.isOn = flag;
    }

    public void PrepareDialogueBox(Npc npc)
    {
        ClearDialogueBox();
        SetupDialoguePanel(npc);
        ToggleDialogueBox(true);
    }

    public void AnswerNext()
    {
        index++;
        dialogueText.text = QuestManager.instance.NextDialogue(index);

        if(QuestManager.instance.CheckLastDialogue(index))
        {
            btnNext.SetActive(false);

            btnAcceptText.text = QuestManager.instance.CurQuest.AnswerAccept;
            btnAccept.SetActive(true);

            btnRejectText.text = QuestManager.instance.CurQuest.AnswerReject;
            btnReject.SetActive(true);
        }
        else
        {
            btnNext.SetActive(true);
            btnNextText.text = QuestManager.instance.CurQuest.AnswerNext[index];
        }
    }

    public void AnswerReject()
    {
        QuestManager.instance.RejectQuest();
        ToggleDialogueBox(false);
    }

    public void AnswerAccept()
    {
        QuestManager.instance.AcceptQuest();
        ToggleDialogueBox(false);
    }

    public void AnswerFinish()
    {
        Debug.Log("Can Finish Quest");
        bool success = QuestManager.instance.DeliveryItem();

        if(success)
        {
            if(QuestManager.instance.NpcGiveReward())
            {
                Debug.Log("Quest Comleted");
                ToggleDialogueBox(false);
            }
        }
            
    }

    public void AnswerNotFinish()
    {
        Debug.Log("Cannot Finish Quest");
        ToggleDialogueBox(false);
    }

    public void MapToggleAvatar()
    {
        foreach(Toggle t in toggleAvatar)
            t.gameObject.SetActive(false);

        for(int i = 0; i < PartyManager.instance.Members.Count; i++)
        {
            toggleAvatar[i].gameObject.SetActive(true);
        }
        toggleAvatar[0].isOn = true;
    }

    public void SelectHeroByAvatar(int i)
    {
        if(toggleAvatar[i].isOn)
        {
            PartyManager.instance.SelectSingleHeroByToggle(i);
        }
        else
        {
            PartyManager.instance.UnSelectSingleHeroToggle(i);
        }
    }

    public void ClearCharPanel()
    {
        charNameText.text = "";
        statText.text = "";
        abilityText.text = "";
        heroImage.sprite = null;
    }

    public void ShowCharPanel()
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;
        Hero hero = (Hero)PartyManager.instance.SelectChars[0];

        charNameText.text = hero.CharName;

        string stat = string.Format
            ("Level: {0}\nExperience: {1}\n" +
            "Attack Damage: {2}\nDefense Power: {3}"
            , hero.Level, hero.Exp,
            hero.AttackDamage, hero.DefensePower);

        statText.text = stat;

        string ability = string.Format
            ("Strength: {0}\nDexterity: {1}\n" +
            "Constitution: {2}\nIntelligence: {3}\n" +
            "Wisdom: {4}\nCharisma: {5}"
            , hero.Strength, hero.Dexterity,
            hero.Constitution, hero.Intelligence,
            hero.Wisdom, hero.Charisma);

        abilityText.text = ability;

        heroImage.sprite = hero.AvatarPic;
    }

    public void ToggleCharPanel()
    {
        if (!charPanel.activeInHierarchy)
        {
            charPanel.SetActive(true);
            blackImage.SetActive(true);
            ShowCharPanel();
        }
        else
        {
            charPanel.SetActive(false);
            blackImage.SetActive(false);
            ClearCharPanel();
        }
    }

    public void MapToggleRemove()
    {
        foreach(Toggle t in toggleRemove)
            t.gameObject.SetActive(false);

        List<Characters> members = PartyManager.instance.Members;

        for(int i = 1; i < members.Count; i++)
        {
            toggleRemove[i - 1].gameObject.SetActive(true);
            toggleRemove[i - 1].targetGraphic.GetComponent<Image>().sprite = members[i].AvatarPic;
        }
    }

    private void CheckRemoveButton()
    {
        switch(idToRemove)
        {
            case -1:
            case 0:
                removeButton.interactable = false; break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                removeButton.interactable = true; break;
            default:
                removeButton.interactable = false; break;
                
        }
    }

    public void TogglePartyPanel(bool flag)
    {
        charPanel.SetActive(!flag);
        partyPanel.SetActive(flag);
        MapToggleRemove();
        CheckRemoveButton();
    }

    public void SelectToRemove(int i)
    {
        if (toggleRemove[i-1].isOn)
            idToRemove = i;
        else 
            idToRemove = -1;

        CheckRemoveButton();
    }

    public void ToggleConfirmPanel(bool flag)
    {
        if(flag == false)
        {
            MapToggleRemove();
            idToRemove = -1;
            CheckRemoveButton();
        }
        partyPanel.SetActive(!flag);
        confirmPanel.SetActive(flag);
    }

    public void RemoveMemberFromParty()
    {
        toggleAvatar[idToRemove].isOn = false;
        PartyManager.instance.RemoveHeroFromParty(idToRemove);
        MapToggleAvatar();
        ToggleConfirmPanel(false);
    }
}
