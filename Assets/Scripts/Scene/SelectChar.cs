using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectChar : MonoBehaviour
{
    [SerializeField] private Image charImage;
    [SerializeField] private TMP_Text charNameText;

    [Header("Stat")]
    [SerializeField] private TMP_Text strText;
    [SerializeField] private TMP_Text dexText;
    [SerializeField] private TMP_Text conText;
    [SerializeField] private TMP_Text intText;
    [SerializeField] private TMP_Text wisText;
    [SerializeField] private TMP_Text chaText;

    [SerializeField]
    private GameObject[] heroPrefabs;

    [SerializeField]
    private int curId = 0;

    private void Start()
    {
        LoadChar();
    }

    private void LoadChar()
    {
        Hero hero = heroPrefabs[curId].GetComponent<Hero>();
        charImage.sprite = hero.AvatarPic;
        charNameText.text = hero.CharName;

        strText.text = hero.Strength.ToString();
        dexText.text = hero.Dexterity.ToString();
        conText.text = hero.Constitution.ToString();
        intText.text = hero.Intelligence.ToString();
        wisText.text = hero.Wisdom.ToString();
        chaText.text = hero.Charisma.ToString();
    }

    public void SelectNextChar()
    {
        curId++;

        if (curId >= heroPrefabs.Length)
            curId = 0;

        LoadChar();
    }

    public void SelectPreviousChar()
    {
        curId--;

        if(curId < 0)
            curId = heroPrefabs.Length - 1;

        LoadChar();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BeginGame()
    {
        Setting.playerPrefabId = curId;
        SceneManager.LoadScene("VillageScene");
    }
}
