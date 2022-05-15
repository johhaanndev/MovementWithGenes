using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain : MonoBehaviour
{
    public int DNAlength = 1;
    public float timeAlive;
    public float distanceRun;
    [HideInInspector] public DNA dna;

    private ThirdPersonCharacter m_Character;
    private Vector3 m_Move;
    private bool m_Jump;
    private bool alive = true;
    private Vector3 initialPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("dead"))
        {
            alive = false;
        }
    }

    public void Init()
    {
        // 0 forward
        // 1 back
        // 2 left
        // 3 right
        // 4 jump
        // 5 crouch
        initialPos = transform.position;
        dna = new DNA(DNAlength, 6);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

    private void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        bool crouch = false;
        if (dna.GetGene(0) == 0)
            v = 1;
        else if (dna.GetGene(0) == 1)
            v = -1;
        else if (dna.GetGene(0) == 2)
            h = -1;
        else if (dna.GetGene(0) == 3)
            h = 1;
        else if (dna.GetGene(0) == 4)
            m_Jump = true;
        else if (dna.GetGene(0) == 5)
            crouch = true;

        m_Move = v * Vector3.forward + h * Vector3.right;
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
        if (alive)
            distanceRun = Vector3.Distance(transform.position, initialPos);
    }
}
