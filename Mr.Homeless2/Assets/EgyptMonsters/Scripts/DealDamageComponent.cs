using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageComponent : MonoBehaviour {
    
    public float speed = 5f; // Hareket hızı
    public GameObject hitFX;

	void DealDamage() {
        transform.parent.GetComponent<DemoController>().DealDamage(this);
    }
    

    void Update()
    {
        // Objenin x yönünde hareket etmesini sağla
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


}
