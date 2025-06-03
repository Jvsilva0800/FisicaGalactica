using UnityEngine;

public class BlockThree : MonoBehaviour
{
    [SerializeField] private float Speed;//Editavel para que consiga encontrar a velocidad média certa
    private float BlockDirection;

    void Update()
    {
        BlockDirection = 1 * Time.deltaTime * Speed;
        transform.Translate(BlockDirection, 0, 0);
    }
}
