using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            Init();
            return instance;
        }
    }
    static void Init() // �ٸ� �Ŵ����鵵 ���⼭ GameManager ��ü�� �߰�
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (instance == null)
        {
            if (gameManager == null)
            {
                gameManager = new GameObject("GameManager");
                gameManager.AddComponent<GameManager>();
            }
            else
            {

            }

            DontDestroyOnLoad(gameManager);
            instance = gameManager.GetComponent<GameManager>();
            Debug.Log("GameManager Load");
        }
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            Init();
        }
    }


}