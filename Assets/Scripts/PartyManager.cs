using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<Characters> members = new List<Characters>();
    public List<Characters> Members { get { return members; } } 

    [SerializeField]
    private List<Characters> selectChars = new List<Characters>();
    public List<Characters> SelectChars { get { return selectChars; } }

    [SerializeField]
    private List<Quest> questList = new List<Quest>();
    public List<Quest> QuestList { get { return questList; } }

    [SerializeField]
    private int partyMoney = 1000;
    public int PartyMoney { get { return partyMoney; } set { partyMoney = value; } }

    public static PartyManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (Characters c in members) 
        { 
            c.charInit(VFXManager.instance, UIManager.instance, InventoryManager.instance);
        }

        SelectSingleHero(0);

        members[0].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[0]));
        members[0].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[1]));
        members[1].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[1]));
        members[1].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[2]));

        InventoryManager.instance.AddItem(members[0], 0);
        InventoryManager.instance.AddItem(members[0], 1);
        InventoryManager.instance.AddItem(members[0], 3);
        InventoryManager.instance.AddItem(members[0], 4);
        InventoryManager.instance.AddItem(members[0], 6);

        InventoryManager.instance.AddItem(members[1], 0);
        InventoryManager.instance.AddItem(members[1], 1);
        InventoryManager.instance.AddItem(members[1], 2);
        InventoryManager.instance.AddItem(members[1], 5);

        UIManager.instance.ShowMagicToggle();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            selectChars[0].IsMagicMode = true;
            selectChars[0].CurMagicCast = selectChars[0].MagicSkills[0];
        }
    }

    public void SelectSingleHero(int i)
    {
        foreach (Characters c in selectChars)
        {
            c.ToggleRingSelection(false);
        }

        selectChars.Clear();

        selectChars.Add(members[i]);
        selectChars[0].ToggleRingSelection(true);
    }

    public void HeroSelectMagicSkill(int i)
    {
        if(selectChars.Count <= 0) return;

        selectChars[0].IsMagicMode = true;
        selectChars[0].CurMagicCast = selectChars[0].MagicSkills[i];
    }

    public int FindIndexFromClass(Characters hero)
    {
        for(int i = 0; i < members.Count; i++)
        {
            if (members[i] == hero)
                return i;
        }
        return 0;
    }

    public void SelectSingleHeroByToggle(int i)
    {
        if(selectChars.Contains(members[i]))
        {
            members[i].ToggleRingSelection(true);
            UIManager.instance.ShowMagicToggle();
        }
        else
        {
            selectChars.Add(members[i]);
            members[i].ToggleRingSelection(true);
            UIManager.instance.ShowMagicToggle();
        }
    }

    public void UnSelectSingleHeroToggle(int i)
    {
        /*if(selectChars.Count <= 1)
        {
            UIManager.instance.ToggleAvatar[i].isOn = true;
            return;
        }*/

        if(selectChars.Contains(members[i]))
        {
            selectChars.Remove(members[i]);
            members[i].ToggleRingSelection(false);
        }
    }

    public void RemoveHeroFromParty(int id)
    {
        if (id ==  -1 || id == 0)
            return;

        if(selectChars.Contains(members[id]))
            selectChars.Remove(members[id]);

        members.Remove(members[id]);
    }

}
