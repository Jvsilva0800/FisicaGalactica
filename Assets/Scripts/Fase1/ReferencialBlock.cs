using UnityEngine;

public class ReferencialBlock : Block
{
    private float BlockDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = 5;//velocidade ideal escolhida
    }

    // Update is called once per frame
    void Update()
    {
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //troca de direção assim que encontrar um gameObject como trigger
        Speed *= -1;
    }

    //Gudando o player ao bloco em movimento. Tudo que está debaixo de um transform pai (a plataforma) vai se mover junto
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            // faz o Player “grudar” no bloco
            coll.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            // solta o Player do bloco
            coll.collider.transform.SetParent(null);
        }
    }
}
