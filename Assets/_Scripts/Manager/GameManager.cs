using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
        public static GameManager instance;

        [Header("------游戏数值------")]
    public int pesos;                                   public int experience;                              public List<int> weaponPrices;                      public List<int> xpTable;                       
        [Header("------资源------")]
    public List<Sprite> playerSprites;                  public List<Sprite> weaponSprites;              

        [Header("------引用------")]
    public Player player;                               public Weapon weapon;                               public UIManager UIManager;                         public SaveManager SaveManager;                 
    
    private void Awake()
    {
                if (GameManager.instance != null)
        {
            
            Destroy(player.gameObject);
            Destroy(gameObject);
            Destroy(UIManager.gameObject);
            Destroy(SaveManager.gameObject);
            return;
        }
               
        instance = this;

                SceneManager.sceneLoaded += LoadState;
        
                DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("UIManager"));
        DontDestroyOnLoad(GameObject.Find("SaveManager"));
    }

    


        public void OnUIChange()
    {
        UIManager.UIUpdate();
    }

        public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        UIManager.ShowText(msg, fontSize, color, position, motion, duration);
    }

        public bool TryUpgradeWeapon()
    {
                if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

                if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();

            return true;
        }
        return false;
    }

        public int GetCurrentLevel()
    {
                int l = 0, add = 0;
        while (experience >= add)
        {
            add += xpTable[l];
            l++;

            if (l == xpTable.Count)
                return l;
        }
        return l;
    }
    public int GetXPToLevel(int level)
    {
                int l = 0, xp = 0;
        while (l < level)
        {
            xp += xpTable[l];
            l++;
        }
        return xp;
    }
    public void GrantXP(int xp)
    {
                int currentLevel = GetCurrentLevel();
        experience += xp;

        OnUIChange();

        if (currentLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        ShowText("LEVEL UP!", 30, Color.yellow, player.transform.position, Vector3.up * 30, 2.0f);
        player.OnLevelUp();
        OnUIChange();     
    }


        public void Respawn()
    {
                SceneManager.LoadScene(1);
        UIManager.HideDeathAnimation();
        
                player.Respawn();
    }

        public void SaveState()
    {
                SaveManager.SaveGame();

                                                                
                    }

        public void LoadState(Scene s, LoadSceneMode sceneMode)
    {
        SaveManager.LoadGame();

                        
                                
                
                        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

                
                player.transform.position = GameObject.Find("SpawnPoint").transform.position;

        OnUIChange();
    }
}