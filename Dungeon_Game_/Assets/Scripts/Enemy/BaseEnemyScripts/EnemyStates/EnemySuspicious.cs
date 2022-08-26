using UnityEngine;

public class EnemySuspicious : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform;
        Debug.Log("I am Suspicious");
    }

public override void UpdateState(BaseEnemy Enemy)
    {

    }

}
