using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : BasePlayer
{
    public ParticleSystem dust;//Referencia as particulas de poeira no pé do player
    public int attackDamage = 25;
    private float moveDirection;
    [SerializeField] private float MoveSpeed = 5;
    [SerializeField] private float JumpForce = 5;
    [SerializeField] private float coyoteTime = 0.05f;
    private float lastGroundedTime = -1f;

    [Header("Knockback")]//Configurações para o knockback do player
    public float kBForce;
    public float kBCount;
    public float kBTime;//sempre é 0
    public bool isKnockRight;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;




    protected override void Awake()
    {
        base.Awake();
        playerHealth.OnDead += PlayerDeathDestroy;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;//ele se inscreve no evento OnJump (definido no InputManager) associando o método HandleJump. Assim, quando a ação de pulo for realizada, o método HandleJump será chamado automaticamente.
    }
    // Update is called once per frame
    void Update()
    {
        FlipSpriteAccordingMoveDirection();
        // toda vez que estiver no chão, registramos o Time.time
        if (groundedChecker.IsGrounded())
            lastGroundedTime = Time.time;

    }
    private void FixedUpdate()//FixedUpdate é chamado em intervalos regulares pelo sistema de física do Unity. Colocar o knockback aqui garante que a alteração de velocidade do Rigidbody2D seja estável .
    {
        KnockLogic();
    }
    void KnockLogic()
    {
        if (kBCount < 0)//Se o contador de knockback (kBCount) já acabou (ou nunca foi iniciado), o jogador se move normalmente, chamando MovePlayer(). Nesse caso ele é decrementado infinitamente 
        {
            MovePlayer();
        }
        else//Enquanto kBCount for ≥ 0, o player está em “stun” e em vez de andar, recebe um impulso.
        {
            if (isKnockRight == true)//Se o flag indica que o golpe veio da direita, aplica uma velocidade apontando para a esquerda (-kBForce) e um componente vertical (+kBForce).
            {
                rb.linearVelocity = new Vector2(-kBForce, kBForce);
            }
            if (isKnockRight == false)//Caso contrário, empurra para direita (+kBForce).
            {
                rb.linearVelocity = new Vector2(kBForce, kBForce);
            }
        }
        kBCount -= Time.deltaTime;//A cada frame fixa, diminui o tempo infinitamente do knockback. Quando ele chega a zero, recebe o Knockback. 
    }

    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.InputManager.Movement * Time.deltaTime * MoveSpeed;
        transform.Translate(moveDirection, 0, 0);
    }

    private void FlipSpriteAccordingMoveDirection()
    {
        if (moveDirection > 0)//Faz com que o personagem ao mover para a direita fique virado para a direita e se estiver andando para a esquerda ele vire para a esquerda, essa mudança é realizada no transform do player, Scale
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (groundedChecker.IsGrounded())
            {//somente exibe a animação de poeira quando o player está no chao
                dust.Play();
            }
        }
        else if (moveDirection < 0)
        {
            transform.localScale = Vector3.one;//ou Vector3(1, 1, 1)
            if (groundedChecker.IsGrounded())
            {
                dust.Play();
            }
        }
    }

    private void HandleJump()//Este método é chamado sempre que o evento OnJump é disparado (ou seja, quando o input de pulo é realizado).
    {
        // permite pular se:
        //  - ainda estiver no chão, ou
        //  - saiu do chão há menos de coyoteTime segundos
        if (Time.time - lastGroundedTime > coyoteTime)
            return;
        dust.Play();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        // rb.linearVelocity += Vector2.up * JumpForce;//o método agora modifica a propriedade linearVelocity do Rigidbody2D; Vector2.up * JumpForce: Cria um vetor apontando para cima (eixo Y) multiplicado pela força definida em JumpForce; rb.linearVelocity += ...: Adiciona esse vetor à velocidade linear atual, fazendo com que o player seja impulsionado para cima.
    }

    private void Attack()//Será chamada no frame de attack do player
    {
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);//pega um array de colliders, pois o atack pode ser pego em mais de um inimigo, o Overlap é em círculo
        print("Making enemy take damage");
        print(hittedEnemies.Length);

        foreach (Collider2D hittedEnemy in hittedEnemies)//faz a verificação em cada collider acertado e chama o método de takedamage em cada um dos componentes Health dos inimigos, fazendo com que a devida animação seja realizada 
        {
            //Logica do knockback do inimigo e do dano
            MeleeEnemy enemy = hittedEnemy.GetComponent<MeleeEnemy>();

            print("Checking enemy");
            if (hittedEnemy.TryGetComponent(out HealthManager enemyHealth))
            {
                enemy.kBCount = enemy.kBTime;
                if (enemy.transform.position.x <= transform.position.x)
                {
                    enemy.isKnockRight = true;
                }
                if (enemy.transform.position.x > transform.position.x)
                {
                    enemy.isKnockRight = false;
                }
                print("Getting damage");
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void PlayerDeathDestroy()
    {
        GetComponent<Collider2D>().enabled = false;//obtém o componente Collider2D do GameObject (responsável por detectar colisões) e o desativa, impedindo que o player interaja fisicamente com outros objetos
        rb.constraints = RigidbodyConstraints2D.FreezeAll;//Define as restrições do Rigidbody2D para FreezeAll, travando completamente o movimento e a rotação do player. Assim, o personagem não se moverá mais, nem reagirá a forças físicas
        GameManager.Instance.InputManager.DisablePlayerInput();
        StartCoroutine(DestroyAfterAnimation(1f));//No momento em que o player morre (por exemplo, no callback do evento OnDead), inicie uma coroutine que aguarde o tempo da animação e, em seguida, destrua o GameObject.
    }

    private IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);//Destroi o gameobject ao qual esse script esteja associado na unity

        GameManager.Instance.UiManager.panelDeath.ShowPanel();//Exibe o painel de morte para que o player descida se quer recomeçar ou voltar ao menu
        GameManager.Instance.Pausar();
    }

    private void OnDrawGizmos()//Área responsável pelo ataque
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnJump -= HandleJump;
        playerHealth.OnDead -= PlayerDeathDestroy;
    }
}
