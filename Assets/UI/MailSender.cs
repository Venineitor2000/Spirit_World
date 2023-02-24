using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class MailSender : MonoBehaviour
{
    

    

    public void EnviarMail(string mail, string contra)
    {
        CrearJson( mail,  contra);
        
        Process.Start(Application.dataPath +"/MailSenderApp/WebSenderAppDefinitiva.exe");
        
    }

    void CrearJson(string mail, string contra)
    {
        string fileName = Application.dataPath +"/MailData.json";
        //Cambiar aca el new mailData por mis funciones
        
        string jsonString = JsonUtility.ToJson(new MailData(mail, contra));
        File.WriteAllText(fileName, jsonString);
    }
}

public class MailData
{
    public string mail;
    
    public string contra;
    
    public MailData(string mail, string contra)
    {
        this.mail = mail;
        this.contra = contra;
    }

    
}