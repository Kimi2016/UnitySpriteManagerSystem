using UnityEngine;
using System.Collections.Generic;

public class SpriteSheetInfo
{

    private Dictionary<string, SpriteRect> _spriteInfo; // Declara uma inst�ncia de um dicion�rio, cuja chave � uma string e o valor � do tipo SpriteRect
    private string _spriteName; // String que guarda o nome do sprite (deve ser igual ao nome da textura)

    public Dictionary<string, SpriteRect> SpriteInfo //Propriedade para habilitar leitura na vari�vel '_spriteInfo'
    {

        get { return _spriteInfo; }
    
    }

    public string SpriteName //Propriedade para habilitar leitura na vari�vel '_spriteName'
    {

        get { return _spriteName; }
    
    }

    public SpriteSheetInfo (Dictionary<string, SpriteRect> SpriteInfo, string SpriteName) //Construtor da classe
    {

        _spriteInfo = SpriteInfo;
        _spriteName = SpriteName;
    
    }

    public SpriteRect GetSprite (string name) //Busca um sprite por nome, usando-o como chave para o dicion�rio '_spriteInfo'. Retorna um SpriteRect
    {

        return _spriteInfo[name];
    
    }

    public string[] GetSpriteNames () //Retorna um vetor com os nomes de todos os sprites do atlas (por ordem)
    {

        string[] names = new string[_spriteInfo.Count]; //Declara a vari�vel que vai conter os nomes
        _spriteInfo.Keys.CopyTo(names, 0); //Copia as chaves do dicion�rio para a vari�vel rec�m-declarada
        return names; //Retorna o vetor

    }

    public Texture2D GetTexture () //Retorna a textura associada � esse SpriteSheet
    {

        Texture2D texture = (Texture2D)Resources.Load(_spriteName); //Carrega uma textura da pasta 'Resources', cujo nome � o valor da vari�vel '_spriteName'
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
