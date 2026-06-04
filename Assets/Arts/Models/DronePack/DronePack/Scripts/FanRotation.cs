using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField]
    private float startRotationSpeed = 100f;
    [SerializeField]
    private float increaseSpeedOffsetTime = 0.5f; 
    [SerializeField]
    private float speedStep = 20f;             
    [SerializeField]
    private float maxRotationSpeed = 500f;

    private float currentRotationSpeed;
    private float timer;

    void Start()
    {
        currentRotationSpeed = startRotationSpeed;
        timer = 0f;
    }

    void Update()
    {
        if (!isActive) return;
        if (currentRotationSpeed < maxRotationSpeed)
        {
            timer += Time.deltaTime; 

            if (timer >= increaseSpeedOffsetTime)
            {
                currentRotationSpeed += speedStep; 

                currentRotationSpeed = Mathf.Min(currentRotationSpeed, maxRotationSpeed);

                timer = 0f; 
            }
        }

        this.transform.Rotate(Vector3.forward, currentRotationSpeed * Time.deltaTime);
    }
}
