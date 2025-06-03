using UnityEngine;

public class CircleTracker : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float initialTime;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        initialTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.cicle++;
        // Registra o tempo no momento da destruição
        float finalTime = Time.time;
        float finalVelocity = (int)rigidBody.linearVelocity.magnitude;//(int) para forçar o valor para inteiro, fazendo com que os valores sejam redondos

        // Calcula o tempo decorrido (em segundos)
        float elapsedTime = (int)(finalTime - initialTime);

        float averageAcceleration = finalVelocity / elapsedTime;
        //Switch responsável por mudar os paineis dos subdesafios utilizando se da variável cicle que está no gameManager para poder ser acessada e exergado por qualquer local do código
        switch (GameManager.Instance.cicle)
        {
            case 1:
                GameManager.Instance.UiManager.display.DisplayAverageAcceleration(elapsedTime, finalVelocity);
                GameManager.Instance.UiManager.panel2.ShowPanelAverageAcceleration(averageAcceleration);
                break;
            case 2:
                GameManager.Instance.UiManager.display.DisplayElapsedTime(averageAcceleration, finalVelocity);
                GameManager.Instance.UiManager.panel2.ShowPanelElapsedTime(elapsedTime);
                break;
            case 3:
                GameManager.Instance.UiManager.display.DisplayFinalVelocity(averageAcceleration, elapsedTime);
                GameManager.Instance.UiManager.panel2.ShowPanelFinalVelocity(finalVelocity);
                break;
        }


    }
}
