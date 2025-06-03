using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeVelocity : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayInformativo;//Caixa de texto responável por disponibilizar a distancia e o tempo do bloco antes depois da alteração de velocidade


    void Awake()
    {
        displayInformativo.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("changeVelocity"))
        {
            collision.gameObject.GetComponent<BlockTwo>().ChangeSpeed();//Atraves do collider estou ativando a função do bloco para alterar a sua velocidade
        }
        displayInformativo.gameObject.SetActive(true);
        displayInformativo.text = $"Chegou neste ponto: {collision.gameObject.GetComponent<Block3Tracker>().GetDistanceTravelled()} m, em {collision.gameObject.GetComponent<Block3Tracker>().GetTimeTravelled()} s";
    }

}
