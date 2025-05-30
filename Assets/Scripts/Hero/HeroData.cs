using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Scriptable Objects/HeroData")]
public class HeroData : ScriptableObject
{
    public int prefabId;
    public int curHP;
    public List<int> magicIds = new List<int>();
    public int[] inventoryItemIds = new int[18];

    public int attackDamage;
    public int defensePower;

    public int exp;
    public int level;
    public int nextExp;

}
