using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

}
