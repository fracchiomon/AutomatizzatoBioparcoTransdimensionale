<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour {

    public Rigidbody rig;
    public float speed=100f;

    void Start () {
		
	}
	

	void Update () {
        rig.AddForce(transform.forward* speed);
	}
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour {

    public Rigidbody rig;
    public float speed=100f;

    void Start () {
		
	}
	

	void Update () {
        rig.AddForce(transform.forward* speed);
	}
}
>>>>>>> f10deb0c (merge di arianna)
