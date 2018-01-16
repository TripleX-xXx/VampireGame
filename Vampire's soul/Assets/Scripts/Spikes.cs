using UnityEngine;

public class Spikes : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Person>().TakeDmg(20);
    }

}
