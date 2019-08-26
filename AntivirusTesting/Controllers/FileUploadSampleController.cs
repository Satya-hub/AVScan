using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AntivirusTesting.Models.FileUploadSample;


namespace AntivirusTesting.Controllers
{
    public class FileUploadSampleController : Controller
    {
        DateTime StartTime, EndTIme;
        // GET: FileUploadSample
        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]

        public ActionResult FileUpload(HttpPostedFileBase file_Uploader)

        {

            if (file_Uploader != null)

            {
                StartTime = DateTime.Now;

                string fileName = string.Empty;

                string destinationPath = string.Empty;



                List<FileUploadModel> uploadFileModel = new List<FileUploadModel>();



                fileName = Path.GetFileName(file_Uploader.FileName);

                destinationPath = Path.Combine(Server.MapPath("~/TempFiles/"), fileName);

                file_Uploader.SaveAs(destinationPath);

                if (Session["fileUploader"] != null)

                {


                    var isFileNameRepete = ((List<FileUploadModel>)Session["fileUploader"]).Find(x => x.FileName == fileName);

                    if (isFileNameRepete == null)

                    {

                        uploadFileModel.Add(new FileUploadModel { FileName = fileName, FilePath = destinationPath });

                        ((List<FileUploadModel>)Session["fileUploader"]).Add(new FileUploadModel { FileName = fileName, FilePath = destinationPath });

                        ViewBag.Message = "File Uploaded Successfully";
                        Dictionary<string, string> listOfFiles = new Dictionary<string, string>();
                        listOfFiles.Add(fileName, destinationPath);
                        AntivirusTesting.Utility.Testing testing = new AntivirusTesting.Utility.Testing();
                        testing.Execute(listOfFiles);

                    }

                    else

                    {

                        ViewBag.Message = "File is already exists";

                    }



                }

                else

                {

                    uploadFileModel.Add(new FileUploadModel { FileName = fileName, FilePath = destinationPath });

                    Session["fileUploader"] = uploadFileModel;
                    Dictionary<string, string> listOfFiles = new Dictionary<string, string>();
                    listOfFiles.Add(fileName, destinationPath);
                    AntivirusTesting.Utility.Testing testing = new AntivirusTesting.Utility.Testing();
                    testing.Execute(listOfFiles);

                    ViewBag.Message = "File Uploaded Successfully";

                }

                EndTIme = DateTime.Now;
                WriteLog("Vikash", StartTime, EndTIme);

            }

            return View();

        }



        [HttpPost]

        public ActionResult RemoveUploadFile(string fileName)

        {

            int sessionFileCount = 0;



            try

            {

                if (Session["fileUploader"] != null)

                {

                    ((List<FileUploadModel>)Session["fileUploader"]).RemoveAll(x => x.FileName == fileName);

                    sessionFileCount = ((List<FileUploadModel>)Session["fileUploader"]).Count;

                    if (fileName != null || fileName != string.Empty)

                    {

                        FileInfo file = new FileInfo(Server.MapPath("~/MyFiles/" + fileName));

                        if (file.Exists)

                        {

                            file.Delete();

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }



            return Json(sessionFileCount, JsonRequestBehavior.AllowGet);

        }





        public FileResult OpenFile(string fileName)

        {

            try

            {

                return File(new FileStream(Server.MapPath("~/MyFiles/" + fileName), FileMode.Open), "application/octetstream", fileName);

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }



        public void WriteLog(string LogData, DateTime starttime, DateTime endtime)
        {
            string createText = Environment.NewLine + "Start Time : " + starttime.ToString() + Environment.NewLine + LogData + Environment.NewLine + "End Time : " + endtime.ToString() + Environment.NewLine + "**********************";
            string path = Server.MapPath("~/LogReport/LogReport");
            System.IO.File.AppendAllText(path, createText);
        }



    }

}

