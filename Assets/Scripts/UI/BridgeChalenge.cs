using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BridgeChalenge : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI instructionText;   // Texto de instrução
    [SerializeField] private TMP_InputField answerInput; // Campo onde o jogador digita
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”
    [SerializeField] private Button confirmButton;   // Botão para enviar

    [Header("Referência para o Tile da ponte")]
    [SerializeField] private TilePonte ponteTile;//Para que ao final do subdesafio se o player acertar a ponte será criada

    [Header("Referencia para valores das placas")]
    [SerializeField] private TextMeshPro[] textPlacas; //Para que ao final do subdesafio

    private float correcAnswer = 11f;
    private bool isPanelActive, rightAnwserClicked = false;

    private void Awake()
    {
        // Desativa o painel no início (caso ainda esteja ativo)
        gameObject.SetActive(false);
        HideTextPlacas();

        // Configura o botão
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    public void ShowPanel()
    {
        answerInput.text = "";
        feedbackText.text = "";
        gameObject.SetActive(true);
        ShowTextPlacas();
        isPanelActive = true;
    }

    private void OnConfirmButtonClicked()
    {
        if (!isPanelActive) return;

        if (rightAnwserClicked) return;

        // Tenta converter o valor digitado em float
        if (float.TryParse(answerInput.text, out float playerAnswer))
        {
            if (correcAnswer == playerAnswer)
            {
                rightAnwserClicked = true;
                // Acertou
                feedbackText.text = "Parabéns, você acertou!";
                // Pode esconder o painel ou esperar alguns segundos
                ponteTile.ActivateBridge();//Ponte é criada
                GameManager.Instance.InputManager.EnablePlayerInput();
                GameManager.Instance.UiManager.tipsBridgeChallenge.HidePanelTips();
                HidePanel();
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

    private void HideTextPlacas()
    {
        foreach (TextMeshPro textPlaca in textPlacas)
        {
            textPlaca.gameObject.SetActive(false);
        }
    }

    private void ShowTextPlacas()
    {
        foreach (TextMeshPro textPlaca in textPlacas)
        {
            textPlaca.gameObject.SetActive(true);
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
