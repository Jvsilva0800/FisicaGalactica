using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengePositionTimeFunction : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI instructionText;   // Texto de instrução
    [SerializeField] private TMP_InputField answerInput; // Campo onde o jogador digita
    [SerializeField] private TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”
    [SerializeField] private Button confirmButton;   // Botão para enviar

    [Header("Referência ao gameObjectCreator")]
    [SerializeField] private GameObjectCreator gameObjectCreator;

    [Header("Force Fild Object")]
    [SerializeField] private GameObject forceFild; // gameObject do campo de força do portão

    private float correctAnswer; // Armazena a resposta correcta
    private bool isPanelActive, rightAnwserClicked = false;//Verificação se o painel está ativo

    private void Awake()
    {
        // Desativa o painel no início (caso ainda esteja ativo)
        gameObject.SetActive(false);

        // Configura o botão
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }


    /// Exibe o painel com a resposta correta a ser adivinhada.
    public void ShowPanel(float answer)//essa answer será passada para o evento registrado no triger zone, pois o objeto não terá sido intanciado ainda
    {
        correctAnswer = answer;
        answerInput.text = "";
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;
    }


    /// Método chamado ao clicar no botão de confirmar
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
                gameObjectCreator.BuildGameObject();//Irá instanciar o bloco com o panel ativo ainda, o panel so será desabilitado quando o bloco for destruido
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


    /// Esconde o painel

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
