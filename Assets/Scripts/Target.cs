using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int _pointValue;
    [SerializeField] private ParticleSystem _explosionParticles;

    private Rigidbody _targetRB;
    private float minSpeed = 13.0f;
    private float maxSpeed = 17.0f;
    private float maxTorque = 10.0f;
    private float xRange = 4.0f;
    private float ySpawnPos = -6.0f;

    private void Awake()
    {
        _targetRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && GameManager.instance.isGameActive)
        {
            GameManager.instance.UpdateLives(-1);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(_explosionParticles, transform.position, _explosionParticles.transform.rotation);
            GameManager.instance.UpdateScore(_pointValue);
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    public void DestroyTarget()
    {
        if (GameManager.instance.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(_explosionParticles, transform.position, _explosionParticles.transform.rotation);
            GameManager.instance.UpdateScore(_pointValue);
        }
    }
}
