using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    // จำนวน Object ที่ต้องการให้มีอยู่ใน Scene
    public int desiredObjectCount = 50;

    // Prefab ของ Object ที่ต้องการ Respawn
    public GameObject objectPrefab;

    // ตำแหน่งที่ต้องการให้ Object ใหม่เกิด
    public Transform[] respawnPoints;

    // ช่วงเวลาระหว่างการ Respawn (หน่วย: วินาที)
    public float respawnInterval = 1f;

    // ตัวแปรสำหรับควบคุมการ Respawn
    private float respawnTimer = 0f;

    void Start()
    {
        // สร้าง Object ที่ต้องการให้ครบจำนวนในตอนเริ่มต้น
        for (int i = 0; i < desiredObjectCount; i++)
        {
            SpawnObject();
        }
    }

    void Update()
    {
        // นับจำนวน Object ใน Scene โดยใช้ Tag ที่กำหนด
        int objectCount = GameObject.FindGameObjectsWithTag("RespawnTag").Length;

        // ถ้าจำนวน Object น้อยกว่าจำนวนที่ต้องการ
        if (objectCount < desiredObjectCount)
        {
            // เพิ่มเวลาของตัวจับเวลา
            respawnTimer += Time.deltaTime;

            // ถ้าตัวจับเวลาถึงเวลาที่กำหนด
            if (respawnTimer >= respawnInterval)
            {
                // ทำการ Respawn Object
                SpawnObject();

                // รีเซ็ตตัวจับเวลา
                respawnTimer = 0f;
            }
        }
    }

    void SpawnObject()
    {
        // เลือกตำแหน่ง Respawn แบบสุ่มจากที่กำหนด
        Transform respawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];

        // สร้าง Object ใหม่ที่ตำแหน่งที่กำหนด
        Instantiate(objectPrefab, respawnPoint.position, respawnPoint.rotation);
    }
}
