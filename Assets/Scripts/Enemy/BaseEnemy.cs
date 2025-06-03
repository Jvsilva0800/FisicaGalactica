using UnityEngine;
[RequireComponent(typeof(Animator))]//Isso garante que sempre que você adicionar o script BaseEnemy a um GameObject, a Unity automaticamente adicionará um componente Animator a esse GameObject, se ele ainda não tiver um. Isso ajuda a evitar erros e economiza tempo, pois você não precisa lembrar de adicionar manualmente esses componentes necessários.
public abstract class BaseEnemy : MonoBehaviour//A palavra-chave abstract indica que essa classe não pode ser instanciada diretamente; ela serve como um "molde" para outras classes (inimigos específicos) herdarem e implementarem seus métodos abstratos.
{
    protected Animator animator;//Membros (variáveis ou métodos) declarados como protected são acessíveis dentro da própria classe e em qualquer classe que a herde. Esse modificador facilita o compartilhamento de dados e comportamentos comuns sem expô-los publicamente
    protected HealthManager enemyHealth;
    //protected AudioSource audioSource;
    protected Rigidbody2D rb;
    protected PlayerBehavior playerBH;


    protected virtual void Awake()//A palavra-chave virtual permite que as classes derivadas possam sobrescrever (override) esse método e adicionar ou modificar seu comportamento
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
        playerBH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        //audioSource = GetComponent<AudioSource>();

    }

    protected abstract void Update();//Essa abordagem obriga cada inimigo específico a definir sua própria lógica de atualização a cada frame, garantindo que o comportamento de cada inimigo possa ser personalizado
}