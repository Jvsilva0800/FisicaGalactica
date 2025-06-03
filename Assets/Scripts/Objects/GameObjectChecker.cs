using TMPro;
using UnityEngine;

public class GameObjectChecker : MonoBehaviour
{
    [SerializeField] private Transform checkerPosition;
    [SerializeField] private UnityEngine.Vector2 checkerSize;
    [SerializeField] private LayerMask layer;
    public TextMeshPro text;//Referencia ao texto do gameObjectchecker, pode ser usado ou não


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObjectDestruction();
    }

    public Collider2D ThereGameObject()
    {
        return Physics2D.OverlapBox(checkerPosition.position, checkerSize, 0f, layer);
    }
    private void OnDrawGizmos()
    {
        if (checkerSize == null)//Se existir dimenssões da caixa ela é desenhada
            return;
        if (ThereGameObject() != null)//Se o método ThereGameObject() retorna verdadeiro, define a cor do gizmo como vermelho; caso contrário, define como verde. Isso permite identificar visualmente se o objeto dentro da caixa de verificação.
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(checkerPosition.position, checkerSize);//Desenha um cubo de arame (wireframe) na posição checkerPosition.position com o tamanho checkerSize. Esse cubo representa a área onde o OverlapBox está verificando se há blocos.
    }

    private void GameObjectDestruction()
    {
        Collider2D gameObjectCollider = ThereGameObject();
        if (gameObjectCollider != null)
        {
            Destroy(gameObjectCollider.gameObject);
        }
    }
}
