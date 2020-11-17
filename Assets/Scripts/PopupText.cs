using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour {
    public Vector3 moveDirection = Vector3.up * 20f;
    public float variation = 5f;
    public float alphaFadeSpeed = 5f;

    void Start() {
        variation = Random.Range(-variation, variation);
        transform.position += new Vector3(Random.Range(-variation, variation), 0f, 0f);
        moveDirection.x += this.variation;
    }
	
    void LateUpdate() {
        transform.position += moveDirection * Time.deltaTime;
        var text = GetComponent<Text>();
        var color = text.color;
        color.a -= alphaFadeSpeed * Time.deltaTime;
        text.color = color;
        if (color.a <= 0f) {
            Destroy(gameObject);
        }
    }
}