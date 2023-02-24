using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

public class SQLiteDB : MonoBehaviour
{
    //Ubicacion de las 2 base de datos para hacer demostraciones:
    //C:\UnityProjectos\Spirit_World_Definitivo
    //C:\Users\venin\AppData\LocalLow\DefaultCompany\Spirit_World_Definitivo
    [SerializeField] Reporte reporte;
    public static SQLiteDB instance;
    private string dbName = "URI=file:DataBase.db";
    private string backUpDbName;
    float startTime = 0;
    float endTime = 3;
    private void Awake()
    {
        
        backUpDbName = "URI=file:" + Application.persistentDataPath + "/DataBase.db";

    instance = this;
        
    }

    private void Update()
    {
        
        startTime += Time.deltaTime;
        if(startTime >= endTime)
        {
            
            startTime = 0;
            GuardarPartida();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GuardarPartida();
            CargarPartida(dbName);
        }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            CargarPartida(dbName);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            GenerarReporte("SELECT * FROM spirit ORDER BY fecha ASC;", "Player");
        if (Input.GetKeyDown(KeyCode.Alpha2))
                GenerarReporte("SELECT * FROM spirit ORDER BY fecha ASC;", "AI");
        if (Input.GetKeyDown(KeyCode.Alpha3))
            GuardarBackUp();
    }


    private void Start()
    {
        CreateTable(dbName);
        CreateTable(backUpDbName);
        string fechaDb = "";
        string fechaBackUp = "";
        using (var connection1 = new SqliteConnection(dbName))
        {
            connection1.Open();
            string sqlSelect = "SELECT * FROM spirit ORDER BY fecha DESC LIMIT 1";
            using (var command = connection1.CreateCommand())
            {
                command.CommandText = sqlSelect;

                using (IDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        fechaDb = reader.GetString(5);


                    }
                }
            }


            connection1.Close();
        }

        using (var connection2 = new SqliteConnection(backUpDbName))
        {
            connection2.Open();
            string sqlSelect = "SELECT * FROM spirit ORDER BY fecha DESC LIMIT 1";
            using (var command = connection2.CreateCommand())
            {
                command.CommandText = sqlSelect;

                using (IDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        fechaBackUp = reader.GetString(5);


                    }
                }
            }


