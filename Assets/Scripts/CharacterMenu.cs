using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, moneyText, upgradeCostText, xpText;

    //Logic - for the character sprite and also the weapon sprite;
    private int currentCharacterSelection = 0;
    public Image CharacterSelectionSprite;
    public Image WeaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //If we went to far away in the aray
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }

        else
        {
            currentCharacterSelection--;

            //If we went to far away in the aray
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        CharacterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();     
    }

    // Update the charcter information
    public void UpdateMenu()
    {
        //Weapon
        WeaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        //Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        moneyText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();


        //xp Bar
        //we have to check whether we are at the maximum level before trying to upgrade our level
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int previousLevelxp = GameManager.instance.GetXpTopLevel(currentLevel - 1);
            int currentLevelxp = GameManager.instance.GetXpTopLevel(currentLevel);

            int diff = currentLevelxp - previousLevelxp;
            int currentXpIntoLevel = GameManager.instance.experience - previousLevelxp;

            float completionRatio = (float)currentXpIntoLevel / (float)diff; //we calculate the ratio between our gained xp and the one nedeed for upgrading to the next level
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpIntoLevel.ToString() + " / " + diff;

        }

    }
}
