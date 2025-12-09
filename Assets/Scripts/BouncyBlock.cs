using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    public float speed = 2f;
    public float maxX = 6f;
    private float startX;
    private float fixedY;
    private bool movingRight;
    private float currentX;

    private void Start()
    {
        startX = transform.position.x;
        fixedY = transform.position.y;
        currentX = startX;
        
        movingRight = Random.value > 0.5f;
        
        // Проверяем настройки коллайдера
        Collider2D collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Обновляем позицию
        currentX += (movingRight ? speed : -speed) * Time.deltaTime;
    
        // Проверяем выход за границы
        if (currentX > maxX)
        {
            currentX = maxX;
            movingRight = false;
        }
        else if (currentX < -maxX)
        {
            currentX = -maxX;
            movingRight = true;
        }
    
        // Устанавливаем позицию
        transform.position = new Vector2(currentX, fixedY);
    }

    // Обработка столкновения с другими объектами
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("PurpleWall"))
            movingRight = !movingRight;
    }
}