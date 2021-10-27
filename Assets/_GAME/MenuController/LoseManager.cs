using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Nom de la sc�ne � charger quand on appuie sur play")]
    private string levelToLoad = "MainMenu";

    public void ButtonQuit()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
