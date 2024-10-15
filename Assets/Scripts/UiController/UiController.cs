using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public static UiController Instance;
    [SerializeField] private GameObject keyPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotKey()
    {
        StartCoroutine(ShowKeyCollection());
       
    }

    IEnumerator ShowKeyCollection()
    {
        EnemyController[] des = FindObjectsOfType<EnemyController>();
        PausePlayerAndEnemy(false, des);
        keyPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        keyPanel.SetActive(false);
        PausePlayerAndEnemy(true, des);
    }


    void PausePlayerAndEnemy(bool status, EnemyController[] enemies)
    {
        foreach (EnemyController e in enemies)
        {
            e.StartMoving = status;
        }
        PlayerController.Instance.AllowMovement = status;
    }
}
