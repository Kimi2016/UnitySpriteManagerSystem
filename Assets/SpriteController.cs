using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour {

    private Sprite sprite;
    public float intervalBetweenFrames;

    void Start()
    {

        sprite = GetComponent<Sprite>();
        sprite.Create(SpriteSet.GetSprite("SpriteDemo"));
        sprite.Play(Sprite.PlayMode.PingPong, 0.1f);
    
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            sprite.Stop();
            sprite.SetSprite("1");
        }
    }
}
