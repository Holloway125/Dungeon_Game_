using UnityEngine;

public abstract class BaseEnemyState
{
public abstract void EnterState(EnemyStateManager Enemy);
public abstract void UpdateState(EnemyStateManager Enemy);
public abstract void OnCollisionEnter(EnemyStateManager Enemy, Collision collision);
}
