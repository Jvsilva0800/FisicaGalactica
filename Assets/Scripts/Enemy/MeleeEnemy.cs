using System.Collections;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public ParticleSystem hurtParticle;
    [SerializeField] private Transform detectPosition;//Um Transform que define a posição central da área de detecção para o inimigo. Geralmente, essa posição é definida no Inspector e pode representar a "vista" ou alcance de detecção.
    [SerializeField] private Vector2 detectBoxSize;//Um Vector2 que define o tamanho (largura e altura) da caixa usada para verificar se o player está próximo
    [SerializeField] private LayerMask playerLayer;//Um LayerMask que indica qual camada (layer) é considerada como o jogador. Isso restringe a detecção somente a colisores pertencentes a essa camada. No caso o Player
    [SerializeField] private float attackCooldown;

    [Header("Knockback")]//Configurações para o knockback do inimigo
    public float kBForce;
    public float kBCount;
    public float kBTime;//sempre é 0
    public bool isKnockRight;

    public int attackDamage = 10;
    private float cooldownTimer;

    protected override void Awake()
    {
        base.Awake();// Chamando o método base.Awake() para garantir que o inimigo seja inicializado
        enemyHealth.OnDead += EnemyDeathAnimAndDestroy;
        enemyHealth.OnHurt += EnemyHurtAnim;

    }

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime;//Inicia a contagem e a incrementa a cada segundo de frame do jogo/ faz a contagem em segundos
        VerifyCanAttack();
    }

    private void FixedUpdate()
    {
        KnockLogic();
    }

    void KnockLogic()
    {
        if (kBCount < 0)//Se o contador de knockback (kBCount) já acabou (ou nunca foi iniciado), o jogador se move normalmente, chamando MovePlayer(). Nesse caso ele é decrementado infinitamente 
        {
            //Não faz nd
        }
        else//Enquanto kBCount for ≥ 0, o player está em “stun” e em vez de andar, recebe um impulso.
        {
            if (isKnockRight == true)//Se o flag indica que o golpe veio da direita, aplica uma velocidade apontando para a esquerda (-kBForce) e um componente vertical (+kBForce).
            {
                rb.linearVelocity = new Vector2(-kBForce, rb.linearVelocityY);//Esse "rb.linearVelocityY" serve para que o inimigo não mova na vertical quando acertado, somente na horizontal
            }
            if (isKnockRight == false)//Caso contrário, empurra para direita (+kBForce).
            {
                rb.linearVelocity = new Vector2(kBForce, rb.linearVelocityY);
            }
        }
        kBCount -= Time.deltaTime;//A cada frame fixa, diminui o tempo infinitamente do knockback. Quando ele chega a zero, recebe o Knockback. 
    }

    private void VerifyCanAttack()
    {
        // audioSource.clip = audioClips[0];
        if (cooldownTimer < attackCooldown)//Esse attackColdown poderá ser modificado para especificar o tempo de coldown to attack, para que o personagem n receba hits infinitos sequencialmente
            return;

        if (PlayerInSight())
        {
            //animator.SetTrigger("attack");
            AttackPlayer();
        }

    }

    private void AttackPlayer()
    {
        // audioSource.clip = audioClips[0];
        cooldownTimer = 0;//zera a contagem do cooldown timer para poder atacar novamente
        if (CheckPlayerInDetectArea().TryGetComponent(out HealthManager playerHealth))//se o player estiver na área pega o component Health dele para realizar o dano
        {
            playerBH.kBCount = playerBH.kBTime;//kbCount do player recebe o valor de kbTime que é de zero, quando acertado, ou seja, irá gerar um knockback
            //isKnockRight é definido comparando as posições X de player e inimigo, para saber de que lado veio o golpe.
            if (CheckPlayerInDetectArea().transform.position.x <= transform.position.x)
            {
                playerBH.isKnockRight = true;
            }
            if (CheckPlayerInDetectArea().transform.position.x > transform.position.x)
            {
                playerBH.isKnockRight = false;
            }

            print("Making player take damage");
            playerHealth.TakeDamage(attackDamage);
        }
    }



    private Collider2D CheckPlayerInDetectArea()//Verifica se o player está na área, retorna um collider 2d do player
    {
        /*
       Physics2D.OverlapBox:

       .Cria uma caixa de detecção na posição definida por detectPosition.position com o tamanho detectBoxSize.
       .O parâmetro 0f indica que a caixa não é rotacionada.
       .O playerLayer garante que apenas colisores que pertençam à camada do jogador sejam considerados.*/
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private bool PlayerInSight()//Verifica se o player esta no range, retorna um booleano
    {

        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;//Se houver algum colisor dentro da caixa, a variável playerCollider não será nula, e o método retorna true; caso contrário, retorna false, isso também faz com que quando o player morrer ele não realize a ação de attack pois o colider é desativado
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;//Primeiro, verifica se detectPosition está definido; se não estiver, o método retorna sem fazer nada, no nosso caso foi criado um game object vazio para o Enemy e logo em seguida anexado manualmente nocampo "Detect Position" do script
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }

    private void EnemyHurtAnim()
    {
        hurtParticle.Play();//Quando o player recebe um hit saem particulas do corpo
    }
    private void EnemyDeathAnimAndDestroy()
    {
        animator.SetTrigger("IsDead");
        StartCoroutine(DestroyAfterAnimation(0.30f));//No momento em que você dispara a animação de morte (por exemplo, no callback do evento OnDead), inicie uma coroutine que aguarde o tempo da animação e, em seguida, destrua o GameObject.
    }

    private IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);//Destroi o gameobject ao qual esse script esteja associado na unity
    }
}
