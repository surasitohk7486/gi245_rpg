using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] heroPrefabs;
    public GameObject[] HeroPrefabs { get { return heroPrefabs; }}

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GeneratePlayerHero();
    }

    private void GeneratePlayerHero()
    {
        int i = Setting.playerPrefabId;

        GameObject heroObj = Instantiate(heroPrefabs[i],
            new Vector3(46f,10f,38f), Quaternion.identity);

        heroObj.tag = "Player";

        Characters hero = heroObj.GetComponent<Characters>();
        PartyManager.instance.Members.Add(hero);
    }
}
