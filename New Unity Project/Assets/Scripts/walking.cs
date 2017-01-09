using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {

    public Animator anim;
    public float vert;
    public float hori;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = true;
	}

	// Update is called once per frame
	void Update () {
        vert = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");
        anim.SetFloat("walk", vert);
        anim.SetFloat("turn", hori);
	}
}
