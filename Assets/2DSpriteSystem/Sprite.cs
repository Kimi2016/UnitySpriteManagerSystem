using UnityEngine;
using System.Collections;

public class Sprite: MonoBehaviour 
{

    private Texture2D _spriteTexture; //Inst�ncia da textura atual do Sprite
    private SpriteSheetInfo _spriteInfo; //Informa��es do atlas do SpriteSheet
    private float _interval; //Intervalo entre um frame e outro (v�lido durante a anima��o)
    private bool _isPlaying; //O sprite est� executando uma anima��o?

    //Propridades para habilitar leitura e/ou escritura nas vari�veis anteriores

    public Texture2D SpriteTexture
    {

        get { return _spriteTexture; }
    
    }

    public SpriteSheetInfo SpriteInfo
    {

        get { return _spriteInfo; }
    
    }

    public float Interval
    {

        get { return _interval;  }
        set { _interval = value; }
    
    }

    public bool IsPlaying
    {

        get { return _isPlaying; }
    
    }

    // Enumerador que cont�m os modos de execu��o poss�veis para o Sprite
    public enum PlayMode
    {
    
        Loop,
        Once,
        PingPong
    
    }

    public void Create (SpriteSheetInfo SpriteInfo) // Fun��o usada como construtor, para inicializar os campos do Sprite
    {
        
        if (IsPlaying) //Se o sprite j� estiver executando anima��o
            Stop(); //Interrompe a mesma
        _spriteInfo = SpriteInfo;
        renderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        _spriteTexture = _spriteInfo.GetTexture(); //Busca a textura referente a este atlas na pasta Resources
        
    }

    public void Play (PlayMode playMode, float intervalBetweenFrames, bool forceStop = true) // Executa a anima��o do atlas atual
    {

        _interval = intervalBetweenFrames; // Seta o intervalo de transi��o entre frames

        if (!forceStop)
        {

            switch (playMode) // Switch entre os modos de execu��o existentes para escolher a corotina a ser iniciada
            {
                case PlayMode.Loop:
                    StartCoroutine(Loop());
                    break;

                case PlayMode.Once:
                    StartCoroutine(Once());
                    break;

                case PlayMode.PingPong:
                    StartCoroutine(PingPong());
                    break;

            }
        }
        else // Se a op��o 'forceStop' for marcada, interrompe a anima��o e a executa novamente com a op��o desabilitada (para n�o cair em loop infinito)
        {
            Stop();
            Play (playMode, intervalBetweenFrames, false);
        }
    }


    // Corotina para a execu��o do sprite no modo Loop
    // Os outros modos de execu��o possuem corotinas muito semelhantes entre si, por isso s� essa ser� detalhada
    private IEnumerator Loop()
    {

        //Busca a lista de nomes no atlas, seta a textura do material e habilita a op��o '_isPlaying'
        string[] spriteNames = _spriteInfo.GetSpriteNames();
        renderer.material.mainTexture = _spriteTexture;
        _isPlaying = true;

        while (true) // Loop infinito
        {
            for (int i = 0; i < spriteNames.Length; i++) //Para cada sprite no sprite sheet (por ordem)
            {

                SpriteRect rect = _spriteInfo.GetSprite(spriteNames[i]); // Pega o ret�ngulo equivalente
                Vector2 imageDimensions = new Vector2(_spriteTexture.width, _spriteTexture.height); // Armazena a dimens�es do atlas
                renderer.material.mainTextureOffset = rect.GetOffset(imageDimensions); // Seta o offset do material, de acordo com o ret�ngulo atual
                renderer.material.mainTextureScale = rect.GetScale(imageDimensions); // Seta a escala do material, de acordo com o ret�ngulo atual
                yield return new WaitForSeconds(_interval); // Delay entre frames
            
            }
        }
    }

    
    private IEnumerator Once()
    {

        string[] spriteNames = _spriteInfo.GetSpriteNames();
        renderer.material.mainTexture = _spriteTexture;
        _isPlaying = true;

        for (int i = 0; i < spriteNames.Length; i++)
        {

            SpriteRect rect = _spriteInfo.GetSprite(spriteNames[i]);
            Vector2 imageDimensions = new Vector2(_spriteTexture.width, _spriteTexture.height);
            renderer.material.mainTextureOffset = rect.GetOffset(imageDimensions);
            renderer.material.mainTextureScale = rect.GetScale(imageDimensions);
            yield return new WaitForSeconds(_interval);

        }

        Stop();

    }

    private IEnumerator PingPong()
    {

        string[] spriteNames = _spriteInfo.GetSpriteNames();
        renderer.material.mainTexture = _spriteTexture;
        _isPlaying = true;

        while (true)
        {

            for (int i = -(spriteNames.Length - 1); i < spriteNames.Length - 1; i++)
            {

                SpriteRect rect = _spriteInfo.GetSprite(spriteNames[Mathf.Abs(i)]);
                Vector2 imageDimensions = new Vector2(_spriteTexture.width, _spriteTexture.height);
                renderer.material.mainTextureOffset = rect.GetOffset(imageDimensions);
                renderer.material.mainTextureScale = rect.GetScale(imageDimensions);
                yield return new WaitForSeconds(_interval);

            }
        }
    }

    public void SetSprite(string name)
    {

        SpriteRect rect = _spriteInfo.GetSprite(name);
        Vector2 imgDimensions = new Vector2(_spriteTexture.width, _spriteTexture.height);
        renderer.material.mainTextureOffset = rect.GetOffset(imgDimensions);
        renderer.material.mainTextureScale = rect.GetScale(imgDimensions);

    }

    public void Stop() //Interrompe a execu��o de uma anima��o
    {

        _isPlaying = false; 
        StopAllCoroutines(); // P�ra todas as corotinas deste MonoBehaviour

    }
}
