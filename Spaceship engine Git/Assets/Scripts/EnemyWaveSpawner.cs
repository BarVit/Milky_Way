using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject hp_bar;
    private int waveNumber;
    private bool isBusy;
    private GameObject enemy;
    void Awake()
    {
        waveNumber = 1;
        isBusy = false;
    }

    //      малый квадрат - ближний бой
    //      большой квадрат - дальний бой
    //      кружок - суппорт
    //
    //       оординаты:
    //      волна 1 - 0, 600, размер респа 100*100
    //      волна 2 - -400, 300, размер респа 100*100
    //      волна 3 - 600, 400, размер респа 200*200
    //
    public void SpawnWave()
    {
        if (waveNumber == 5) EndGame();
        if (!isBusy)
        {
            isBusy = true;
            switch (waveNumber)
            {
                case 1:
                    EnemySpawner(new Vector3(-50, 0, 600), "close");
                    EnemySpawner(new Vector3(0, 0, 600), "close");
                    EnemySpawner(new Vector3(50, 0, 600), "close");
                    EnemySpawner(new Vector3(-30, 0, 700), "long");
                    EnemySpawner(new Vector3(30, 0, 700), "long");
                    break;
                case 2:
                    EnemySpawner(new Vector3(-440, 0, 300), "close");
                    EnemySpawner(new Vector3(-420, 0, 320), "close");
                    EnemySpawner(new Vector3(-400, 0, 310), "close");
                    EnemySpawner(new Vector3(-380, 0, 280), "close");
                    EnemySpawner(new Vector3(-440, 0, 200), "long");
                    EnemySpawner(new Vector3(-460, 0, 250), "support");
                    break;
                case 3:
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    EnemySpawner(new Vector3(Random.Range(600, 800), 0, Random.Range(400, 600)), "close");
                    break;
                case 4:
                    EnemySpawner(new Vector3(0, 0, 1500), "boss");
                    break;

            }
            waveNumber++;
            isBusy = false;
        }
    }
    private void EnemySpawner(Vector3 coords, string enemyType)
    {
        Color color = Color.white;
        switch (enemyType)
        {
            case "close":
                enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
                color = Color.red;
                break;
            case "long":
                enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
                enemy.transform.localScale *= 2;
                color = Color.cyan;
                break;
            case "support":
                enemy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                enemy.transform.localScale *= 2;
                color = Color.yellow;
                break;
            case "boss":
                enemy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                enemy.transform.localScale *= 10;
                color = Color.white;
                break;
        }
        enemy.tag = "Enemy";
        enemy.transform.position = coords;
        enemy.AddComponent<Rigidbody>();
        enemy.GetComponent<Rigidbody>().isKinematic = true;
        enemy.GetComponent<Rigidbody>().useGravity = false;
        Instantiate(hp_bar, enemy.transform);
        enemy.AddComponent<Enemy>();
        enemy.GetComponent<Renderer>().material.mainTexture = Resources.Load("Assets/Materials/Target.mat") as Texture2D;
        enemy.GetComponent<MeshRenderer>().material.color = color;
        enemy.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color * 1.5f);
    }
    private void EndGame()
    {
        Debug.Log("You Win! End game!");
    }
}
