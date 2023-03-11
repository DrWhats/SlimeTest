using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float existenceTime = 3f; // время существования объекта в секундах

    private float spawnTime; // время, когда объект был создан

    void Start()
    {
        spawnTime = Time.time; // запоминаем время создания объекта
    }

    void Update()
    {
        if (Time.time - spawnTime > existenceTime) // если время жизни объекта больше заданного времени
        {
            Destroy(gameObject); // уничтожаем объект
        }
    }
}
