using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danocoletavel : MonoBehaviour
{
    public int Damage;
    // Start is called before the first frame update
   private void OnTriggerStay (Collider other)
   {
    other.GetComponent<Barradevida>().ReceberDano(Damage);
   }
}
