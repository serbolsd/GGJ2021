using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

  public List<EnemyMovement> m_enemysInLevel;

  public GameObject prefUI;
  public UIPlayer m_UIPlayer = null;
  public Vida_Script m_livePlayer;
  public PauseMenu m_pauseMenu;
  public DeathMenu m_deathMenu;

  public PlayerMovement2 m_player;

  public List<ElementOfLevel> m_elements;

  public bool setFullLife = false;

  // Start is called before the first frame update
  void Start()
  {
    m_UIPlayer = FindObjectOfType<UIPlayer>();
    if (m_UIPlayer ==null)
    {
      m_UIPlayer = Instantiate(prefUI).GetComponent<UIPlayer>();
    }
    m_UIPlayer.GetComponent<Canvas>().worldCamera = Camera.main;
    m_livePlayer = m_UIPlayer.m_livePlayer;
    m_pauseMenu = m_UIPlayer.m_pauseMenu;
    m_deathMenu = m_UIPlayer.m_deathMenu;

    if (setFullLife)
    {
      m_livePlayer.VidaActual = m_livePlayer.VidaMaxima;
    }

    m_player = FindObjectOfType<PlayerMovement2>();
    m_player.onStart();

    var Enemys = FindObjectsOfType<EnemyMovement>();
    foreach (var Enemy in Enemys)
    {
      var move = Enemy.GetComponent<EnemyMovement>();
      m_enemysInLevel.Add(move);
      move.onStart();
    }

    var elements = FindObjectsOfType<ElementOfLevel>();
    foreach (var element in elements)
    {
      var comp = element.GetComponent<ElementOfLevel>();
      m_elements.Add(comp);
      comp.onStart();
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (m_pauseMenu.GameIsPause || m_livePlayer.m_died)
    {
      return;
    }
    m_player.onUpdate();
    foreach (var Enemy in m_enemysInLevel)
    {
      Enemy.onUpdate();
    }
    foreach (var element in m_elements)
    {
      element.onUpdate();
    }
  }
}
