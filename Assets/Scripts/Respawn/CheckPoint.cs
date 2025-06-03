using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public BoxCollider2D trigger;//Referencia ao boxcollider do checkpoint

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.respawnPoint = transform;//muda o ponto de spawn para a posição em que o chepoint estiver
            trigger.enabled = false;//Garante que o player não pege um check point ja pego
        }
    }
}
