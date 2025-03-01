using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string rutaLaboratorio = @"C:\\Users\\Mario\\OneDrive\\Escritorio\\LaboratorioAvengers"; 
        //Carpeta principal
        string rutaInventos = Path.Combine(rutaLaboratorio, "inventos.txt"); //archivo de inventos
        string rutaBackup = Path.Combine(rutaLaboratorio, "Backup"); //carpeta de respaldo
        string rutaArchivosClasificados = Path.Combine(rutaLaboratorio, "ArchivosClasificados"); //carpeta de archivos clasificados
        string rutaProyectosSecretos = Path.Combine(rutaLaboratorio, "ProyectosSecretos"); //carpeta de proyectos secretos

        CrearCarpeta(rutaLaboratorio);

        while (true)
        {
            //Menu principal del programa
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("---------- Menu de Opciones ----------");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Crear Archivo de Inventos de Tony Stark ._.");
            Console.WriteLine("2. Agregar un Invento");
            Console.WriteLine("3. Leer linea por linea los inventos");
            Console.WriteLine("4. Leer todo el contenido del archivo");
            Console.WriteLine("5. Copiar archivo a Backup");
            Console.WriteLine("6. Mover archivo a Archivos Clasificados");
            Console.WriteLine("7. Crear Carpeta de Proyectos Secretos");
            Console.WriteLine("8. Listar archivos en LaboratorioAvengers");
            Console.WriteLine("9. Eliminar archivo `inventos.txt`");
            Console.WriteLine("10. Salir");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();
            //Leer la opcion que el usuario selecciona 
            switch (opcion)
                //para manejar y ejecutar las diferentes opciones del menu 
            { 
                case "1": //crear el archivo de inventos.txt
                    CrearArchivo(rutaInventos);
                    break;
                case "2": //agregar un invento al archivo
                    Console.Write("Ingrese el nombre del invento: ");
                    string invento = Console.ReadLine();
                    AgregarInvento(rutaInventos, invento);
                    break;
                case "3": //leer linea por linea los inventos
                    LeerLineaPorLinea(rutaInventos);
                    break;
                case "4"://leer todo el contenido del archivo
                    LeerTodoElTexto(rutaInventos);
                    break;

                case "5"://Copiar el archivo a la carpeta de respaldo
                    CopiarArchivo(rutaInventos, Path.Combine(rutaBackup, "inventos.txt"));
                    break;
                case "6"://Mover el archivo a la carpeta de archivos clasificados
                    MoverArchivo(rutaInventos, Path.Combine(rutaArchivosClasificados, "inventos.txt"));
                    break;
                case "7"://Crear la carpeta de proyectos secretos
                    CrearCarpeta(rutaProyectosSecretos);
                    break;
                case "8"://Listar los archivos en la carpeta de laboratorio

                    ListarArchivos(rutaLaboratorio);
                    break;
                case "9"://eliminar el archivo de inventos
                    EliminarArchivo(rutaInventos);
                    break;
                case "10"://Salir del programa
                    Console.WriteLine("Saliendo del programa..........");
                    return;

                default:
                    Console.WriteLine("Opcion no valida. Intente de nuevo.");
                    //mensaje de error cuando la seleccion no es valida
                    break;
            }
        }
    }
    //crear una carpeta si no existe
    static void CrearCarpeta(string ruta)
    {
        try
        {
            if (!Directory.Exists(ruta))//verificar si la carpeta existe
            {
                Directory.CreateDirectory(ruta);
                Console.WriteLine($"Carpeta '{ruta}' creada exitosamente.");
            }
            else
            {
                Console.WriteLine($"La carpeta '{ruta}' ya existe.");
            }
        }
        catch (Exception ex)//manejo de excepciones
        {
            Console.WriteLine($"Error al crear la carpeta: {ex.Message}");//mensaje de error si la carpeta ya existe
        }
    }
    //crear un archivo si no existe
    static void CrearArchivo(string ruta)
    {
        try
        {
            if (!File.Exists(ruta))
            {
                File.Create(ruta).Close();
                Console.WriteLine($"Archivo {ruta} creado con exito");
            }
            else
            {
                Console.WriteLine($"El archivo {ruta} ya existe");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear el archivo {ruta}: {ex.Message}");
        }
    }
    //funcion para agregar un invento al archivo
    static void AgregarInvento(string ruta, string invento)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(ruta))
            {
                sw.WriteLine(invento);
            }
            Console.WriteLine($"Invento {invento} agregado con exito");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el invento {invento}: {ex.Message}");
        }
    }
    //funcion para leer linea por linea los inventos
    static void LeerLineaPorLinea(string ruta)
    {
        try
        {
            if (File.Exists(ruta))
            {
                using (StreamReader sr = new StreamReader(ruta))
                    //abir archivo solo para lectura
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                        //leer cada linea del archivo
                    {
                        Console.WriteLine(linea);
                    }
                }
            }
            else
            {
                Console.WriteLine($"El archivo {ruta} no existe. Ultron debe haberlo borrado!!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo {ruta}: {ex.Message}");
        }
    }
    //funcion para leer todo el contenido del archivo
    static void LeerTodoElTexto(string ruta)
    {
        try
        {
            if (File.Exists(ruta))//verificar si el archivo existe
            {
                string contenido = File.ReadAllText(ruta);
                Console.WriteLine(contenido);
                //leer y mostrar el contenido del archivo
            }
            else
            {
                Console.WriteLine($"Error: El archivo '{ruta}' no existe. ¡Ultron debe haberlo borrado!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }
    }
    //funcion para copiar un archivo
    static void CopiarArchivo(string origen, string destino)
    {
        try
        {
            if (File.Exists(origen))//verificar si el archivo de origen existe
            {
                CrearCarpeta(Path.GetDirectoryName(destino));
                //crear la carpeta de destino si no existe
                File.Copy(origen, destino, true);//copiar el archivo
                Console.WriteLine($"Archivo copiado exitosamente a '{destino}'.");
            }
            else
            {
                Console.WriteLine($"Error: El archivo '{origen}' no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al copiar el archivo: {ex.Message}");
        //mensaje si el archivo de origen no existe
        }

    }
    //funcion para mover un archivo
    static void MoverArchivo(string origen, string destino)
    {
        try
        {
            if (File.Exists(origen))
            {
                CrearCarpeta(Path.GetDirectoryName(destino));
                File.Move(origen, destino);
                Console.WriteLine($"Archivo movido exitosamente a '{destino}'.");
            }
            else
            {
                Console.WriteLine($"Error: El archivo '{origen}' no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al mover el archivo: {ex.Message}");
        }
    }
    //funcion para listar los archivos en una carpeta
    static void ListarArchivos(string ruta)
    {
        try
        {
            if (Directory.Exists(ruta))
            {
                string[] archivos = Directory.GetFiles(ruta);//otener la lista de archivos
                Console.WriteLine($"Archivos en '{ruta}':");
                foreach (string archivo in archivos)
                {
                    Console.WriteLine(Path.GetFileName(archivo));
                    //mostrar el nombre de cada archivo
                }
            }
            else
            {
                Console.WriteLine($"Error: La carpeta '{ruta}' no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al listar archivos: {ex.Message}");
        }
    }
    //funcion para eliminar un archivo
    static void EliminarArchivo(string ruta)
    {
        try
        {
            if (File.Exists(ruta))
            {
                File.Delete(ruta);//eliminar el archivo
                Console.WriteLine($"Archivo '{ruta}' eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine($"Error: El archivo '{ruta}' no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar el archivo: {ex.Message}");
        }
    }
}