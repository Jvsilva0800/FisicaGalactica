using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    [SerializeField] private Transform checkerPosition;// Um Transform que define a posição central onde será realizada a verificação (por exemplo, a base do personagem).
    [SerializeField] private UnityEngine.Vector2 checkerSize;// Um Vector2 que define as dimensões (largura e altura) da área de verificação.
    [SerializeField] private LayerMask groundLayer;// Um LayerMask que especifica quais camadas são consideradas "chão" para o teste de colisão.

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(checkerPosition.position, checkerSize, 0f, groundLayer);//Physics2D.OverlapBox: Cria uma caixa (definida por checkerPosition.position e checkerSize) e verifica se há algum Collider2D que intersecte essa caixa, considerando apenas as camadas especificadas em groundLayer (no caso é a camada criada na unity "ground"). Se encontrar um Collider2D, o método retorna verdadeiro; caso contrário, retorna falso.
    }

    private void OnDrawGizmos()//Esse método é chamado pela Unity para desenhar gizmos na cena, o que ajuda a visualizar a área de verificação durante o desenvolvimento
    {
        if (checkerSize == null)//Embora um Vector2 não possa ser nulo, essa verificação é redundante ou pode ter sido colocada por segurança.
            return;
        if (IsGrounded())//Se o método IsGrounded() retorna verdadeiro, define a cor do gizmo como vermelho; caso contrário, define como verde. Isso permite identificar visualmente se o objeto está tocando o chão.
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(checkerPosition.position, checkerSize);//Desenha um cubo de arame (wireframe) na posição checkerPosition.position com o tamanho checkerSize. Esse cubo representa a área onde o OverlapBox está verificando colisões com o chão.
    }
}
