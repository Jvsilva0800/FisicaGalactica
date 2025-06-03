using UnityEngine;
public static class SpawnBlockEvent //É uma classe estática, ou seja, você não precisa criar instâncias dela — basta acessar SpawnEvents diretamente de qualquer script.
{
    // 1) Declara um evento estático chamado OnSpawn,
    //    que transporta um GameObject como “payload”.
    //    System.Action<GameObject> é um delegate que recebe um GameObject.
    public static event System.Action<GameObject> OnSpawn;

    public static void RaiseOnSpawn(GameObject go)//função que deve ser chamada no gameobject que será enviado
      => OnSpawn?.Invoke(go);//significa “chama todos os métodos que se inscreveram, passando o GameObject recém-criado”.
}
