using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MeetingPointChallenge : MonoBehaviour
{
    public string corretAnswer;
    [Header("Referências às UI Buttons")]
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;


    [Header("Referências de UI")]
    [SerializeField] private TMPro.TextMeshProUGUI feedbackText;      // Texto para exibir “errou/acertou”

    [Header("Portal Next Level")]
    [SerializeField] private GameObject portalNextLevel;

    private bool isPanelActive, rightAnwserClicked = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        // adiciona listeners programaticamente
        buttonA.onClick.AddListener(() => HandleAnswer("A"));
        buttonB.onClick.AddListener(() => HandleAnswer("B"));
        buttonC.onClick.AddListener(() => HandleAnswer("C"));
        buttonD.onClick.AddListener(() => HandleAnswer("D"));
    }


    public void ShowPanel()
    {
        feedbackText.text = "";
        gameObject.SetActive(true);
        isPanelActive = true;
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


    private void HandleAnswer(string answer)
    {
        if (!isPanelActive) return;

        if (rightAnwserClicked) return;
        if (answer == corretAnswer)
        {
            rightAnwserClicked = true;

            feedbackText.text = "Parabéns, você acertou!";
            GameManager.Instance.Phase3Manager.ExibirPontoEncontro();
            GameManager.Instance.InputManager.EnablePlayerInput();
            HidePanel();
            GameManager.Instance.UiManager.tipsMeetingPoint.HidePanelTips();
            GameManager.Instance.Despausar();
            portalNextLevel.SetActive(true);
        }
        else
        {
            // Errou - permite tentar novamente
            feedbackText.text = $"Resposta incorreta. Tente novamente.";//Nunca vai ser exibido pois o jogo está pausado
        }
    }
}
