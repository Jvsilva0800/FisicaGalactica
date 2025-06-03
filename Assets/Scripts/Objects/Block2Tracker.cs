using System;
using UnityEngine;

public class Block2Tracker : MonoBehaviour
{
    // Armazena o tempo (em segundos) de quando o bloco foi instanciado
    private float initialTime;

    private void Start()
    {
        initialTime = Time.time;
    }

    private void OnDestroy()
    {
        // Registra o tempo no momento da destruição
        float finalTime = Time.time;

        int iTime = (int)initialTime;
        int fTime = (int)finalTime;

        // Calcula o tempo decorrido (em segundos)
        float elapsedTime = fTime - iTime;
        GameManager.Instance.UiManager.display.DisplayTimeVariation(iTime, fTime);
        GameManager.Instance.UiManager.timeChallenge.ShowPanel(elapsedTime);
        GameManager.Instance.UiManager.tipsTimeChallenge.ShowPanelTips();

    }
}
