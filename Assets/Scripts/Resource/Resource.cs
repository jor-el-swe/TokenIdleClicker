using UnityEngine;

namespace Resource {
     [CreateAssetMenu (fileName = "Resource", menuName = "ScriptableObjects/Resource")]
     public class Resource : ScriptableObject {
          public Color resourceColor;
          public ulong CurrentAmount {
               get {
                    var currentValueString = PlayerPrefs.GetString (this.name + "_string", "0");
                    var currentAmount = System.Convert.ToUInt64 (currentValueString);
                    return currentAmount;
                    //return PlayerPrefs.GetInt(this.name, 0);
               }
               set {
                    var currentValueString = value.ToString ();

                    PlayerPrefs.SetString (this.name + "_string", currentValueString);

                    //PlayerPrefs.SetInt(this.name, value);
               }
          }
     }
}