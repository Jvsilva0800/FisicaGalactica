using UnityEngine;

public class TrackSpeed : MonoBehaviour//O objetivo desse código é colocar o display que irá mostrar a velocidade do bloco na posição certa, quando o bloco tiver velocidade igual a 0
{
    [SerializeField] private TriggerDisplay2 finalTriggerDisplay2;
    private BlockSeven block;

    private void OnEnable() => SpawnBlockEvent.OnSpawn += HandleNewBlock;
    private void OnDisable() => SpawnBlockEvent.OnSpawn -= HandleNewBlock;

    void Update()
    {
        if (block != null)
        {
            if (block.GetSpeed() == 0f)
            {
                finalTriggerDisplay2.transform.position = new Vector3(block.transform.position.x, finalTriggerDisplay2.transform.position.y, 0);//altera somente a posição do x com a posição do bloco, o y se mantem a posição original
            }
        }
    }

    private void HandleNewBlock(GameObject obj)//obj é a referencia do obj recem instanciado
    {
        block = obj.GetComponent<BlockSeven>();
    }



}
