using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int velocidade = 10;
    public int forcaPulo = 7;
    public bool noChao;
    private Rigidbody rb;
    private AudioSource source;
    public AudioClip clipPulo, clipMoeda;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out source);
    }

    void OnCollisionEnter(Collision col) 
    {
        if(col.gameObject.tag == "Chão"){
            noChao = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direcao = new Vector3(h, 0, v);
        rb.AddForce(direcao * velocidade * Time.deltaTime,ForceMode.Impulse);

        //Pular
        if(Input.GetKeyDown(KeyCode.Space) && noChao) //se apertou espaço
        {
            source.PlayOneShot(clipPulo);
            //Aplica força pra cima
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            noChao = false;
        }
            //se o jogar caiu
        if(transform.position.y <= -10){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
