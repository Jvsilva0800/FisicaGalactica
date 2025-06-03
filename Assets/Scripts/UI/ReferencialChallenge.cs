
using UnityEngine;
using UnityEngine.UI;

public class ReferencialChallenge : MonoBehaviour
{
    private string corretAnswer;
    private int aux = 1;//resposnável por coordenar qual painel irá aparecer e quando será ativado novamente os inputs do player
    [Header("Referências às UI Buttons")]
    public Button buttonA;
    public Button buttonB;

    [Header("Referências de UI")]
    [SerializeField] private TMPro.TextMeshProUGUI QuestText;
    [SerializeField] private TMPro.TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”

    private bool isPanelActive;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        // adiciona listeners programaticamente
        buttonA.onClick.AddListener(() => HandleAnswer("A"));
        buttonB.onClick.AddListener(() => HandleAnswer("B"));

    }


    public void ShowReferencialPanel1()
    {
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;
        corretAnswer = "A";
    }

    public void ShowReferencialPanel2()
    {
        QuestText.text = "Agora tomando como referencial o bloco ao qual o player está em cima, pode se considerar que o player está em movimento ?";
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;
        corretAnswer = "B";
    }

    public void HidePanel()
    {
        isPanelActive = false;
        gameObject.SetActive(false);
    }

    private void HandleAnswer(string answer)
    {
        if (!isPanelActive) return;
        if (answer == corretAnswer)
        {
            feedbackText.text = "Parabéns, você acertou!";
            HidePanel();
            if (aux == 1)
            {
                GameManager.Instance.ReferencialManager.PlayerToBlock();
                ShowReferencialPanel2();
            }
            else if (aux == 2)
            {
                GameManager.Instance.UiManager.tipsReferencialChallenge.HidePanelTips();
                GameManager.Instance.ReferencialManager.PlayerToGround();
                GameManager.Instance.InputManager.EnablePlayerInput();
            }
            aux += 1;
        }
        else
        {
            // Errou - permite tentar novamente
            feedbackText.text = $"Resposta incorreta. Tente novamente.";//Nunca vai ser exibido pois o jogo está pausado
        }
    }
}
