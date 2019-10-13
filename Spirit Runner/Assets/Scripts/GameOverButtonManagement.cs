using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButtonManagement : MonoBehaviour
{
    public SceneManager sceneManager;
    public Button gameoverBTN;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameoverBTN.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        SceneManager.LoadScene(0);
    }
}
