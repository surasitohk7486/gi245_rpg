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

    public static PartyManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (Characters c in members) 
        { 
            c.charInit(VFXManager.instance, UIManager.instance);
        }

        SelectSingleHero(0);

        members[0].MagicSkills.Add(new Magic(0, "Power Glow", 10f, 20, 3f, 1f, 0, 0));
        members[0].MagicSkills.Add(new Magic(1, "Fire Ball", 10f, 35, 3f, 4f, 1, 1));
        members[1].MagicSkills.Add(new Magic(1, "Fire Ball", 10f, 35, 3f, 4f, 1, 1));
        members[1].MagicSkills.Add(new Magic(2, "Rainbow Glow", 10f, 50, 3f, 1f, 2, 2));

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
}
