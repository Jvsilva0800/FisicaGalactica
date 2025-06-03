using UnityEngine;

public class BlockTracker : MonoBehaviour
{
    // Armazena a posição inicial do bloco (quando instanciado)
    private Vector3 initialPosition;
    // Armazena o tempo (em segundos) de quando o bloco foi instanciado
    private float initialTime;

    // Se necessário, um fator de conversão (por padrão, 1 unidade = 1 metro)
    private float unitToMeter = 1.0f;


    private void Start()
    {
        // Registra a posição e o tempo no momento da criação
        initialPosition = transform.position;
        initialTime = Time.time;
    }

    //O método OnDestroy é chamado automaticamente pela Unity pouco antes do GameObject (ou o componente, se estiver sendo destruído) ser removido da cena
    private void OnDestroy()
    {
        // Registra a posição e o tempo no momento da destruição
        float finalTime = Time.time;
        Vector3 finalPosition = transform.position;

        // Calcula o tempo decorrido (em segundos)
        float elapsedTime = (int)(finalTime - initialTime);

        // Calcula a distância percorrida (em metros, considerando a conversão)
        float distanceTraveled = (int)Vector3.Distance(initialPosition, finalPosition) * unitToMeter;
        float averageSpeed = distanceTraveled / elapsedTime; // m/s

        GameManager.Instance.UiManager.display.DisplayResults((int)elapsedTime, (int)distanceTraveled);
        GameManager.Instance.UiManager.speedChallenge.ShowPanel(averageSpeed);
        GameManager.Instance.UiManager.tipsSpeedChallenge.ShowPanelTips();

    }
}
