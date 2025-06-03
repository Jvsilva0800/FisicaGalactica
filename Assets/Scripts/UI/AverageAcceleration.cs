using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AverageAcceleration : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI instructionText;   // Texto de instrução
    [SerializeField] private TMP_InputField answerInput; // Campo onde o jogador digita
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”
    [SerializeField] private Button confirmButton;   // Botão para enviar

    private float correctAnswer;
    private bool isPanelActive, rightAnwserClicked = false;//Verificação se o painel está ativo
    private void Awake()
    {
        // Desativa o painel no início (caso ainda esteja ativo)
        gameObject.SetActive(false);

        // Configura o botão
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    public void ShowPanel(float initialSpeed, float finalTime, float finalSpeed, float answer)
    {
        correctAnswer = answer;
        instructionText.text = $"Um bloco parte do repouso {initialSpeed} m/s e, após {finalTime} s, atinge {finalSpeed}m/s. Qual é a aceleração média desse bloco em m/s²?";
        answerInput.text = "";
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;
    }

    private void OnConfirmButtonClicked()
    {
        if (!isPanelActive) return;

        if (rightAnwserClicked) return;

        // Tenta converter o valor digitado em float
        if (float.TryParse(answerInput.text, out float playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                rightAnwserClicked = true;
                // Acertou
                feedbackText.text = "Parabéns, você acertou!";
                GameManager.Instance.InputManager.EnablePlayerInput();
                HidePanel();
                GameManager.Instance.UiManager.tipsAverageAcceleration.HidePanelTips();
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
