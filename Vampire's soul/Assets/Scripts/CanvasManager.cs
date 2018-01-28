using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public Image playerHealthBar;
    public Image playerHealthBarGold;

    public Image biteIcon;
    public Image blinkIcon;
    public Image waveIcon;

    public Image biteCoolDownIcon;
    public Image blinkCoolDownIcon;
    public Image waveCoolDownIcon;

    public Image potion;
    public Image potionFrame;

    public Image stune;

    private int biteCoolDown = 1;
    private int blinkCoolDown = 6;
    private int waveCoolDown = 4;
    private int stuneCoolDown = 0;

    private int biteCoolDownCurrent = 0;
    private int blinkCoolDownCurrent = 0;
    private int waveCoolDownCurrent = 0;
    private int stuneCoolDownCurrent = 0;

    public void SetHealthBar(float a)
    {
        playerHealthBar.fillAmount = a;
        playerHealthBarGold.fillAmount = a;
    }

    public void SetHealthBarGold(float a)
    {
        playerHealthBar.fillAmount = a;
    }

    public bool SelectBite()
    {
        if (biteCoolDownCurrent == 0)
        {
            biteIcon.enabled = true;
            blinkIcon.enabled = false;
            waveIcon.enabled = false;
            return true;
        }
        return false;
    }

    public bool SelectBlink()
    {
        if (blinkCoolDownCurrent == 0)
        {
            biteIcon.enabled = false;
            blinkIcon.enabled = true;
            waveIcon.enabled = false;
            return true;
        }
        return false;
    }

    public bool SelectWave()
    {
        if (waveCoolDownCurrent == 0)
        {
            biteIcon.enabled = false;
            blinkIcon.enabled = false;
            waveIcon.enabled = true;
            return true;
        }
        return false;
    }

    public void UseBite()
    {
        biteCoolDownCurrent = biteCoolDown;
        biteCoolDownIcon.fillAmount = 0;
    }
    public void UseBlink()
    {
        blinkCoolDownCurrent = blinkCoolDown;
        blinkCoolDownIcon.fillAmount = 0;
    }
    public void UseWave()
    {
        waveCoolDownCurrent = waveCoolDown;
        waveCoolDownIcon.fillAmount = 0;
    }

    public void UsePotion()
    {
        potion.fillAmount = 0;
        potionFrame.enabled = false;
    }

    public void AddIngredient()
    {
        potion.fillAmount += 1f / 3;
        if (potion.fillAmount == 1) potionFrame.enabled = true;
    }

    public void StuneAdd(int time)
    {
        stuneCoolDown += time-1;
        stuneCoolDownCurrent += time;
    }

    public void StuneReplace(int time)
    {
        stuneCoolDown = time-1;
        stuneCoolDownCurrent = time;
    }

    public void NextTour()
    {
        if (biteCoolDownCurrent > 0)
        {
            biteCoolDownCurrent--;
            biteCoolDownIcon.fillAmount = 1 - (float)biteCoolDownCurrent / biteCoolDown;
        }
        if (blinkCoolDownCurrent > 0)
        {
            blinkCoolDownCurrent--;
            blinkCoolDownIcon.fillAmount = 1 - (float)blinkCoolDownCurrent / blinkCoolDown;
        }
        if (waveCoolDownCurrent > 0)
        {
            waveCoolDownCurrent--;
            waveCoolDownIcon.fillAmount = 1 - (float)waveCoolDownCurrent / waveCoolDown;
        }
        if (stuneCoolDownCurrent > 0)
        {
            Debug.Log(stuneCoolDownCurrent);
            stuneCoolDownCurrent--;
            Debug.Log(stuneCoolDownCurrent);
            stune.fillAmount = (float)stuneCoolDownCurrent / stuneCoolDown;
        }
        if (stuneCoolDownCurrent <= 0 && stuneCoolDown != 0)
        {
            stuneCoolDownCurrent = 0;
            stuneCoolDown = 0;
        }
    }

    private void Awake()
    {
        biteIcon.enabled = true;
        blinkIcon.enabled = false;
        waveIcon.enabled = false;
        potionFrame.enabled = false;
        potion.fillAmount = 0;
        stune.fillAmount = 0;
    }

}
