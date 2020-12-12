using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleView2D : MonoBehaviour
{


    public void OnClickCustomBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Customize");
    }
}
