using System;
using TMPro;
using UnityEngine;

public class Block4Tracker : MonoBehaviour
{
    // Armazena a posição inicial do bloco (quando instanciado)
    private Vector3 initialPosition, realTimePosition;
    // Armazena o tempo (em segundos) de quando o bloco foi instanciado
    private float initialTime, distanceTravelled, timeTravelled, inRealTime;

    // Se necessário, um fator de conversão (por padrão, 1 unidade = 1 metro)
    private float unitToMeter = 1.0f;
    [SerializeField] private TextMeshPro meter;//Texto que exibe a distancia que o bloco percorre
    [SerializeField] private TextMeshPro time;//Texto que exibe o tempo do bloco desde a sua criação

    private void Start()
    {
        // Registra a posição e o tempo no momento da criação
        initialPosition = transform.position;
        initialTime = Time.time;
    }

    private void Update()
    {
        realTimePosition = transform.position;
        inRealTime = Time.time;
        distanceTravelled = (int)Math.Ceiling(Vector3.Distance(initialPosition, realTimePosition) * unitToMeter);///"Math.Ceiling" arredonda para cima
        timeTravelled = (int)Math.Ceiling(inRealTime - initialTime);
        meter.text = $"{distanceTravelled} m";//Atualização do texto com a distancia percorrida;
        time.text = $"{timeTravelled} s";//atualização do texto com o tempo de vida do bloco
    }

    //O método OnDestroy é chamado automaticamente pela Unity pouco antes do GameObject (ou o componente, se estiver sendo destruído) ser removido da cena
    private void OnDestroy()
    {
        GameManager.Instance.InputManager.EnablePlayerInput();
        GameManager.Instance.UiManager.tipsPositionTimeFunction.HidePanelTips();
        GameManager.Instance.UiManager.challengePositionTimeFunction.HidePanel();
    }

    public float GetDistanceTravelled() => distanceTravelled;
    public float GetTimeTravelled() => timeTravelled;
}
