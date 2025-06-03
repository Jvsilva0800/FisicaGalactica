using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool oneShot = false;//Responsável, se ativado, por permitir somente u8ma interação
    private bool alreadyEntered = false;
    private bool alreadyExited = false;

    public string collisionTag;//Verifica a tag especificada que irá ativar o evento
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyEntered)
            return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return;


        GameManager.Instance.InputManager.DisablePlayerInput();
        onTriggerEnter?.Invoke();

        if (oneShot)
            alreadyEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (alreadyExited)
            return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return;

        onTriggerExit?.Invoke();

        if (oneShot)
            alreadyExited = true;
    }
}
