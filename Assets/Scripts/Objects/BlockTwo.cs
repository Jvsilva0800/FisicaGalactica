using UnityEngine;

public class BlockTwo : MonoBehaviour
{
    //Criação desssa classe é importante para a demonstração na fase 2 sobre velocidade constante, onde o bloco instanciado sem a alteração da velocidade terá sempre 4 de velocidade
    private float Speed = 4f;//Esse valor foi escolhudo pois o valor da velocidade e do tempo decorrido na esteira2 sempre bate com o de um objeto em velocidade constante.
    private float BlockDirection;

    public void ChangeSpeed()
    {
        Speed = Random.Range(5f, 11f);//Altera novamente o valor da velocidade, valores de 5 a 11 inclusos, esse valor acima de 4 é para a segunda fase ,para que não se repita o 4 e caia em velocidade constante
    }

    // Update is called once per frame
    void Update()
    {
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);
    }
}
