using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject[] enemies;
    public int EnemyNumber;
    public int time = 0;
    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyNumber = enemies.Length;
        UIManager.Instance.UpdateUIEnemys(EnemyNumber);
        StartCoroutine(UpdateTime());
    }

    public void EnemyDied(){
        EnemyNumber--;
        UIManager.Instance.UpdateUIEnemys(EnemyNumber);
        if(EnemyNumber == 0){
            UIManager.Instance.ShowWinScreen();
        }
    }
    public void PlayAgain(){
        SceneManager.LoadScene("Game");
    }

    IEnumerator UpdateTime(){
        while(true){
            yield return new WaitForSeconds(1);
            Debug.Log("1 second");
            UIManager.Instance.UpdateUITime(time++);
        }
    }
}
