using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    private float impactRadius;
    private float dmg;

    private SphereCollider collider;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float p_radius, float p_dmg)
    {
        collider.radius = p_radius;
        dmg = p_dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.transform.tag == "enemies")
        {
            Debug.Log($"Tien vilain !!! prend {dmg} dégats");
        }
    }
}
