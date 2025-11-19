using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    [Header("Movimiento")]
    private float movimientoX = 0f; 
    public float velocidad = 2f;
    private Rigidbody2D rb2d;

    [Header("Salto")]
    public float fuerzaSalto = 5f;

    [Header("Comprobaci n de suelo")]
    private bool estaEnSuelo;
    public LayerMask Suelo;
    public Transform CompruebaSuelo; 
    private float radioEsferaTocaSuelo = 0.1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        transform.localScale = new Vector3(1,1,1);
    }

    void Update()
    {
        rb2d.linearVelocity = new Vector2(movimientoX * velocidad, rb2d.linearVelocity.y);

        Debug.Log("Posicion del jugador: " + transform.position);
    }

    private void FixedUpdate()
    {
        if (CompruebaSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(CompruebaSuelo.position, radioEsferaTocaSuelo, Suelo);
        }
    }

    private void OnMove(InputValue inputMovimiento)
    {
        Vector2 input = inputMovimiento.Get<Vector2>();
        movimientoX = input.x;

        if (Mathf.Abs(movimientoX) > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Sign(movimientoX), 1, 1);
        }
    }

    private void OnJump(InputValue inputSalto)
    {
        if (estaEnSuelo)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, fuerzaSalto);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (CompruebaSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CompruebaSuelo.position, radioEsferaTocaSuelo);
        }
    }
}
