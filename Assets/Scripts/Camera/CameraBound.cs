using UnityEngine;
using Unity.Cinemachine;  // não esqueça de importar!

[RequireComponent(typeof(Collider2D))]
public class CameraZone : MonoBehaviour
{

    public CinemachineConfiner2D confiner;

    void Awake()
    {
        // Desativa logo de cara: sem limite até o player entrar na zona
        confiner.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Ativa o confiner e força ele a recalccular
        confiner.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        //Desativa ao sair da zona:
        if (confiner != null)//tem de ser diferente de null para não dar erro ao mudar de Scene
        {
            confiner.enabled = false;
        }

    }
}