using UnityEngine;
using System.Collections;

public class SpriteRect
{

    private int _x, _y, _w, _h; //Variáveis referentes às coordenadas e dimensões do retângulo

    //Lista de propriedades para habilitar a leitura das variáveis privadas da classe

    public int x
    {

        get { return _x; }

    }

    public int y
    {

        get { return _y; }
    
    }

    public int Width
    {

        get { return _w; }

    }

    public int Height
    {

        get { return _h; }

    }

    public SpriteRect(int x, int y, int width, int height) //Construtor da classe (possibilita a atribuição de valores às variáveis privadas)
    {

        _x = x;
        _y = y;
        _w = width;
        _h = height;

    }

    public Vector2 GetScale(Vector2 imageDimensions) //Retorna a escala do material, para que o mesmo se encaixe perfeitamente a este retângulo
    {

        float x = _w / imageDimensions.x;
        float y = _h / imageDimensions.y;
        return new Vector2(x, y);

    }

    public Vector2 GetOffset(Vector2 imageDimensions) //Retorna o offset do material, para que o mesmo se encaixe perfeitamente a este retângulo
    {

        Vector2 scale = GetScale(imageDimensions);
        float x = _x / imageDimensions.x;
        float y = 1 - (_y / imageDimensions.y + scale.y);
        return new Vector2(x, y);
    
    }
}