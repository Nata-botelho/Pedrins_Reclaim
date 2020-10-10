using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;           //variavel publica que guarda o objeto do jogador principal
    public Vector3 offset;              //variavel que guarda o offset de distancia entre o jogador a camera

    void Start () 
    {
        //calcula o offset inicial (pos da camera - pos do jogador)
        offset = transform.position - player.transform.position;
    }

    // LateUpdate eh chamado em cada frame, apos a Update ser chamada
    void LateUpdate () 
    {
        //dirige a posicao da camera para a mesma do jogador, pos da camera = pos do jogador + offset
        transform.position = player.transform.position + offset;
    }
}