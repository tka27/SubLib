using UnityEngine;

public struct PlayerRunState : IState
{
    public void Enter()
    {
        Player.Instance.Animator.SetBool(AnimationType.Run.ToString(), true);
    }

    public void Update()
    {
        const float defaultMoveSpeed = 5;
        Vector3 moveDirrection =
            defaultMoveSpeed * Player.Instance.MoveSpeedMultiplier * Player.Instance.Input.Direction;
        moveDirrection.y = Player.Instance.Rigidbody.velocity.y;
        Player.Instance.Rigidbody.velocity = moveDirrection;

        Vector3 velocity = new Vector3(Player.Instance.Rigidbody.velocity.x, 0, Player.Instance.Rigidbody.velocity.z);
        if (velocity.magnitude > 0)
        {
            Player.Instance.transform.rotation = Quaternion.LookRotation(velocity.normalized);
            // Quaternion.Lerp(Player.Instance.transform.rotation,
            // Quaternion.LookRotation(velocity.normalized), Time.deltaTime * 20);
        }

        if (!Player.Instance.Input.IsActive()) Player.Instance.Statable.SetState(UnitState.Idle);
    }

    public void Exit()
    {
        Player.Instance.Animator.SetBool(AnimationType.Run.ToString(), false);
    }
}