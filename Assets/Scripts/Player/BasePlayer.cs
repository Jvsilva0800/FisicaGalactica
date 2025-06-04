using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    protected Animator animator;
    protected GroundedChecker groundedChecker;
    protected HealthManager playerHealth;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {

        animator = GetComponent<Animator>();
        groundedChecker = GetComponent<GroundedChecker>();
        playerHealth = GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
    }
}
