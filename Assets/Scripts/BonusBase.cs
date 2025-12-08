using UnityEngine;
using UnityEngine.UI;

public class BonusBase : MonoBehaviour
{
    // Публичные поля для настройки в инспекторе
    public string bonusText = "+100";
    public Color backgroundColor = Color.yellow;
    public Color textColor = Color.black;
    
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
        // Добавляем 100 очков (базовая реализация)
        Debug.Log("Бонус активирован: +100 очков");
        
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