using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationChalengePanel : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI instructionText;   // Texto de instrução
    [SerializeField] private TMP_InputField answerInput; // Campo onde o jogador digita
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”
    [SerializeField] private Button confirmButton;   // Botão para enviar

    [Header("Configuração")]
    [SerializeField] private float marginOfError = 0.1f; // Margem de erro aceitável 10%

    private float correctAnswer; // Armazena aceleraçao médi correta
    private bool isPanelActive, rightAnwserClicked = false;//Verificação se o painel está ativo

    private void Awake()
    {
        // Desativa o painel no início (caso ainda esteja ativo)
        gameObject.SetActive(false);

        // Configura o botão
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    public void ShowPanelAverageAcceleration(float acceleration)//Configurações do panel que cálcula da aceleração média/ primeiro panel a aprecer
    {
        correctAnswer = acceleration;
        answerInput.text = "";
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;

        GameManager.Instance.InputManager.DisablePlayerInput();//Vai desabilitar a movimentação do player assim que o painel para resposta aparecer.
    }

    public void ShowPanelElapsedTime(float elapsedTime)//Configuração do panel que quer descobir do player o tempo necessário para alcançar uma determinada velocidade com base na aceleração média e a velocidade final
    {
        correctAnswer = elapsedTime;
        instructionText.text = "Qual o tempo necessário para alcançar essa velocidade ?";
        answerInput.text = "";
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;

        rightAnwserClicked = false;
    }

    public void ShowPanelFinalVelocity(float finalVelocity)//Configuração do panel que quer descobir do player o tempo necessário para alcançar uma determinada velocidade com base na aceleração média e a velocidade final
    {
        correctAnswer = finalVelocity;
        instructionText.text = "Qual a velocidade final desse objeto ?";
        answerInput.text = "";
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;

        rightAnwserClicked = false;
    }

    /// Método chamado ao clicar no botão de confirmar
    private void OnConfirmButtonClicked()
    {
        if (!isPanelActive) return;

        if (rightAnwserClicked) return;
        // Tenta converter o valor digitado em float
        if (float.TryParse(answerInput.text, out float playerAnswer))
        {
            // Verifica se a diferença está dentro da margem de erro
            float difference = Mathf.Abs(playerAnswer - correctAnswer);
            if (difference <= marginOfError)
            {
                rightAnwserClicked = true;

                // Acertou
                feedbackText.text = "Parabéns, você acertou!";
                HidePanel();
                GameManager.Instance.UiManager.display.DisableDisplay();
                GameManager.Instance.Phase2Manager.OnChallengeSucceeded();//Começa os sub-desafios
            }
            else
            {
                // Errou - permite tentar novamente
                feedbackText.text = $"Resposta incorreta. Tente novamente.";
            }
        }
        else
        {
            feedbackText.text = "Digite um valor numérico válido.";
        }
    }

    // Esconde o painel
    public void HidePanel()
    {
        StartCoroutine(HideAfter(1.5f));//espera 1,5 segundos antes de esconder o panel
    }
    private IEnumerator HideAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPanelActive = false;
        gameObject.SetActive(false);
    }
}
