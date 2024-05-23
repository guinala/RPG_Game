using UnityEngine;
using ScriptableObjectArchitecture;

public class SceneLoader : MonoBehaviour
{
    [Header("Configuration")]
    public SceneSO sceneToLoad;
    public LevelEntranceSO levelEntrance;
    public bool loadingScreen;

    [Header("Player Path")]
    public PlayerPathSO playerPath;

    [Header("Broadcasting events")]
    public LoadSceneRequestGameEvent loadSceneEvent;

    [SerializeField] private MusicArea musicArea;

    public void LoadScene()
    {
        if (this.levelEntrance != null && this.playerPath != null)
            this.playerPath.levelEntrance = this.levelEntrance;

        var request = new LoadSceneRequest(
            scene: this.sceneToLoad,
            loadingScreen: this.loadingScreen
        );

        this.loadSceneEvent.Raise(request);

        Debug.Log("Aquí cambio de música");
        AudioManager.instance.setMusicArea(musicArea);
        
    }
}
