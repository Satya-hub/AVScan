using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntivirusTesting.Models.FileUploadSample;

namespace AntivirusTesting.Controllers
{
    public class MultipleFileController : Controller
    {

        DateTime StartTime, EndTIme;
        // GET: MultipleFile
        public ActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files, string Upload, string Submit)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Upload))
                {

                    StartTime = DateTime.Now;

                    string fileName = string.Empty;

                    string destinationPath = string.Empty;



                    List<MultipleFileModel> uploadFileModel = new List<MultipleFileModel>();


                    destinationPath = Path.Combine(Server.MapPath("~/TempFiles/"), fileName);



                    //iterating through multiple file collection   
                    foreach (HttpPostedFileBase file in files)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/TempFiles/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            uploadFileModel.Add(new MultipleFileModel { FileName = file.FileName, FilePath = destinationPath });
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }

                    }

                    Session["fileUploader"] = uploadFileModel;

                    ViewBag.Message = "File Uploaded Successfully";

                    EndTIme = DateTime.Now;
                    WriteLog("Vikash", StartTime, EndTIme);
                }
                if (!string.IsNullOrEmpty(Submit))
                {
                    string rootFolderPath = Server.MapPath("~/TempFiles/");
                    string destinationPath = Server.MapPath("~/MyFiles/");
                    string[] fileList = System.IO.Directory.GetFiles(rootFolderPath);
                    foreach (string file in fileList)
                    {
                        string fileToMove = Path.Combine(rootFolderPath, file);
                        string moveTo = Path.Combine(destinationPath, file);
                        //moving file
                        System.IO.File.Move(fileToMove, moveTo);
                    }
                }
            }
            return View();
        }



        public void WriteLog(string LogData, DateTime starttime, DateTime endtime)
        {
            System.TimeSpan diffResult = starttime.Subtract(endtime);
            string createText = Environment.NewLine + "Start Time : " + starttime.ToString() + Environment.NewLine + LogData + Environment.NewLine + "End Time : " + endtime.ToString() + Environment.NewLine + "Total time taken to process file : " + diffResult.ToString() + Environment.NewLine + "**********************";
            string path = Server.MapPath("~/LogReport/LogReport");
            System.IO.File.AppendAllText(path, createText);
        }



    }
}
