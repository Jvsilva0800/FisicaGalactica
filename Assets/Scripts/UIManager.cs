using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Display display;// Referência ao componente Display, que é um painel
    public PanelDeath panelDeath;//Referencia ao painel de morte

    [Header("PRIMEIRA FASE")]
    [SerializeField] public Tips tipsReferencialChallenge;
    [SerializeField] public SpeedChalangePanelUI speedChallenge;//Referencia ao componente Panel que fará o gerenciamento da resposta
    [SerializeField] public Tips tipsSpeedChallenge;//Referencia ao componente Panel das dicas do desafio de velocidade média
    [SerializeField] public BridgeChalenge bridgeChallenge;//Panel do subdesafio na ponte de variação de espaço
    [SerializeField] public Tips tipsBridgeChallenge;//Referencia ao componente Panel das dicas do subdesafio de variação de espaço
    [SerializeField] public TimeChallenge timeChallenge;//Panel do subdesafio da variação de tempo
    [SerializeField] public Tips tipsTimeChallenge;//Referencia ao componente Panel das dicas do subdesafio de variação de tempo

    [Header("SEGUNDA FASE")]
    public Tips tipsUniformVelocity;
    public Tips smallTip;
    public UniformVelocityChallenge uniformVelocityChallenge;
    public Tips tipsPositionTimeFunction;
    public ChallengePositionTimeFunction challengePositionTimeFunction;
    public MeetingPointChallenge meetingPointChallenge;
    public Tips tipsMeetingPoint;



    [Header("TERCEIRA FASE")]
    public AccelerationChalengePanel panel2;//Panel da terceira fase
    public Tips tipsAverageAcceleration;
    public AverageAcceleration averageAcceleration;
    public Tips tipsAverageAcceleration2;
    public Tips smallTipAverageAcceleration2;
    public AverageAcceleration2 averageAcceleration2;


    private void Awake()
    {

    }


}
