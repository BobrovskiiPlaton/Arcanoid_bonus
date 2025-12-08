using UnityEngine;

public class BonusMovement : MonoBehaviour
{
    public float speed = 3f;
    
    void Update()
    {
        // Двигаем бонус вниз
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}