using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

public class UserManager: MonoBehaviour 
{
    public static UserManager instance;
    public static string ContraRandom()
    {
        System.Random random = new System.Random();
        int length = 8;
        var rString = "";
        for (var i = 0; i < length; i++)
        {
            rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
        }
        return rString;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }    

        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        
        usuarios = new List<Usuario>();
        grupos = new List<Grupo>();
        AddGrupo("Admin", "Posee acceso al menu de administrador, puede gestionar usuarios y grupos", true);
        AddGrupo("Tester", "Posee acceso al menu de tester, puede...", true);
        
        AddUsuario("Venineitor2000", "Juan", "Altuna", "Altu@gmail.com", new List<Grupo>() { grupos[0]}, true);
        AddUsuario("Pedro2000", "Pedro", "Del Campo", "Pedro@gmail.com", new List<Grupo>() { grupos[1] }, true);

        
        //grupos[0].AddUsuarios(new List<Usuario>() { usuarios[0] });
        //grupos[1].AddUsuarios(new List<Usuario>() { usuarios[1] });
        
        
    }
    List<Usuario> usuarios;
    static List<Grupo> grupos;
    Usuario usuarioLogueadoActual;
    public List<Usuario> GetUsuarios()
    {
        
        return usuarios;
    }

   

    public List<Usuario> GetUsuariosDisponibles()
    {
        
        return (usuarios.FindAll(x => x.GetEstado() == true));
    }

    public static void AddGrupo(string nombre, string descripcion, bool estado, List<Usuario> usuarios = null)
    {
        Grupo nuevoGrupo = new Grupo(nombre, descripcion, estado, usuarios);
        grupos.Add(nuevoGrupo);
        
        
    }

    public static void EliminarGrupo(Grupo grupo)
    {
        grupos.Remove(grupo);
        foreach (var usuario in grupo.GetUsuarios())
        {
            usuario.DeleteGrupo(grupo);
        }
        
        grupo = null;
    }

    public static List<Grupo> getGrupos()
    {
        
        
        return grupos;
    }

    public static List<Grupo> getGruposDisponibles()
    {

        
        return grupos.FindAll(x => x.GetEstado() == true);
    }
    public void EliminarUsuario(Usuario usuario)
    {
        usuarios.Remove(usuario);
        usuario = null;
    }

    public void AddUsuario(string nombreUsuario, string nombre, string apellido, string email, List<Grupo> grupos, bool estado)
    {
        
        Usuario nuevoUsuario = new Usuario(nombreUsuario, nombre, apellido, email, grupos, estado);
        usuarios.Add(nuevoUsuario);
    }

    public bool Validarusuario(string nombreUsuario, string password)
    {
        Usuario usuario = usuarios.Find(x => x.GetNombreUsuario() == nombreUsuario);
        if (usuario != null) 
        {
            if (usuario.GetEstado() == false)
                return false;
            if (usuario.ValidarContraseña(password))
            {
                usuarioLogueadoActual = usuario;
                return true;
            }
               
        }
            
        return false;
    }

    public void CerrarSesion()
    {
        usuarioLogueadoActual = null;
    }

    public Usuario GetUsuarioLogueadoActual()
    {
        return usuarioLogueadoActual;
    }

}

public class Usuario
{
    

    string nombreUsuario;
    string nombre;
    string apellido;
    string password; //Cifrada
    string email;
    List<Grupo> grupos = new List<Grupo>();
    bool estado;
   

    public Usuario(string nombreUsuario, string nombre, string apellido, string email, List<Grupo> grupos, bool estado = true)
    {
        foreach (Grupo grupo in grupos)
            grupo.AddUsuario(this);
        
        this.nombreUsuario = nombreUsuario;
        this.nombre = nombre;
        this.apellido = apellido;

        this.email = email;
        this.grupos = grupos;
        this.estado = estado;
        password = EncriptarContraseña("Password");//Contra por defecto, despues cada uno se la reinicia solo
    }

  

