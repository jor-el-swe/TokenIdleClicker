using UnityEngine;
[CreateAssetMenu]
public class Resource : ScriptableObject {
     public int CurrentAmount {
          get => PlayerPrefs.GetInt(this.name, 0);
          set => PlayerPrefs.SetInt(this.name, value);
     }
     
}
