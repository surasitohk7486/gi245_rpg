using UnityEngine;

[System.Serializable]
public class Magic
{
    [SerializeField]
    private int id;
    public int ID { get { return id; } }

    [SerializeField]
    private string name;
    public string Name { get { return name; } }

    [SerializeField]
    private float range;
    public float Range { get { return range; } }

    [SerializeField]
    private int power;
    public int Power { get { return power; } }

    [SerializeField]
    private float loadTime;
    public float LoadTime { get { return loadTime; } }

    [SerializeField]
    private float shootTime;
    public float ShootTime { get {return shootTime; } }

    [SerializeField]
    private int loadID;
    public int LoadID { get { return loadID; } }

    [SerializeField]
    private int shootID;
    public int ShootID { get {return shootID; } }

    public Magic(int id, string name, float range, int power, float loadTime, float shootTime, int loadID, int shootID)
    {
        this.id = id;
        this.name = name;
        this.range = range;
        this.power = power;
        this.loadTime = loadTime;
        this.shootTime = shootTime;
        this.loadID = loadID;
        this.shootID = shootID;
    }
}
