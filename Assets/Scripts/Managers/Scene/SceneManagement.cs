using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

  

    private void GoToMainMenu()
    {

        SceneManager.LoadScene(0);
    }
    private void GoToDiceGameScene()
    {
        SceneManager.LoadScene(1);
    }
  
   
}
