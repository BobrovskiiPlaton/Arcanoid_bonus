using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenBlock : MonoBehaviour
{
    public GameObject bonusPrefab; // Префаб бонуса
    public float bonusSpeed = 3f; // Скорость падения бонуса
    
    private bool isQuitting = false;
    private bool gameHasStarted = false;
    
    private void Start()
    {
        // Ждем немного, чтобы игра успела начаться
        // или используем флаг, который устанавливается в GameManager
        Invoke(nameof(MarkGameStarted), 0.1f);
    }
    
    private void MarkGameStarted()
    {
        gameHasStarted = true;
    }
    
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    
    private void OnDestroy()
    {
        // Если приложение закрывается, не создаем бонус
        if (isQuitting) return;
        
        // Если префаб не назначен, не создаем бонус
        if (bonusPrefab == null) return;
        
        // Если сцена не загружена, не создаем бонус
        if (!gameObject.scene.isLoaded) return;
        
        // Не создаем бонус, если игра еще не началась (при старте сцены)
        if (!gameHasStarted) return;
        
        // Не создаем бонус, если сцена выгружается (при перезагрузке)
        if (SceneManager.GetActiveScene().isLoaded == false) return;
        
        CreateBonus();
    }
    
    private void CreateBonus()
    {
        GameObject bonusObj = Instantiate(bonusPrefab, transform.position, Quaternion.identity);

        BonusBase bonusScript = bonusObj.AddComponent<BonusBase>();
        bonusScript.Initialize();
        
        BonusMovement movement = bonusObj.AddComponent<BonusMovement>();
        movement.speed = bonusSpeed;
    }
}