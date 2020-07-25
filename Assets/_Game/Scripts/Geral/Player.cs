using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public Animator p_animator;
    private bool idle = true;

    public static Player instance;

	void Start () {
        if (instance == null)
            instance = this;

        p_animator = GetComponent<Animator>();
        StartCoroutine(AnimAction());
    }
	
    private IEnumerator AnimAction()
    {
        while (true)
        {
            float t = Random.Range(4, 6);
            yield return new WaitForSeconds(t);
            p_animator.SetTrigger("action");
        }
    }

    public void OnItemFoodEnter()
    {
        p_animator.SetBool("food-aceppt", true);
    }

    public void OnItemFoodExit()
    {
        p_animator.SetBool("food-aceppt", false);
    }

    public void OnItemFoodUp(Comida comida)
    {
        p_animator.SetTrigger("eat");
        foreach (Gain g in comida.gains)
            GameController.instance.AdicionarMedidor(g.gainType, g.value);

        LogAcao LogAcao = new LogAcao(
            "Comer",
            "Comeu: "+ comida.name,
            System.DateTime.Now,
            Medidores.Alimentacao,
            comida.bom
        );

        SaveGameController.Instance.AddComportamento(ComportamentosType.acao, LogAcao);


        this.ActionAfter(0.5f, () =>
        {
            p_animator.SetBool("food-aceppt", false);
        });
    }

    public void OnItemMedicamentoUp(MedicamentoObject medicamento)
    {
       GameController.instance.AdicionarMedidor(Medidores.Saude, medicamento.Quantidade);

        LogAcao LogAcao = new LogAcao(
            "Tomar remedio",
            "Tomou: " + medicamento.Nome,
            System.DateTime.Now,
            Medidores.Saude,
            true
        );

        SaveGameController.Instance.AddComportamento(ComportamentosType.acao, LogAcao);
        p_animator.SetBool("food-aceppt", false);
    }

}
