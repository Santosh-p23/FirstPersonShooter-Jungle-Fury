using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public static Enemy_Manager instance;
    [SerializeField]
    private GameObject boar_Prefab, cannibal_Prefab;

    public Transform[] cannibal_SpawnPoints, boar_SpawnPoints;

    [SerializeField]
    private int cannibal_Count, boar_Count;

    private int cannibal_initial_count, boar_initial_count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        cannibal_initial_count = cannibal_Count;
        boar_initial_count = boar_Count;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }
    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        SpawnCannibals();
        SpawnBoars();
    }

    void SpawnCannibals()
    {
        int index = 0;
      
        for(int i =0; i< cannibal_Count; i++)
        {
            if (index >= cannibal_SpawnPoints.Length)
            {
                index = 0;
            }

           

            Instantiate(cannibal_Prefab, cannibal_SpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        cannibal_Count = 0;
        
    }

    void SpawnBoars()
    {
        int index = 0;

        for (int i = 0; i < boar_Count; i++)
        {
            if (index >=  boar_SpawnPoints.Length)
            {
                index = 0;
            }



            Instantiate(boar_Prefab, boar_SpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        boar_Count = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);
        SpawnBoars();
        SpawnCannibals();

        StartCoroutine("CheckToSpawnEnemies");
    }
    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            cannibal_Count++;
            if(cannibal_Count> cannibal_initial_count)
            {
                cannibal_Count = cannibal_initial_count;
            }
        }

        else{

            boar_Count++;
            if(boar_Count > boar_initial_count)
            {
                boar_Count = boar_initial_count;
            }
        }
    }

    public void StopSpawnning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
