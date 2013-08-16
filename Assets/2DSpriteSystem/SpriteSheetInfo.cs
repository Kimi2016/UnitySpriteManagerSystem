using UnityEngine;
using System.Collections.Generic;

public class SpriteSheetInfo
{

    private Dictionary<string, SpriteRect> _spriteInfo; // Declara uma instância de um dicionário, cuja chave é uma string e o valor é do tipo SpriteRect
    private string _spriteName; // String que guarda o nome do sprite (deve ser igual ao nome da textura)

    public Dictionary<string, SpriteRect> SpriteInfo //Propriedade para habilitar leitura na variável '_spriteInfo'
    {

        get { return _spriteInfo; }
    
    }

    public string SpriteName //Propriedade para habilitar leitura na variável '_spriteName'
    {

        get { return _spriteName; }
    
    }

    public SpriteSheetInfo (Dictionary<string, SpriteRect> SpriteInfo, string SpriteName) //Construtor da classe
    {

        _spriteInfo = SpriteInfo;
        _spriteName = SpriteName;
    
    }

    public SpriteRect GetSprite (string name) //Busca um sprite por nome, usando-o como chave para o dicionário '_spriteInfo'. Retorna um SpriteRect
    {

        return _spriteInfo[name];
    
    }

    public string[] GetSpriteNames () //Retorna um vetor com os nomes de todos os sprites do atlas (por ordem)
    {

        string[] names = new string[_spriteInfo.Count]; //Declara a variável que vai conter os nomes
        _spriteInfo.Keys.CopyTo(names, 0); //Copia as chaves do dicionário para a variável recém-declarada
        return names; //Retorna o vetor

    }

    public Texture2D GetTexture () //Retorna a textura associada à esse SpriteSheet
    {

        Texture2D texture = (Texture2D)Resources.Load(_spriteName); //Carrega uma textura da pasta 'Resources', cujo nome é o valor da variável '_spriteName'
        return texture;

    }

    public override string ToString()
    {

        string _return = SpriteName + "\n";

        foreach (string s in _spriteInfo.Keys)
        {

            SpriteRect current = _spriteInfo[s];
            _return += string.Format("{0}-({1}, {2}, {3}, {4})\n", s, current.x, current.y, current.Width, current.Height);

        }

        return _return;

    }

}
