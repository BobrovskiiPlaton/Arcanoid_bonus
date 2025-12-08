using UnityEngine;
using UnityEngine.UI;

public class BonusBase : MonoBehaviour
{
    // Публичные поля для настройки в инспекторе
    public string bonusText = "+100";
    public Color backgroundColor = Color.yellow;
    public Color textColor = Color.black;
    public int bonusPoints = 100; // Добавляем поле для очков
    
    // Ссылки на компоненты
    private SpriteRenderer spriteRenderer;
    private Text uiText;
    
    // Метод для инициализации бонуса
    public void Initialize()
    {
        // Получаем или находим компоненты
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiText = GetComponentInChildren<Text>();
        
        // Применяем настройки
        if (spriteRenderer != null)
        {
            spriteRenderer.color = backgroundColor;
        }
        
        if (uiText != null)
        {
            uiText.text = bonusText;
            uiText.color = textColor;
        }
    }
    
    // Виртуальный метод активации бонуса
    public virtual void BonusActivate()
    {
        // Находим PlayerScript и добавляем очки
        PlayerScript player = FindObjectOfType<PlayerScript>();
        if (player != null)
        {
            player.BlockDestroyed(bonusPoints);
            Debug.Log($"Бонус активирован: +{bonusPoints} очков");
        }
        else
        {
            Debug.LogWarning("PlayerScript не найден!");
        }
        
        // Уничтожаем объект бонуса
        Destroy(gameObject);
    }
    
    // Обработка столкновений
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что коснулись ракетки
        if (other.CompareTag("Player"))
        {
            BonusActivate();
        }
        // Проверяем, что коснулись нижней границы
        else if (other.CompareTag("BottomWall"))
        {
            // Разрушаем без активации
            Destroy(gameObject);
        }
    }
}