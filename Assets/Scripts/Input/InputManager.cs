using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;


    public float Movement => playerControls.Gameplay.MoveDirection.ReadValue<float>();
    public event Action OnJump; //Permitir que outras partes do código se inscrevam para serem notificadas quando o jogador executar a ação de pulo.
    public event Action OnAttack;
    public InputManager()//Construtor
    {
        playerControls = new();//Instanciação da classe PlayerControls
        playerControls.Gameplay.Enable();// Isso faz com que o sistema de inputs passe a escutar as entradas definidas para o gameplay, permitindo que as ações (como a de movimento) sejam registradas e lidas
        playerControls.Gameplay.Jump.performed += OnJumpPerformed;//Acessa a ação Jump do mapa de ações Gameplay. Inscreve o método OnJumpPerformed para ser chamado sempre que a ação de pulo for executada (quando o input associado é realizado).Essa assinatura faz com que, ao ocorrer o evento performed da ação Jump, o método OnJumpPerformed seja invocado.

        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
    }


    private void OnJumpPerformed(InputAction.CallbackContext context)//O parâmetro InputAction.CallbackContext context contém informações sobre o evento
    {
        OnJump?.Invoke();// verifica se há algum assinante no evento OnJump e, se houver, dispara o evento.
    }//Proposito dessa função: Notificar todas as partes que se inscreveram no evento OnJump de que o jogador executou a ação de pulo.

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }

    public void DisablePlayerInput() => playerControls.Gameplay.Disable();// método que desative o input do player

    public void EnablePlayerInput() => playerControls.Gameplay.Enable();//método que habilita o input do player
}
