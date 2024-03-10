using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {

        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(HUD);
            Destroy(Menu);
            return;
        }


        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded; //this will be called every time we load the scene
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player_0 player;
    public Weapon weapon; 
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointbar;
    public Animator DeathMenuAnim;
    public GameObject HUD;
    public GameObject Menu;

    //Logic
    public int pesos = 0;
    public int experience = 0;

    //FloatingText
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // if this function returns FALSE, we are not going to chance anything on MENU
        // if this function returns TRUE, it means that we can upgrade the weapon = change the weapon sprite to another weapon sprite

        // is the weapon max level rn?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false; //this one is returned when we are not in any of these cases - our weapon is not at the maximum level, neither do we have enough money to upgrade our weapon

    }

    //HealthBar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxhitpoint;
        hitpointbar.localScale = new Vector3(1, ratio, 1);
    }

    //Experience System

    public int GetCurrentLevel()
    { 
        int r = 0; //return value
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max Level
                return r;
        }

        return r;
    }
    public int GetXpTopLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        //when we actually kill an enemy, instead of adding the experience manually, this function will check whether we leveled up or not
        int currentLevel = GetCurrentLevel();
        experience += xp;

        if (currentLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLevelUp();
        OnHitpointChange();
    }

    //save state
    ///*
    ///* INT preferedSkin
    ///* INT pesos
    ///* INT experience;
    ///* INT weaponLevel;
    ///*
    ///

    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Death Menu and Respawn
    public void Respawn()
    {
           DeathMenuAnim.SetTrigger("HideMenu");
           UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
           player.Respawn();  
    }

    //Save State
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //this will be called only the first time we load the scene
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change Player Skin
        pesos = int.Parse(data[1]);

        //Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        player.SetLevel(GetCurrentLevel());

        //Change the Weapon Level
        weapon.weaponLevel = int.Parse(data[3]);
        weapon.SetWeaponLevel(int.Parse(data[3]));

    }
}
