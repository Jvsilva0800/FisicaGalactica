using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private PlayerBehavior playerBH;
    private Rigidbody2D rb;
    public float force;
    public int attackDamage = 15;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerBH = player.GetComponent<PlayerBehavior>();

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 4)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            if (other.TryGetComponent(out HealthManager playerHealth))
            {
                playerBH.kBCount = playerBH.kBTime;
                if (other.transform.position.x <= transform.position.x)
                {
                    playerBH.isKnockRight = true;
                }
                if (other.transform.position.x > transform.position.x)
                {
                    playerBH.isKnockRight = false;
                }
                playerHealth.TakeDamage(attackDamage);
            }
            Destroy(gameObject);//Caso acerte o player ou o chao será destruido
        }
    }
}
