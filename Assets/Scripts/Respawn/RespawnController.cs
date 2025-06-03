
using UnityEngine;

public class RespawnController : MonoBehaviour
{

    public int damage = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = GameManager.Instance.respawnPoint.position;
            collision.GetComponent<HealthManager>().TakeDamage(damage);//Caso o player passe pelo trigger irá tomar 5 de dano
        }
        if (collision.CompareTag("Enemy"))//Se o inimigo cair no colisor irá ser destruido
        {
            Destroy(collision.gameObject);
        }
    }
}
