using System.Web.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            if (id != 0)
            {
                Person person = new Person(id);
                ViewData.Model = person;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(Person person, string action)
        {

            if (!string.IsNullOrEmpty(ValidateData(person, action)))
            {
                ViewData["Error"] = ValidateData(person, action);
                return View();
            }

            switch (action)
            {
                case "search":
                    ViewData.Model = new Person().GetPerson(person.Id, person.Email);
                    return View("Search");
                    
                case "add":

                    if(new Person(person.Id).Id == 0)
                    {
                        bool addPerson = new Person().AddPerson(person);

                        if (!addPerson) ViewData["Error"] = "No se ha podido agregar el elemento";
                        else ViewData["Message"] = "Todo ha ido bien, se ha agregado el elemento :D";
                    }
                    else
                    {
                        bool updatePerson = new Person().UpdatePerson(person);

                        if (!updatePerson) ViewData["Error"] = "No se ha podido actualizar el elemento";
                        else ViewData["Message"] = "Todo ha ido bien, se ha actualizado :D";
                    }

                    break;
                case "delete":

                    bool deletePerson = new Person().DeletePerson(person.Id);

                    if (!deletePerson) ViewData["Error"] = "No se ha podido eliminar el elemento";
                    else ViewData["Message"] = "Se ha eliminado el elemento :D";

                    break;
            }

            return View();
        }

        /// <summary>
        /// Validates the data.
        /// </summary>
        /// <returns><c>true</c>, if data was validated, <c>false</c> otherwise.</returns>
        /// <param name="person">Person.</param>
        private string ValidateData(Person person, string action)
        {
            if (action == "delete" && person.Id == 0)
            {
                return "Necesita por lo menos el ID para eliminar un elemento";
            }

            if((action == "add") && person.Id == 0 && string.IsNullOrEmpty(person.Email))
            {
                return "Necesita por lo menos el id y el email para guardar o actualizar";
            }



            return null;
        }
    }
}
