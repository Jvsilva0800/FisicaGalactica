using UnityEngine;

[System.Serializable]
public struct EsteiraTransformState
{
    public Vector3 position;    // Posição local ou global, conforme desejar
    public Vector3 rotation;    // Rotação em Euler angles
    public Vector3 scale;       // Escala local
}

public class EsteiraStates : MonoBehaviour
{
    [Header("Defina aqui até 3 estados de posição/rotação/escala")]
    [SerializeField] private EsteiraTransformState[] states;

    private int currentIndex = 0;

    /// <summary>
    /// Avança para o próximo estado da esteira (posição, rotação e escala).
    /// </summary>
    public void GoToNextState()
    {
        if (currentIndex < states.Length)
        {
            // Aplica as transformações definidas no Inspector
            transform.position = states[currentIndex].position;
            transform.eulerAngles = states[currentIndex].rotation;
            transform.localScale = states[currentIndex].scale;

            currentIndex++;
        }
        else
        {
            Debug.Log("Todos os estados da esteira já foram aplicados.");
        }
    }

    /// <summary>
    /// Reseta para o primeiro estado (opcional).
    /// </summary>
    public void ResetToFirstState()
    {
        currentIndex = 0;
        GoToNextState();
    }
}
