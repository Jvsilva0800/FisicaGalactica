using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;//referência a um componente Slider da UI, que exibirá visualmente o valor de vida. 
    public Gradient gradient;//referência a um Gradient (de cor), que define como a cor da barra varia conforme o valor
    public Image fill;//referência ao Image que representa a parte preenchida do Slider 

    public void SetMaxHealth(int health)//método que inicializa a barra vida com o valor máximo
    {
        slider.maxValue = health;// faz com que o Slider vá de 0 até health
        slider.value = health;//Configura o valor atual do Slider igual ao máximo, “enchendo” a barra totalmente no início

        fill.color = gradient.Evaluate(1f);//retorna a cor no fim do gradiente 
    }

    public void SetCurrentHealth(int health)//chamado sempre que a vida do jogador (ou objeto) muda
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);//Ajusta a cor do preenchimento de acordo com a proporção atual (slider.normalizedValue varia de 0 a 1). Ex.: vida em 50% → cor no meio do gradiente. 
    }
}
