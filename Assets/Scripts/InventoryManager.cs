using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject[] ItemPrefab {  get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData { get { return itemData; } set { itemData = value; } }

    private const int MAXSLOT = 16;

    public static InventoryManager instance;
    void Awake()
    {
        instance = this;
    }

    public bool AddItem(Characters character, int id)
    {
        Item item = new Item(itemData[id]);

        if(character.InventoryItems.Count < MAXSLOT)
        {
            character.InventoryItems.Add(item);
            return true;
        }
        else
        {
            Debug.Log("Inventory Full");
            return false;
        }
    }
}
