using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{

	public GameObject player;
	SpriteRenderer m_sprite;
	private Image image;


	void Awake()
	{
		image = GetComponent<Image>();
	}


    void Update()
	{
		player = Player.instance.m_Controller.gameObject;
		m_sprite = player.GetComponent<SpriteRenderer>();
		image = GetComponent<Image>();
		image.sprite = m_sprite.sprite;
	}
}