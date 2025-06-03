using UnityEngine;
//O padrão Singleton é uma técnica de design que garante que uma classe tenha apenas uma instância e fornece um ponto de acesso global a ela.
//PAra a utilização do gamemanager ele deve estar associado a um gameobject na cena
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }
    public UIManager UiManager;//Acessar as funcionalidades do UiManager
    public LevelManager LevelManager;//Acessar as funcionalidade de transição dos leveis.
    public PhaseProgressionManager PhaseManager;
    public ReferencialManager ReferencialManager;
    public Phase2ProgressionManager Phase2Manager;
    public Phase3Progression Phase3Manager;
    public int cicle = 0;//Responsável por contar os cicles que servirão para a troca dos displays na fase 2
    public float attackTimerCounter;//Variável global que irá cronometrar o tempo para que o player não ataque várias vezes seguidas(tive que faze-lo aqui pois a foice começa desativada ao início do jogo)
    public Transform respawnPoint;//Define o local onde o player vai spawnar, no inicio do jogo recebe o transform do starting point

    private void Awake()//Ocorre antes do primeiro frame
    {
        if (Instance != null) Destroy(this.gameObject);//Verificando caso ja exista um valor no instance que não seja esse
        Instance = this;

        InputManager = new();

    }
    private void Update()
    {
        attackTimerCounter += Time.deltaTime;
    }

    public void Pausar()
    {
        Time.timeScale = 0f;//Pausa o jogo
    }
    public void Despausar()
    {
        Time.timeScale = 1f;//Tira o pause do jogo
    }
}
