using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralLevelLoader : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update

    // Update is called once per frame
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);

    }
}
