using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]Text TimeText;
    [SerializeField]Text EnemysText;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject WinScreen;
    // Start is called before the first frame updat
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void UpdateUITime(int time){
        TimeText.text = time.ToString();
    }

    public void UpdateUIEnemys(int enemys){
        EnemysText.text = enemys.ToString();
    }
    public void ShowGameOverScreen(){
        GameOverScreen.SetActive(true);
    }
    public void ShowWinScreen(){
        WinScreen.SetActive(true);
    }
}
