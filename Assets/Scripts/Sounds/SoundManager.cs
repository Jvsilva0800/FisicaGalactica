using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private SoundLibrary sfxLibrary;
    [SerializeField]
    private AudioSource sfx2DSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    // NOVA FUNÇÃO: Versão 3D com controle de distância
    public void PlaySound3D(string soundName, Vector3 pos, float maxDistance)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip == null) return;

        // 1. Cria um objeto temporário para ser a fonte do som
        GameObject soundGameObject = new GameObject("TempAudio");
        soundGameObject.transform.position = pos;

        // 2. Adiciona o componente AudioSource
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        // 3. Configura as propriedades do AudioSource
        audioSource.clip = clip;
        audioSource.spatialBlend = 1.0f; // Essencial para o som ser 3D
        audioSource.minDistance = 0.5f;  // Distância onde o som começa a diminuir
        audioSource.maxDistance = maxDistance; // Sua distância máxima!
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // Curva de atenuação (mais natural)

        // 4. Toca o som
        audioSource.Play();

        // 5. Agenda a destruição do objeto após o clipe terminar
        Destroy(soundGameObject, clip.length);
    }

    public void PlaySound2D(string soundName)//Esta função é usada para sons que não pertencem a um local específico no mundo do jogo. Eles são frequentemente chamados de sons de "UI" (Interface do Usuário) ou sons ambientes
    {
        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }
}
