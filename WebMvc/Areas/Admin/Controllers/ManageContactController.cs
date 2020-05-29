using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using WebMvc.Controllers;
using WebMvc.Models.ModelView;

namespace WebMvc.Areas.Admin.Controllers
{
    public class ManageContactController : BaseController
    {
        // GET: Admin/Manage
        public ActionResult Index()
        {
            ContactDTO dataContact = null;
            string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
            if (System.IO.File.Exists(path))
            {
                //Deserialize
                Stream streamD = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter formatterD = new BinaryFormatter();
                //quá trình Deserialize ngược với quá trình Serialize, trả về một object, bạn nhớ ép kiểu để sử dụng.
                dataContact = formatterD.Deserialize(streamD) as ContactDTO;
                streamD.Close();
            }
            
            return View(dataContact);
            }

        public ActionResult InsertContact(ContactDTO cv)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/"), "DataContact.hat");
                //Serialize
                using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                  var formatter = new BinaryFormatter();
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