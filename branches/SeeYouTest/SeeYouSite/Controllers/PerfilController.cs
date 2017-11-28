using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeeYouSite.Models;
using SeeYouSite.ViewModels;

namespace SeeYouSite.Controllers
{ 
    public class PerfilController : Controller
    {
        private SYRContext syrDb = new SYRContext();

        //
        // GET: /Perfil/

        public ViewResult Index()
        {
            ViewBag.Title = "SYR - Administração";

            var usuarios = syrDb.usuarios;
            ViewBag.ListaUsuarios = new SelectList(syrDb.usuarios, "id_usuario", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult listaPercursos(int id_usuario)
        {
            var percurso = syrDb.percursos.Where(p => p.id_usuario == id_usuario);

            return PartialView("TabelaPercursosPartial", percurso.ToList());
        }
        
        //
        // GET: /Perfil/Edit/5

        public ActionResult Edit(long id_percurso)
        {

            ViewBag.Title = "SYR - Edição";

            percursos percursos = syrDb.percursos.Find(id_percurso);

            return View(percursos);
        }

        //
        // POST: /Perfil/Edit/5

        [HttpPost]
        public ActionResult Edit(percursos percursos)
        {
            if (ModelState.IsValid)
            {
                syrDb.Entry(percursos).State = EntityState.Modified;
                syrDb.SaveChanges();
                return RedirectToAction("Index");
            }
           return View(percursos);
        }

        //
        // GET: /Perfil/Delete/5

        public ActionResult Delete(int id_percurso)
        {
            ViewBag.Title = "SYR - Deletar";

            percursos percursos = syrDb.percursos.Find(id_percurso);
            return View(percursos);
        }

        //
        // POST: /Perfil/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id_percurso)
        {            
            percursos percursos = syrDb.percursos.Find(id_percurso);
            syrDb.percursos.Remove(percursos);
            syrDb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            syrDb.Dispose();
            base.Dispose(disposing);
        }
    }
}