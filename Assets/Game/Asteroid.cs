using KS.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject explosion;

    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(-50, 50);
        this.gameObject.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
        EventHandler.instance.OnDeath += OnAsteroidDeath;
    }
   
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnAsteroidDeath(GameObject gameObject)
    {
        if (this.gameObject == gameObject)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            GameUIController.Instance.SetScore(GameUIController.Instance.currentScore + 1);
        }
    }

}
