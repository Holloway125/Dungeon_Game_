using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    BaseEnemyState currentState;
    public EnemyChasing ChasingState = new EnemyChasing();
    public EnemyDefault DefaultState = new EnemyDefault();
    public EnemySuspicious SuspiciousState = new EnemySuspicious();
    public EnemyAttacking AttackingState = new EnemyAttacking();
    public EnemyRetreating RetreatingState = new EnemyRetreating();
    // Start is called before the first frame update
    void Start()
    {
        currentState = DefaultState;

        currentState.EnterState(this);
    }
    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseEnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
