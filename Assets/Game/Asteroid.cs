using KS.Actor.Movement;
using KS.Common;
using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    private float rotationSpeed;
    private float startDifficultyRatio = 1f;

    private void OnEnable()
    {
        // when OnEnable called if difficultyRatio changed, speed will be updated for all asteroids
        float currentDifficultyRatio = GameController.instance.GetComponent<LevelController>().DifficultyRatio;
        Debug.Log("startDifficultyRatio: " + startDifficultyRatio + " currentDifficultyRatio: " + currentDifficultyRatio);
        if (startDifficultyRatio != currentDifficultyRatio)
        {
            this.gameObject.GetComponent<MovementSystem>().SetSpeed(currentDifficultyRatio);
            //startDifficultyRatio = currentDifficultyRatio;
            Debug.Log(gameObject.name + " objesinin hızı arttırıldı: " + this.gameObject.GetComponent<MovementSystem>().Speed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startDifficultyRatio = GameController.instance.GetComponent<LevelController>().DifficultyRatio; // this will be starting value of difficultyRatio and this value won't change
        rotationSpeed = Random.Range(-50, 50);
        this.gameObject.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }



}
