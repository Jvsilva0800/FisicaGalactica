using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChalangePanelUI : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI instructionText;   // Texto de instrução
    [SerializeField] private TMP_InputField answerInput; // Campo onde o jogador digita
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”
    [SerializeField] private Button confirmButton;   // Botão para enviar

    private float marginOfError = 0.1f; // Margem de erro aceitável 10%

    private float correctSpeed; // Armazena a velocidade correta
    private bool isPanelActive, rightAnwserClicked = false;//Verificação se o painel está ativo e se a resposta correta também ja foi feita

    private void Awake()
    {
        // Desativa o painel no início (caso ainda esteja ativo)
        gameObject.SetActive(false);

        // Configura o botão
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }


    /// Exibe o painel com a velocidade correta a ser adivinhada.
    public void ShowPanel(float speed)
    {
        correctSpeed = speed;
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
            float difference = Mathf.Abs(playerAnswer - correctSpeed);
            if (difference <= marginOfError)
            {
                rightAnwserClicked = true;
                // Acertou
                feedbackText.text = "Parabéns, você acertou!";
                // Pode esconder o painel ou esperar alguns segundos
                HidePanel();//Esconde o painel de perguntas
                GameManager.Instance.UiManager.display.DisableDisplay();//Desativar display ao final de cada resposta correta
                GameManager.Instance.PhaseManager.OnChallengeSucceeded();//Começa os sub-desafios
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

    /// <summary>
    /// Esconde o painel
    /// </summary>
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
