using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirEscena : MonoBehaviour
{
    [SerializeField]int indexScene;
    public void Abrir()
    {
        SceneManager.LoadScene(indexScene);
    }
}
