using UnityEngine;
using System;

public class BaseProjectile : MonoBehaviour
{
    private MeshFilter m_mesh;
    private float m_speed = 1f;
    private float m_dmg;
    private float m_range;
    private float m_impactRadius;

    private Rigidbody rb;
    [SerializeField]
    private GameObject impact;

    private void Awake()
    {
        m_mesh = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SetupValue
    public void SetValues(Mesh p_mesh, float p_speed, float p_dmg, float p_range, float p_impactRadius)
    {
        m_mesh.mesh = p_mesh;
        m_speed = p_speed;
        m_dmg = p_dmg;
        m_range = p_range;
        m_impactRadius = p_impactRadius;
    }

    //move forward
    private void Move()
    {
        // move forward at m_speed speed
        rb.linearVelocity = transform.forward * m_speed;
    }

    //collision
    private void OnCollisionEnter(Collision collision)
    {
        

        Destroy(this);
    }

    //death
    private void OnDestroy()
    {
        //instantiate radius impact collider
        GameObject l_impact = Instantiate(impact);
        l_impact.GetComponent<ImpactEffect>().SetValue(m_impactRadius, m_dmg);
    }
}
