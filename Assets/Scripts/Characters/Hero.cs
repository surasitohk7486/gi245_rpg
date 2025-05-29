using UnityEngine;

public class Hero : Characters
{
    [SerializeField] 
    private int exp;
    public int Exp { get { return exp; } set { exp = value; } }

    [SerializeField]
    private int level;
    public int Level { get { return level; } set { level = value; } }

    [SerializeField]
    private int strength;
    public int Strength { get { return strength; } set { strength = value; } }

    [SerializeField]
    private int dexterity;
    public int Dexterity { get { return dexterity; } set { dexterity = value; } }

    [SerializeField]
    private int constitution;
    public int Constitution { get { return constitution; } set { constitution = value; } }

    [SerializeField]
    private int intelligence;
    public int Intelligence { get { return intelligence; } set { intelligence = value; } }

    [SerializeField]
    private int wisdom;
    public int Wisdom { get { return wisdom; } set { wisdom = value; } }
    
    [SerializeField]
    private int charisma;
    public int Charisma { get { return charisma; } set { charisma = value; } }


    private void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
            case CharState.WalkToEnemy:
                WalkToEnemyUpdate();
                break;
            case CharState.Attack:
                AttackUpdate();
                break;
            case CharState.WalkToMagicCast:
                WalkToMagicCastUpdate();
                break;
            case CharState.WalkToNPC:
                WalkToNPCUpdate();
                break;
        }
    }

    protected void WalkToNPCUpdate()
    {
        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);

        if (distance <= 2f)
        {
            navAgent.isStopped = true;
            SetState(CharState.Idle);

            Npc npc = curCharTarget.GetComponent<Npc>();

            if (npc.IsShopKeeper)
                uiManager.PrepareShopPanel(npc, this);
            else
                uiManager.PrepareDialogueBox(npc);
        }
    }

    public void SaveItemInInventory(Item item)
    {
        for(int i = 0; i < 16; i++)
        {
            if (InventoryItems[i] == null)
            {
                InventoryItems[i] = item;
                return;
            }
        }
    }
}
