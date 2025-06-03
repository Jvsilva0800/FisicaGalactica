using UnityEngine;

public class PhaseProgressionManager : MonoBehaviour
{

    [SerializeField] private GameObjectCreator gameObjectCreator;
    [SerializeField] private EsteiraStates esteira;//Referencia a Esteira
    [SerializeField] private int maxStructures = 3; // Número máximo de estruturas
    [SerializeField] private PortalNextLevel portal;

    private int currentStructureCount = 0;

    /// <summary>
    /// Chama esse método quando o jogador acerta o desafio.
    /// Atualiza o contador e, se ainda não atingiu o limite, prepara e instancia a próxima estrutura.
    /// </summary>
    public void OnChallengeSucceeded()
    {
        currentStructureCount++;
        Debug.Log("Estrutura completada: " + currentStructureCount);

        if (currentStructureCount < maxStructures)
        {
            esteira.GoToNextState();//Segue para a nova posição da esteira
            gameObjectCreator.BuildGameObject();//Cria novamente o bloco no início da esteira
        }
        else
        {
            Debug.Log("Fase completa!");
            // Aqui você pode implementar a lógica de finalização da fase, exibir mensagem de vitória, ou transitar para o próximo nível.
            GameManager.Instance.InputManager.EnablePlayerInput();//Vai habilitar a movimentação do player assim que o mesmo responda as três perguntas corretamente.
            GameManager.Instance.UiManager.tipsSpeedChallenge.HidePanelTips();//Somente quando o desafio for completo em seus N estágios que o panel de dicas será ocultado

            portal.ActivatePortal();//Ativa o portal para a próxima cena
        }
    }


}
