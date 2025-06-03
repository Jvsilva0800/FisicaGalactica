using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Phase3Progression : MonoBehaviour
{
    private List<Block> spawnedBlocks = new();//Lista que irá armazenar os 2 blocos instanciados na esteira.
    [SerializeField] private GameObject firstCreator;//Referencia ao gameObject do GameObjectCreator que está instanciando o bloco da esquerda.
    [SerializeField] private GameObject secondCreator;//Referencia ao gameObject do GameObjectCreator que está instanciando o bloco da direita.
    [SerializeField] private GameObjectChecker gameObejectChecker;//Referencia ao gameObejectChecker que irá 
    float v1, v2, x10, x20, tEncontro, xEncontro;

    void Start()
    {
        gameObejectChecker.gameObject.SetActive(false);
    }

    public void RegisterNewBlock(Block block)
    {
        spawnedBlocks.Add(block);//Adiciona os blocos na lista para verificação da velocidade de cada
        if (spawnedBlocks.Count == 2)//Quando os dois são adicionados na lista chama afunção de ponto de encontro
        {
            MeetingPoint();
        }
    }

    //função responsável por fazer o cálculo que será usado para validação do ponto de encontro e tempo de encontro entre os dois blocos
    public void MeetingPoint()
    {

        v1 = spawnedBlocks[0].GetComponent<Block>().GetSpeed();
        v2 = spawnedBlocks[1].GetComponent<Block>().GetSpeed();

        //Existe o problema que esta sendo compensado ao exibir os valores ao player, pois a posição do transform.x de cada creator é em relação ao transform geral da cena, que é o centro da cena, por isso o valor de x está negativo pois a esteira está a esquerda do centro da Cena
        x10 = firstCreator.transform.position.x;//Ao perguntar ao player no painel informar que a posição é 0m
        x20 = secondCreator.transform.position.x;//Ao perguntar ao player no painel informar que a posição é 26m

        tEncontro = (x20 - x10) / (v1 - v2);//cálculo do tempo de encontro
        xEncontro = x10 + v1 * tEncontro;//cálculo da posição de encontro 

        StartCoroutine(PausarPainel(1f));//Pausa o jogo 1 segundo após essa função ser chamada para dar mais dinâmica na resposta do desafio

        // Debug.Log($"Velocidade bloco 1: {v1} ms\n Posição inicial bloco 1: {x10} m");
        // Debug.Log($"Velocidade bloco 2: {v2} ms\n Posição inicial bloco 2: {x20} m");

        // Debug.Log($"Tempo de encontro: {tEncontro} s\nPosição de encontro: {xEncontro} m");

    }

    private System.Collections.IEnumerator PausarPainel(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.UiManager.meetingPointChallenge.ShowPanel();
        GameManager.Instance.UiManager.tipsMeetingPoint.ShowPanelTips();
        GameManager.Instance.Pausar();
    }
    public void ExibirPontoEncontro()
    {
        gameObejectChecker.text.text = $"Ponto de Encotro: {(xEncontro - x10)} m \nTempo de Encontro: {tEncontro}s";//(xEncontro - x10) é o calculo compensado.
        gameObejectChecker.transform.position = new Vector3(xEncontro, firstCreator.transform.position.y, 0f);//Faz com que a posição do gameObejectChecker seja alterada para o local do enconto. A posição y poderia ser qualquer um dos dois creators presos na esteira
        gameObejectChecker.gameObject.SetActive(true);
    }
}
