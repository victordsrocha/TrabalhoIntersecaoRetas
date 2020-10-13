using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public LineRenderer linha1;
    public LineRenderer linha2;
    public SpriteRenderer pontoIntersecao;
    public Button botaoResetar;
    public Text textoIntersecao;

    private int _pontoAtual;
    private Vector3 _mousePos;

    // Start is called before the first frame update
    void Start()
    {
        InicializacaoUI();
        Resetar();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pontoAtual < 4)
        {
            CriarLinhas();
        }
        else
        {
            CalculaIntersecao();
        }
    }

    private float ProdutoEscalar(Vector2 u, Vector2 v)
    {
        return u.x * v.x + u.y * v.y;
    }

    private float CalculaT(Vector2 p1, Vector2 p2, Vector2 q, Vector2 n)
    {
        return ProdutoEscalar((q - p1), n) / ProdutoEscalar((p2 - p1), n);
    }


    private void CalculaIntersecao()
    {
        var pontoA = linha1.GetPosition(0);
        var pontoB = linha1.GetPosition(1);
        var pontoC = linha2.GetPosition(0);
        var pontoD = linha2.GetPosition(1);

        var delta = pontoD - pontoC;
        var n = new Vector2(-delta.y, delta.x);
        var t = CalculaT(pontoA, pontoB, pontoC, n);

        if (t >= 0 && t <= 1)
        {
            // vec2 do ponto de intersecao
            var ponto = pontoA + t * (pontoB - pontoA);

            // mostrar na tela
            pontoIntersecao.gameObject.SetActive(true);
            pontoIntersecao.transform.position = new Vector2(ponto.x, ponto.y);
            textoIntersecao.text = "x = " + ponto.x.ToString("F") + " ,y = " + ponto.y.ToString("F");
        }
        else
        {
            textoIntersecao.text = "Não houve intersecao";
        }
    }

    private void CriarLinhas()
    {
        if (!(Camera.main is null)) _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch (_pontoAtual)
        {
            case 0:
            {
                linha1.SetPosition(0, (Vector2) _mousePos);
                linha1.SetPosition(1, (Vector2) _mousePos);
                if (Input.GetMouseButtonUp(0))
                {
                    _pontoAtual++;
                }

                break;
            }
            case 1:
            {
                linha1.SetPosition(1, (Vector2) _mousePos);
                if (Input.GetMouseButtonUp(0))
                {
                    _pontoAtual++;
                }

                break;
            }
            case 2:
            {
                linha2.SetPosition(0, (Vector2) _mousePos);
                linha2.SetPosition(1, (Vector2) _mousePos);
                if (Input.GetMouseButtonUp(0))
                {
                    _pontoAtual++;
                }

                break;
            }
            case 3:
            {
                linha2.SetPosition(1, (Vector2) _mousePos);
                if (Input.GetMouseButtonUp(0))
                {
                    _pontoAtual++;
                }

                break;
            }
        }
    }

    public void Resetar()
    {
        pontoIntersecao.gameObject.SetActive(false);
        _pontoAtual = 0;

        textoIntersecao.text = "";

        linha1.startColor = Color.magenta;
        linha1.endColor = Color.red;
        linha1.startWidth = 0.025f;
        linha1.SetPosition(0, Vector2.zero);
        linha1.SetPosition(1, Vector2.zero);

        linha2.startColor = Color.green;
        linha2.endColor = Color.yellow;
        linha2.startWidth = 0.025f;
        linha2.SetPosition(0, Vector2.zero);
        linha2.SetPosition(1, Vector2.zero);
    }

    private void InicializacaoUI()
    {
        botaoResetar.onClick = new Button.ButtonClickedEvent();
        botaoResetar.onClick.AddListener(Resetar);
    }
}