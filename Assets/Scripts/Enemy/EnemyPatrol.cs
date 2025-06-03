using System.Numerics;
using UnityEngine;

public class EnemyPatrol : BaseEnemy
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        currentPoint = pointB.transform;
        animator.SetBool("IsRunning", true);
    }
    protected override void Update()
    {
        UnityEngine.Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new UnityEngine.Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new UnityEngine.Vector2(-speed, 0);
        }

        if (UnityEngine.Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (UnityEngine.Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    private void flip()
    {
        UnityEngine.Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);

        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

    }

}
