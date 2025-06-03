using UnityEngine;

public class Heart : MonoBehaviour, ICollectible
{
    [SerializeField] private int health = 20;
    private HealthManager playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }
    public void Collect()
    {

        if (playerHealth.currentHealth < playerHealth.maxHealth)//o objeto so será destruido caso o player não esteja com a vida cheia
        {
            Debug.Log("Vida coletada!");
            playerHealth.Heal(health);//irá curar o player ao pegar o coletável, o valor da cura depende do campo "health"
            Destroy(gameObject);
        }

    }


}
