using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

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

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
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
}
