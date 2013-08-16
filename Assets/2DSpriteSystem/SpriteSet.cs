using UnityEngine;
using System.Collections.Generic;

public class SpriteSet : MonoBehaviour 
{

    public TextAsset spriteData;
    private static List<SpriteSheetInfo> sprites;

    void Awake()
    {

        sprites = XMLParser.Parse(spriteData);

    }

    public static SpriteSheetInfo GetSprite(string name)
    {

        foreach (SpriteSheetInfo sprite in sprites)
        {

            if (sprite.SpriteName == name)
            {

                return sprite;
            
            }
        }

        return null;

    }
}
