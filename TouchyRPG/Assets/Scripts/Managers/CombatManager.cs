using UnityEngine;
using System.Collections;

public enum CombatStates
{
    NONE,
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class CombatManager : MonoBehaviour
{
    [Header("Combat Configuration")]
    public int baseGoldPrize = 10;
    public float prizeIncrementPerLevel = 0.20f; // 20%
    public float difficultyIncrementPerLevel = 0.15f; // 15%
    public float timeBetweenActions = 1.5f; // 1.5 seconds

    [Header("Combat Messages Configuration")]
    public string combatStartedInfoText = "An enemy appeared...";
    public string playerTurnInfoText = "Player's turn...";
    public string playerWonInfoText = "Enemy defeated!";
    public string enemyTurnInfoText = "Enemy's turn...";
    public string enemyAttackedInfoText = "Enemy attacked!";

    [Header("Dependencies")]
    public CombatUI combatUI;


    // Private

    private CombatStates _combatState = CombatStates.NONE;
    private int _currentLevel = 1;
    private int _gold = 0;

    private CombatRequest _request;
    private CombatUnitSO _currentEnemy;
    private GameObject _currentEnemyGO;

    public void SetupCombat(CombatRequest request)
    {
        // Save references for later
        this._request = request;

        // Set player's HP to max
        this._request.player.ResetHP();

        // Instantiate player
        GameObject playerGO = Instantiate(this._request.player.unitPrefab, request.playerPosition.position, Quaternion.identity);
        playerGO.transform.parent = request.playerPosition;

        // Start combat
        StartCoroutine(StartCombat());
    }

    public void NextCombat()
    {
        this._currentLevel += 1;
        StartCoroutine(StartCombat());
    }

    public void ResetCombat()
    {
        this._currentLevel = 1;
        this._gold = 0;
        StartCoroutine(StartCombat());
    }

    public void OnPlayerUsedItem(int inventoryItemId)
    {
        if (this._combatState != CombatStates.PLAYERTURN)
            return;

        if (inventoryItemId == 0 || inventoryItemId == 1) // Player used a weapon
        {
            if (this._request.player.inventory.weapons.Count > inventoryItemId)
            {
                var usedWeapon = this._request.player.inventory.weapons[inventoryItemId];

                this._request.player.AttackUnit(this._currentEnemy, usedWeapon);

                if (this._currentEnemy.currentHP <= 0f) // Enemy is dead
                {
                    StartCoroutine(CombatWon());
                }
                else
                {
                    StartCoroutine(EnemyTurn());
                }
            }
        }

        else if (inventoryItemId == 2 || inventoryItemId == 3)
        {
            var consumableIndex = inventoryItemId % 2;

            if (this._request.player.inventory.consumables.Count > consumableIndex)
            {
                var usedConsumable = this._request.player.inventory.consumables[consumableIndex];

                this._request.player.Heal(usedConsumable.item.healPower);
                this._request.player.inventory.RemoveConsumable(usedConsumable.item);
            }

            StartCoroutine(EnemyTurn());
        }
    }


    // Combat status

    private IEnumerator StartCombat()
    {
        this._combatState = CombatStates.START;


        int randomNumber = Random.Range(0, this._request.enemies.Length);
        this._currentEnemy = this._request.enemies[randomNumber];

        // Setup enemy's life
        if (this._currentLevel > 1)
            this._currentEnemy.maxHP *= (1 + difficultyIncrementPerLevel);

        this._currentEnemy.currentHP = this._currentEnemy.maxHP;

        // Instantiate enemy
        this._currentEnemyGO = Instantiate(this._currentEnemy.unitPrefab, this._request.enemyPosition.position, Quaternion.identity);
        this._currentEnemyGO.transform.parent = this._request.enemyPosition;

        // Configure HUD
        this.combatUI.ResetHUD();
        this.combatUI.ShowCombatMenu();
        this.combatUI.SetupHUD(this._request.player, this._currentEnemy, this._request.player.inventory, this._currentLevel, this._gold);
        this.combatUI.SetInfoText(combatStartedInfoText);

        yield return new WaitForSeconds(this.timeBetweenActions);

        PlayerTurn();
    }

    private void PlayerTurn()
    {
        this._combatState = CombatStates.PLAYERTURN;
        this.combatUI.SetInfoText(playerTurnInfoText);

        // Wait until player attacks
    }

    private IEnumerator EnemyTurn()
    {
        this._combatState = CombatStates.ENEMYTURN;
        this.combatUI.SetInfoText(enemyTurnInfoText);

        yield return new WaitForSeconds(this.timeBetweenActions);

        // Enemy attacks
        this.combatUI.SetInfoText(enemyAttackedInfoText);

        var usedWeapon = this._currentEnemy.inventory.weapons[0]; // Always the first weapon for the enemy, change if you like
        this._currentEnemy.AttackUnit(this._request.player, usedWeapon);

        yield return new WaitForSeconds(this.timeBetweenActions);

        if (this._request.player.currentHP <= 0f) // Player is dead
        {
            CombatLost();
        }
        else
        {
            PlayerTurn();
        }
    }

    private IEnumerator CombatWon()
    {
        this._combatState = CombatStates.WON;

        // Set UI text
        this.combatUI.SetInfoText(playerWonInfoText);

        // Get some money for your win
        var earnedGold = (int)(this.baseGoldPrize * (1 + this.prizeIncrementPerLevel * this._currentLevel));
        this._gold += earnedGold;

        // Wait a bit
        yield return new WaitForSeconds(this.timeBetweenActions);

        // And show the win menu
        this.combatUI.ShowWonMenu(this._gold);
        this.ResetEnemysHPToBase();
        Destroy(this._currentEnemyGO);
        this._currentEnemyGO = null;
    }

    private void CombatLost()
    {
        this._combatState = CombatStates.LOST;

        this.combatUI.ShowLostMenu();
        this.ResetEnemysHPToBase();
    }

    public void FinishCombat()
    {
        this._request.player.inventory.AddGold(this._gold);
    }

    private void ResetEnemysHPToBase()
    {
        this._currentEnemy.maxHP = this._currentEnemy.baseHP;
    }
}
