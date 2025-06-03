using UnityEngine;

public class BlockSix : Block
{
    private float BlockDirection;
    private float initialTime, timeAlive;//tempo de criação do bloco com base no tempo do jogo

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = 0;
        initialTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive = (int)(Time.time - initialTime);//forço o valor em inteiro
        if (timeAlive == 1)//quando o tempo de vida do bloco bater 1 sec o tempo inicial será resetado e a velocidade alterada
        {
            initialTime = Time.time;
            Speed += 2f;
        }
        //movimentação do bloco.+
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);

    }
}
