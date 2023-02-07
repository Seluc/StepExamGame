using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewIsland : MonoBehaviour
{
    // Start is called before the first frame update
    private AsyncOperation asyncLoadLevelOn;
    
    public void LoadIslandOne()
    {
        if (asyncLoadLevelOn == null)
        {
            StartCoroutine(LoadIslandOneAsync());
        }
    }

    private IEnumerator LoadIslandOneAsync()
    {
        asyncLoadLevelOn = SceneManager.LoadSceneAsync("Level 1 Island", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoadLevelOn.isDone)
        {
            yield return null;
        }
    }
    
    
}
