using UnityEngine;

public class ItemPick : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Item Item { get { return item; } }

    private InventoryManager inventoryManager;
    private PartyManager partyManager;

    public void Init(Item item, InventoryManager invManager, PartyManager ptyManager)
    {
        this.item = item;
        inventoryManager = invManager;
        partyManager = ptyManager;
    }

    private void PickUpItem(Characters hero)
    {
        if(inventoryManager.AddItem(hero,item.ID))
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Pick Up");
       
        if(partyManager.SelectChars.Count > 0)
        {
            PickUpItem(partyManager.SelectChars[0]);
        }
    }
}
