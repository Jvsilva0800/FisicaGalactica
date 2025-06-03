using System.Collections;
using System.Data;
using UnityEngine;
public class FallingPlataform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float resetDelay = 2f;

    private bool falling = false;

    [SerializeField] private Rigidbody2D rb;

    // guarda o estado original
    private Vector3 startPos;
    private Quaternion startRot;
    private RigidbodyType2D startBodyType;
    private void Awake()
    {
        // registra onde a plataforma “mora” no início
        startPos = transform.position;
        startRot = transform.rotation;
        startBodyType = rb.bodyType;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Avoid calling the coroutine multiple times if it's already been called (falling)
        if (falling)
            return;

        // If the player landed on the platform, start falling
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(StartFall());
        }
    }

    private IEnumerator StartFall()
    {
        falling = true;

        // Wait for a few seconds before dropping
        yield return new WaitForSeconds(fallDelay);

        // Enable rigidbody and destroy after a few seconds
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(resetDelay);

        // faz o “reset”
        rb.bodyType = startBodyType;            // volta a ser kinematic
        rb.linearVelocity = Vector2.zero;             // zera qualquer velocidade
        transform.position = startPos;                 // reposiciona na origem
        transform.rotation = startRot;                 // (se precisar)
        falling = false;
    }

}
