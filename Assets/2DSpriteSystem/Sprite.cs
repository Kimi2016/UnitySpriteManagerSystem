using UnityEngine;
using System.Collections;

public class Sprite: MonoBehaviour 
{

    private Texture2D _spriteTexture; //Instância da textura atual do Sprite
    private SpriteSheetInfo _spriteInfo; //Informações do atlas do SpriteSheet
    private float _interval; //Intervalo entre um frame e outro (válido durante a animação)
    private bool _isPlaying; //O sprite está executando uma animação?

    //Propridades para habilitar leitura e/ou escritura nas variáveis anteriores

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

    // Enumerador que contém os modos de execução possíveis para o Sprite
    public enum PlayMode
    {
    
        Loop,
        Once,
        PingPong
    
    }

    public void Create (SpriteSheetInfo SpriteInfo) // Função usada como construtor, para inicializar os campos do Sprite
    {
        
        if (IsPlaying) //Se o sprite já estiver executando animação
            Stop(); //Interrompe a mesma
        _spriteInfo = SpriteInfo;
        renderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        _spriteTexture = _spriteInfo.GetTexture(); //Busca a textura referente a este atlas na pasta Resources
        
    }

    public void Play (PlayMode playMode, float intervalBetweenFrames, bool forceStop = true) // Executa a animação do atlas atual
    {

        _interval = intervalBetweenFrames; // Seta o intervalo de transição entre frames

        if (!forceStop)
        {

            switch (playMode) // Switch entre os modos de execução existentes para escolher a corotina a ser iniciada
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
        else // Se a opção 'forceStop' for marcada, interrompe a animação e a executa novamente com a opção desabilitada (para não cair em loop infinito)
        {
            Stop();
            Play (playMode, intervalBetweenFrames, false);
        }
    }


    // Corotina para a execução do sprite no modo Loop
    // Os outros modos de execução possuem corotinas muito semelhantes entre si, por isso só essa será detalhada
    private IEnumerator Loop()
    {

        //Busca a lista de nomes no atlas, seta a textura do material e habilita a opção '_isPlaying'
        string[] spriteNames = _spriteInfo.GetSpriteNames();
        renderer.material.mainTexture = _spriteTexture;
        _isPlaying = true;

        while (true) // Loop infinito
        {
            for (int i = 0; i < spriteNames.Length; i++) //Para cada sprite no sprite sheet (por ordem)
            {

                SpriteRect rect = _spriteInfo.GetSprite(spriteNames[i]); // Pega o retângulo equivalente
                Vector2 imageDimensions = new Vector2(_spriteTexture.width, _spriteTexture.height); // Armazena a dimensões do atlas
                renderer.material.mainTextureOffset = rect.GetOffset(imageDimensions); // Seta o offset do material, de acordo com o retângulo atual
                renderer.material.mainTextureScale = rect.GetScale(imageDimensions); // Seta a escala do material, de acordo com o retângulo atual
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

    public void Stop() //Interrompe a execução de uma animação
    {

        _isPlaying = false; 
        StopAllCoroutines(); // Pára todas as corotinas deste MonoBehaviour

    }
}
