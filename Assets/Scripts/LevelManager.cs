using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public Slider progressBar;//Referência a um Slider de UI que será usado para mostrar o progresso de carregamento.
    public GameObject transitionsContainer;//GameObject que agrupa (como filhos) todos os componentes de transição de cena (SceneTransition).
    private SceneTransition[] transitions;//Array privado que irá armazenar todas as instâncias de SceneTransition encontradas nesse container.

    private void Start()
    {
        //Busca, sob transitionsConatiner, todos os componentes que herdam de SceneTransition (inclusive nos netos), e os armazena no array transitions. Isso facilita escolher dinamicamente qual animação usar.
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
    }

    //Método público para iniciar o processo de troca de cena. 
    public void LoadScene(string sceneName, string transitionName)
    {
        //Dispara a corrotina LoadSceneAsync, que faz o carregamento assíncrono combinado com as animações de transição.
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        //Usando LINQ, seleciona o primeiro elemento em transitions cujo name (nome do GameObject/Componente) bate com transitionName. Essa será a animação de entrada/saída usada.
        SceneTransition transition = transitions.First(t => t.name == transitionName);


        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);//Inicia o carregamento da cena em background, retornando um AsyncOperation que indica o progresso.
        scene.allowSceneActivation = false;//Impede que a cena seja ativada automaticamente quando atingir 90% de progresso. Assim, dá para controlar exatamente o momento de troca (após a animação).

        yield return transition.AnimateTransitionIn();//Invoca a corrotina de “entrada” definida na transição. O yield return faz a função aguardar até essa animação terminar.

        progressBar.gameObject.SetActive(true);// Ativa o objeto do slider para que o jogador veja o progresso de carregamento.


        do
        {
            /*
           O scene.progress varia de 0 a 0.9 enquanto a cena carrega; Unity reserva os últimos 10% para a ativação.
           A cada frame (yield return null), atualiza progressBar.value com o valor atual.*/
            progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);// O laço só sai quando scene.progress atingir 0.9 (isto é, carregou tudo, mas ainda não ativou)


        scene.allowSceneActivation = true;
        yield return null;//Ativa a cena – Como allowSceneActivation agora é true, a Unity completa o carregamento e troca de cena.
        progressBar.gameObject.SetActive(false);

        yield return transition.AnimateTransitionOut();//Invoca a corrotina de “saída” da transição. O yield return faz a corrotina aguardar o fim dessa animação antes de concluir.

    }
}
