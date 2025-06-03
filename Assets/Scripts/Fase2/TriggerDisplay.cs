using TMPro;
using UnityEngine;

public class TriggerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro infoText;//Caixa de texto responável por disponibilizar a distancia e o tempo do bloco


    void Awake()
    {
        infoText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        infoText.gameObject.SetActive(true);
        infoText.text = $"Posição Final: {collision.gameObject.GetComponent<Block4Tracker>().GetDistanceTravelled()} m Tempo Final: {collision.gameObject.GetComponent<Block4Tracker>().GetTimeTravelled()} s";
    }

}