    public void ActualizarCampos(string nombreUsuario, string nombre, string apellido, string email, List<Grupo> grupos, bool estado)
    {
        foreach (var grupoNuevo in grupos)
        {
            if (this.grupos.Find(x => x.GetNombre() == grupoNuevo.GetNombre()) == null)
                grupoNuevo.AddUsuario(this);
        }
        foreach (var grupoViejo in this.grupos)
        {
            if (grupos.Find(x => x.GetNombre() == grupoViejo.GetNombre()) == null)
                grupoViejo.DeleteUsuario(this);
        }
        this.nombreUsuario = nombreUsuario;
        this.nombre = nombre;
        this.apellido = apellido;
        this.estado = estado;
        this.email = email;
        this.grupos = grupos;
        
    }

    public string GetApellidoNombre()
    { return this.apellido + " "+ this.nombre; }
    public string GetApellido()
        { return this.apellido; }
    public string GetNombre()
        { return this.nombre; }
    public bool GetEstado()
    {
        return this.estado;
    }

    public List<Grupo> GetGrupos()
    {
        return grupos;
    }
    string EncriptarContraseña(string password)
    {
        string strOriginal = password;

        byte[] bytOriginal = Encoding.ASCII.GetBytes(strOriginal);

        SHA512 objAlgoritmo = SHA512.Create();

        byte[] bytHash = objAlgoritmo.ComputeHash(bytOriginal);

        string strHash = Convert.ToBase64String(bytHash);

        return strHash;
    }

    public void EstablecerContraseña(string password)
    {
        


        this.password = EncriptarContraseña(password);
        
    }

    public bool ValidarContraseña(string password)
    {
        return this.password.Equals(EncriptarContraseña(password));
    }

    public string GetNombreUsuario()
    {
        return nombreUsuario;
    }

    public string GetEmail()
    {
        return email;
    }

    public void AddGrupo(Grupo grupo)
    {
        grupos.Add(grupo);
    }

    public void DeleteGrupo(Grupo grupo)
    {
        grupos.Remove(grupo);
    }
}

public class Grupo
{
    
    
    List<Usuario> usuarios = new List<Usuario>();
    string nombre;
    string descripcion;
    bool estado;
    

    public Grupo(string nombre, string descripcion, bool estado, List<Usuario> usuarios = null)
    {
        if(usuarios == null)
            usuarios = new List<Usuario>();
        foreach (var item in usuarios)
        {
            item.AddGrupo(this);
        }
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.usuarios = usuarios;
        
    }

    public void ActualizarCampos(string nombre, string descripcion, bool estado, List<Usuario> usuarios)
    {
        //Lo que estaria haciendo aca es agarrar y decir, dentro de los usuarios viejos, existe este usuario nuevo? Si la respuesta es no, quiere decir que es un usuario que recien esta siendo añadido, por tanto le avisa que fue tiene que añadir este grupo a su lista de grpos ahora que forma parte de el
        foreach (var usuarioNuevo in usuarios)
        {
            if (this.usuarios.Find(x => x.GetNombreUsuario() == usuarioNuevo.GetNombreUsuario()) == null)               
                usuarioNuevo.AddGrupo(this);
        }
        foreach (var usuarioViejo in this.usuarios)
        {
            if(usuarios.Find(x => x.GetNombreUsuario() == usuarioViejo.GetNombreUsuario()) == null)
                usuarioViejo.DeleteGrupo(this);
        }
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.usuarios = usuarios;
        
    }

    public void AddUsuario(Usuario usuario)
    {
        
        usuarios.Add(usuario);
        
    }

    public void DeleteUsuario(Usuario usuario)
    {
        foreach(var grupo in usuario.GetGrupos())
        {
            grupo.DeleteUsuario(usuario);
        }
        usuarios.Remove(usuario);
    }

    public List<Usuario> GetUsuarios()
    {
        
        return usuarios;
    }
    public string GetDescipcion()
    {
        return descripcion;
    }
    public string GetNombre()
    {
        return nombre;
    }

    public bool GetEstado()
    {
        return estado;
    }
    
}