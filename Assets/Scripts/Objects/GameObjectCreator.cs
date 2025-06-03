using System.Collections;
using UnityEngine;

public class GameObjectCreator : MonoBehaviour
{
    [SerializeField] private Transform checkerPosition;
    [SerializeField] private UnityEngine.Vector2 checkerSize;

    [Header("GameObject a ser criado")]
    [SerializeField] private GameObject gameObjectPrefab;


    private void OnDrawGizmos()
    {
        if (checkerSize == null)//Se existir dimenssões da caixa ela é desenhada
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(checkerPosition.position, checkerSize);//Desenha um cubo de arame (wireframe) na posição checkerPosition.position com o tamanho checkerSize. Esse cubo representa a área onde o OverlapBox está verificando se há blocos.
    }

    public void BuildGameObject()
    {
        StartCoroutine(InstantiateAfter(1f));//Depois de 1 segundo de delay irá instanciar o Bloco
    }
    private IEnumerator InstantiateAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0f);
        GameObject newBlock = Instantiate(gameObjectPrefab, spawnPos, Quaternion.identity);
        SpawnBlockEvent.RaiseOnSpawn(newBlock);//Pode ser usado pelo script que quiser a referencia do bloco instanciado, baste se inscrever no evento OnSpawn
    }
}
