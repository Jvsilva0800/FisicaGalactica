using UnityEngine;

public class BlockFour : Block
{//Bloco do desafio do ponto de encontro segunda fase, 
    private float BlockDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = 4;//Velocidade ideal escolhida
        //ChangeSpeed();
        GameManager.Instance.Phase3Manager.RegisterNewBlock(this);
    }

    // Update is called once per frame
    void Update()
    {
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);
    }

}
