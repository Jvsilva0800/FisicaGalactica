using UnityEngine;

public class Blockone : MonoBehaviour
{
    System.Random random = new();
    private float Speed;
    private float BlockDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeSpeed();
    }

    public void ChangeSpeed()
    {
        Speed = random.Next(3, 6);//Altera novamente o valor da velocidade, valores de 3 a 6 inclusos
    }

    // Update is called once per frame
    void Update()
    {
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);
    }


}
