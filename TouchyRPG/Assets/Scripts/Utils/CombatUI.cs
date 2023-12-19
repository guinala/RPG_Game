using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject combatMenu;
    public GameObject wonMenu;
    public GameObject lostMenu;

    public TextMeshProUGUI infoText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI earnedGoldText;

    public TextMeshProUGUI playerName;
    public Slider playerHP;
    public TextMeshProUGUI enemyName;
    public Slider enemyHP;


    // Private
    private CombatUnitSO _player;
    private CombatUnitSO _enemy;


    public void SetupHUD(CombatUnitSO player, CombatUnitSO enemy, int level, int gold)
    {
        // Save references for later
        this._player = player;
        this._enemy = enemy;

        // Link HUD
        this.playerName.text = this._player.unitName;
        this.playerHP.minValue = 0;
        this.playerHP.maxValue = this._player.maxHP;

        this.enemyName.text = this._enemy.unitName;
        this.enemyHP.minValue = 0;
        this.enemyHP.maxValue = this._enemy.maxHP;

        this.levelText.text = "Level \n" + level.ToString();
        this.goldText.text = gold.ToString();
    }


    public void SetInfoText(string infoText)
    {
        this.infoText.text = infoText;
    }

    public void ShowCombatMenu()
    {
        combatMenu.SetActive(true);
        wonMenu.SetActive(false);
        lostMenu.SetActive(false);
    }

    public void ShowWonMenu(int earnedGold)
    {
        this.earnedGoldText.text = "+" + earnedGold.ToString();

        wonMenu.SetActive(true);
        combatMenu.SetActive(false);
        lostMenu.SetActive(false);
    }

    public void ShowLostMenu()
    {
        lostMenu.SetActive(true);
        wonMenu.SetActive(false);
        combatMenu.SetActive(false);
    }

    public void ResetHUD()
    {
        this._player = null;
        this._enemy = null;

        // Link HUD
        this.playerName.text = "";
        this.playerHP.minValue = 0;
        this.playerHP.maxValue = 0;
        this.playerHP.value = 0;

        this.enemyName.text = "";
        this.enemyHP.minValue = 0;
        this.enemyHP.maxValue = 0;
        this.enemyHP.value = 0;

        this.levelText.text = "Level \n-";
        this.goldText.text = "";
    }

    private void Update()
    {
        if (this._player != null)
            this.playerHP.value = this._player.currentHP;

        if (this._enemy != null)
            this.enemyHP.value = this._enemy.currentHP;
    }
}
