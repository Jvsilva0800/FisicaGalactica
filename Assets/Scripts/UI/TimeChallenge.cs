using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeChallenge : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI instructionText;   // Texto de instrução
    [SerializeField] private TMP_InputField answerInput; // Campo onde o jogador digita
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”
    [SerializeField] private Button confirmButton;   // Botão para enviar

    [Header("Force Fild Object")]
    [SerializeField] private GameObject forceFild; // gameObject do campo de força do portão

    private float correctAnswer = 0;
    private bool isPanelActive, rightAnwserClicked = false;

    private void Awake()
    {
        // Desativa o painel no início (caso ainda esteja ativo)
        gameObject.SetActive(false);
        // Configura o botão
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    public void ShowPanel(float correctAnswer)
    {
        this.correctAnswer = correctAnswer;
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
            // Verifica se a diferença está dentro da margem de erro
            float difference = playerAnswer - correctAnswer;
            if (difference == 0)
            {
                rightAnwserClicked = true;
                // Acertou
                feedbackText.text = "Parabéns, você acertou!";
                GameManager.Instance.InputManager.EnablePlayerInput();
                GameManager.Instance.UiManager.display.DisableDisplay();
                GameManager.Instance.UiManager.tipsTimeChallenge.HidePanelTips();
                HidePanel();
                forceFild.SetActive(false);
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
