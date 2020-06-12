using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;
using WebMvc.Controllers;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ManageContactController : BaseController
    {
        // GET: Admin/Manage
        //form create and display content of date contact if have
        public ActionResult Index()
        {
            ContactDTO dataContact = null;
            string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
            if (System.IO.File.Exists(path))
            {
                Stream streamD = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter formatterD = new BinaryFormatter();
                dataContact = formatterD.Deserialize(streamD) as ContactDTO;
                streamD.Close();
            }

            return View(dataContact);
        }
        //method insert data contact
        public ActionResult InsertContact(ContactDTO cv)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
                //Serialize
                using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cv);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}