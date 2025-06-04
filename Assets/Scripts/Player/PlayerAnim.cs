using UnityEngine;

public class PlayerAnim : BasePlayer
{
    [Header("Refeências para que o player ataque")]
    [SerializeField] private Animator animatorMeleeWeapon;
    [SerializeField] private Scythe meleeWeapon;
    [SerializeField] private float timeBtwAttacks = 0.15f;


    protected override void Awake()
    {
        base.Awake();
        playerHealth.OnHurt += PlayHurtAnim;
        playerHealth.OnDead += PlayDeadAnim;
        animatorMeleeWeapon = meleeWeapon.GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnAttack += PlayAttackAnim;
    }

    private void Update()
    {

        // attackTimerCounter += Time.deltaTime;//Atualiza o timer de attack a todo frame do jogo
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;//Como esta dentro do método Update, faz a verificação a todo momento se o player está se movendo, se sim a variável recebe o valor true, se não false.
        bool isJumping = !groundedChecker.IsGrounded();//Caso ele não esteja no chão o booleano recebe True

        animator.SetBool("IsMoving", isMoving);//Apos a intrução de cima é setado no parametro escolhido, "isMoving", o valor booleano descoberto fazendo assim a transição de animações ja configuradas 

        animator.SetBool("IsJumping", isJumping);

    }

    //Animações da foice e do player mexendo o braço
    private void PlayAttackAnim()
    {
        if (GameManager.Instance.attackTimerCounter > timeBtwAttacks)
        {
            meleeWeapon.EnableMeleeWeapon();//A arma aparece 

            SoundManager.Instance.PlaySound3D("Scythe", transform.position);
            animatorMeleeWeapon.SetTrigger("scythe");
            animator.SetTrigger("Attack");

            GameManager.Instance.attackTimerCounter = 0;//Apos a animação ser executada o timer é resetado
        }

    }

    private void PlayHurtAnim()
    {
        animator.SetTrigger("Hurt");
        SoundManager.Instance.PlaySound3D("Hurt Player", transform.position);
    }
    private void PlayDeadAnim()
    {
        animator.SetBool("IsDead", true);
        SoundManager.Instance.PlaySound3D("Player Death", transform.position);
    }


    private void OnDisable()
    {
        GameManager.Instance.InputManager.OnAttack -= PlayAttackAnim;
        playerHealth.OnHurt -= PlayHurtAnim;
        playerHealth.OnDead -= PlayDeadAnim;

    }

}
