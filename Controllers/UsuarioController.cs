using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IATech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IATech.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            // criar uma referencia para o UsuarioModel

            UsuarioModel uModel = new UsuarioModel();

            // executar o metodo Listar()

            return View(uModel.Listar());
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        [HttpGet]
        public ActionResult Create()
        {
            // a view agora está relacionada ao modelo
            return View(new UsuarioModel());
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioModel uModel)
        {
            // uModel.Salvar();
            // return RedirectToAction("Index");

            if (ModelState.IsValid) // Testa se o Model (Anotação) é valido
            {
                uModel.Salvar();
                return RedirectToAction("Index");
            }
            else
            {
                return View(uModel);
            }
        }

        // este método apenas recupera o registro e mostra no formulário para alteração dos dados
        // GET: UsuarioController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id) // esse parâmetro é o ID do usuario ou UsuarioID
        {
            UsuarioModel uModel = new UsuarioModel();
            uModel.Editar(id);


            return View(uModel);
        }

        // este método salva no banco de dados os dados alterados
        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioModel uModel)
        {
            uModel.Atualizar();

            return RedirectToAction("Index");
        }
        // método realiza a exclusão do registro, passando os dados para o Model UsuairoModel
        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            UsuarioModel uModel = new UsuarioModel();

            uModel.Excluir(id);

            return RedirectToAction("Index");
        }
        // alterar este codigo para excluir em duas etapas, semelhante ao editar
        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
