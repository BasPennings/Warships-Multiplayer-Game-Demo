using UnityEngine;

public class FloatingPoint : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float submergeDepth = 1F;
    [SerializeField] private float displacementAmount = 3F;
    [SerializeField] private int floatingPoints = 1;
    [SerializeField] private float waterDrag = 0.99F;
    [SerializeField] private float waterAngularDrag = 0.5F;

    private void FixedUpdate()
    {
        rigidbody.AddForceAtPosition(Physics.gravity / floatingPoints, transform.position, ForceMode.Acceleration);

        float waveHeight = WaveCollisionManager.instance.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / submergeDepth) * displacementAmount;
            rigidbody.AddForceAtPosition(new Vector3(0, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0),
                transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
