using UnityEngine;

public class Enemy : Characters
{
    [SerializeField]
    private int expDrop;
    public int ExpDrop { get { return expDrop; } }

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
        }
    }

    protected override void Die()
    {
        base.Die();
        partyManager.DistributeTotalExp(expDrop);
    }
}
