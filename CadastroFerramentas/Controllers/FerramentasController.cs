using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroFerramentas.DAO;
using CadastroFerramentas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CadastroFerramentas.Controllers
{
    public class FerramentasController : Controller
    {
        public IActionResult Index()
        //Esse método vai ser mostrar a lista na página Index
        {
            FerramentasDAO dao = new FerramentasDAO();
            List<FerramentasViewModel> lista = dao.Listagem();

            return View(lista);
        }

        public IActionResult Create(int id)
        {
            ViewBag.Operacao = "I";
            FerramentasViewModel ferramentas = new FerramentasViewModel();

            FerramentasDAO dao = new FerramentasDAO();
            ferramentas.Id = dao.ProximoId();
            PreparaListaFabricanteParaCombo();
            return View("Form", ferramentas);
        }

   
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                FerramentasDAO dao = new FerramentasDAO();
                FerramentasViewModel ferramentas = dao.Consulta(id);
                PreparaListaFabricanteParaCombo();
                if (ferramentas == null)
                    return RedirectToAction("index");
                else
                    return View("Form", ferramentas);
                

            }

            catch
            {
                //posteriormente iremos redirecionar para uma tela de erro
                return RedirectToAction("index");
            }
        }

        public IActionResult Salvar(FerramentasViewModel ferramentas, string Operacao)
        {
            
            try
            {
                ValidaDados(ferramentas, Operacao);
                if(ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreparaListaFabricanteParaCombo();
                    return View("Form", ferramentas);
                }

                else
                {
                    FerramentasDAO dao = new FerramentasDAO();
                    if (Operacao == "I")
                        dao.Inserir(ferramentas);
                    else
                        dao.Alterar(ferramentas);

                    return RedirectToAction("index");
                }
            }

            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operacao;
                PreparaListaFabricanteParaCombo();
                return View("Form", ferramentas);
            }
        }

        public IActionResult Delete (int id)
        {
            try
            {
                FerramentasDAO dao = new FerramentasDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }

            catch
            {
                //posteriormente iremos redirecionar para uma tela de erro
                return RedirectToAction("index");
            }
        }

        private void ValidaDados(FerramentasViewModel ferramentas, string operacao)
        {
            FerramentasDAO dao = new FerramentasDAO();
            
            if (string.IsNullOrEmpty(ferramentas.Descricao))
                ModelState.AddModelError("Descricao", "Preencha uma descrição válida!");
                        
            
            if (!dao.ConsultaFabricante(ferramentas.FabricanteId))
                ModelState.AddModelError("FabricanteId", "O fabricante não é valido!");
        }

        private void PreparaListaFabricanteParaCombo()
        {
            FabricanteDAO dao = new FabricanteDAO();
            var fabricantes = dao.ListaFabricante();
            List<SelectListItem> listaFabricante = new List<SelectListItem>();
            listaFabricante.Add(new SelectListItem("Selecione um fabricante...", "0"));
            foreach (var fabricante in fabricantes)
            {
                SelectListItem item = new SelectListItem(fabricante.Descricao, fabricante.Id.ToString());
                listaFabricante.Add(item);
            }
            ViewBag.Fabricantes = listaFabricante;
        }
    }
}