            connection2.Close();
        }
        //No tengo bakc up
        if (fechaBackUp == "")
        {
            //Pero si db
            if (fechaDb != "")
            {
                CargarPartida(dbName);
                Debug.Log("Cargue con db, no habia back up");
            }
            else
            Debug.Log("No hay nada para cargar");


        }          
        //Tengo back up pero no db
        else if(fechaDb == "")
        {

            CargarPartida(backUpDbName);
            
            //Creo o sobreescribo el archivo db, para que sea igual al back up
            string rutaOrigen = Application.persistentDataPath + "/DataBase.db";
            string rutaDestino = Application.dataPath + "/.." + "/DataBase.db";
            System.IO.File.Delete(rutaDestino);
            System.IO.File.Copy(rutaOrigen, rutaDestino);
            Debug.Log("cargue back up, no habia db");
        }
        //Tengo back up y db
        else
        {
            System.DateTime dateDb = System.DateTime.Parse(fechaDb);
            System.DateTime dateBackUP = System.DateTime.Parse(fechaBackUp);
            if (dateBackUP > dateDb)
            {
                CargarPartida(backUpDbName);
                //Creo o sobreescribo el archivo db, para que sea igual al back up
                
                string rutaOrigen = "file:" + Application.persistentDataPath + "/DataBase.db";
                string rutaDestino = Application.dataPath + "/.." + "/DataBase.db";
                System.IO.File.Delete(rutaDestino);
                System.IO.File.Copy(rutaOrigen, rutaDestino);
                Debug.Log("cargue back up, la partida estaba desact");
            }

            CargarPartida(dbName);
            Debug.Log("cargue partida, estaba bien actualizada");
        }

    }
    private void OnDestroy()
    {
        Debug.Log("Hoña");
        GuardarPartida();
        GuardarBackUp();
    }

    void GuardarBackUp()
    {
        CreateTable(backUpDbName);
        string fechaDb = "";
        string fechaBackUp = "";
        using (var connection1 = new SqliteConnection(dbName))
        {
            connection1.Open();
            string sqlSelect = "SELECT * FROM spirit ORDER BY fecha DESC LIMIT 1";
            using (var command = connection1.CreateCommand())
            {
                command.CommandText = sqlSelect;

                using (IDataReader reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        fechaDb = reader.GetString(5);


                    }
                }
            }


            connection1.Close();
        }

        using (var connection2 = new SqliteConnection(backUpDbName))
        {
            connection2.Open();
            string sqlSelect = "SELECT * FROM spirit ORDER BY fecha DESC LIMIT 1";
            using (var command = connection2.CreateCommand())
            {
                command.CommandText = sqlSelect;

                using (IDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        fechaBackUp = reader.GetString(5);


                    }
                }
            }


            connection2.Close();
        }
        if(fechaBackUp == "")
        {
            ActualizarBackUp("SELECT * FROM spirit ORDER BY fecha ASC");
            

        }

        else
        {
            
            System.DateTime dateDb = System.DateTime.Parse(fechaDb);
            System.DateTime dateBackUP = System.DateTime.Parse(fechaBackUp);
            if (dateBackUP < dateDb)
            {
                ActualizarBackUp($"SELECT * FROM spirit WHERE fecha > '{fechaBackUp}';");
            }
        }
            

        
    }

    void ActualizarBackUp(string sqlSelectCommand)
    {
        using (var connection1 = new SqliteConnection(dbName))
        {
            List<string> commands = new List<string>();
            connection1.Open();
            string sqlSelect = sqlSelectCommand;
            using (var command = connection1.CreateCommand())
            {
                command.CommandText = sqlSelect;

                using (IDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        

                        AñadirEspiritu(float.Parse(reader["posX"].ToString()), float.Parse(reader["posY"].ToString()), float.Parse(reader["posZ"].ToString()), reader["name"].ToString(), reader["fecha"].ToString(), backUpDbName);

                    }
                    
                }
            }


            connection1.Close();
        }
    }
    public void CargarPartida(string dbName)
    {
  
        
        Query("SELECT * FROM spirit ORDER BY id DESC;", dbName);


    }
    public void GuardarPartida()
    {            Debug.Log(System.DateTime.Now.ToString());

        //Query("DROP TABLE IF EXISTS spirit;");
        CreateTable(dbName);
        foreach (var spirit in FindObjectsOfType<ControladorEspiritu>())
        {
            AñadirEspiritu(spirit.transform.position.x, spirit.transform.position.y, spirit.transform.position.z, spirit.name, (System.DateTime.Now.ToString()), dbName);
        }
    }

    void AñadirEspiritu(float posX, float posY, float posZ, string name, string date, string dbName)
    {

        Query($"INSERT INTO spirit (posX, posY, posZ, name, fecha) VALUES ({(int)posX} , {(int)posY}, {(int)posZ}, '{name}', '{date}');", dbName);




    }

    private void CreateTable(string dbName)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string sqlcreation = "";


                sqlcreation += "CREATE TABLE IF NOT EXISTS spirit(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "posX     real NOT NULL,";
                sqlcreation += "posY     real NOT NULL,";
                sqlcreation += "posZ     real NOT NULL,";
                sqlcreation += "name VARCHAR(50) NOT NULL,";
                sqlcreation += "fecha VARCHAR(50) NOT NULL";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }
            
            connection.Close();
        }
    }


    void GenerarReporte(string q, string espirituName)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    
                    
                    List<string> list = new List<string>();
                    list.Add(espirituName + "\n");
                    while (reader.Read())
                    {
                        
                                if (espirituName == reader["name"].ToString())
                                {
                                    list.Add(reader["name"].ToString() + $" estaba en la posicion: " + float.Parse(reader["posX"].ToString()) + ", " + float.Parse(reader["posY"].ToString()) + ", " + float.Parse(reader["posZ"].ToString()) + " a la hora " + reader["fecha"].ToString()+ "\n");


                                }
                            
                    }

                    reporte.GenerarReporte(list, espirituName);
                }
            }

            connection.Close();
        }
    }

    public void Query(string q, string dbName)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    int co;
                    List<string> espiritusCargados = new List<string>();
                    co = FindObjectsOfType<ControladorEspiritu>().Length;
                    while (reader.Read())
                    {

                        if(co > 0)
                        foreach (var spirit in FindObjectsOfType<ControladorEspiritu>())
                        {
                                
                            if(spirit.name == reader["name"].ToString() && !espiritusCargados.Contains(spirit.name))
                            {
                                co--;
                                    espiritusCargados.Add(spirit.name);//nuevo
                                Debug.Log(reader["name"].ToString() + $"Estaba en la posicion: " + float.Parse(reader["posX"].ToString()) + ", " + float.Parse(reader["posY"].ToString()) + ", " + float.Parse(reader["posZ"].ToString()) + " a la hora " + reader["fecha"].ToString());
                                spirit.transform.position = new Vector3(float.Parse(reader["posX"].ToString()), float.Parse(reader["posY"].ToString()), float.Parse(reader["posZ"].ToString()));
                            }
                        }
                    }
                }
            }

            connection.Close();
        }
    }
}

