using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamenI
{
    internal class Program
    {
        static string option, nombre;
        static int  puestos, entrada, salida;
        static double horas, extras, salario, dias, tardia, marca1, marca2, anticipada;
        static bool data=false;
        public static string menu()
        {
            Console.WriteLine("1.Puesto.\n2.Entradas y Salidas\n3.Reporte\n0.Salir");
            option=Console.ReadLine();
            switch(option)
            {
                case "1":
                    Console.WriteLine("Digite su nombre: ");
                    nombre=Console.ReadLine();
                    puesto();
                    break;
                case "2":
                    if (puestos==0)
                    {
                        Console.WriteLine("No ha seleccionado un puesto");
                    }
                    else
                    {
                        datos(puestos);
                        EntradasySalidas();
                        break;
                    }
                    break;
                case "3":
                    if (data==false)
                    {
                        Console.WriteLine("Ingrese las Entradas y Salidas");
                    }
                    else
                    {
                        reporte();
                    }
                    break;
                    
                case "0":
                break;
                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }
            return option;
                
        }
        public static int puesto()
        {
            Console.WriteLine("1.Gerente\n2.Supervisor\n3.Operario\nSeleccione una opcion.");
            try
            {
                puestos = int.Parse(Console.ReadLine());
                if (puestos < 0 || puestos > 3)
                {
                    Console.WriteLine("Seleccion invalida");
                    puestos = 0;
                }
                return puestos;
            }
            catch(FormatException)
            {
                Console.WriteLine("Seleccion invalida");
            }
            return puestos;
               
           
        }

        static void datos(int puestos)
        {
            if (puestos == 1)
            {
                salario = 7000;
                entrada = 8;
                salida = 16;
                dias = 5;
            }
            else if (puestos == 2)
            {
                salario = 5000;
                entrada = 9;
                salida = 18;
                dias = 6;
            }
            else if(puestos == 3)
            {
                salario = 2500;
                entrada = 9;
                salida = 18;
                dias = 6;
            }
            data = true;
        }

        static void EntradasySalidas()
        {
            for (int i = 0; i < dias; i++)
            {
                bool aux=false;
                double dia = 0;
                int horaxdias = salida - entrada;
                do
                {
                    bool morning = true;
                    marca1 = marcas(i,morning);
                    morning=false;
                    marca2 = marcas(i, morning);
                    dia = marca2 - marca1;
                    if (dia < 5 || dia > 13)
                    {
                        Console.WriteLine("Registro de horas invalido");
                        continue;
                    }
                    else if (horaxdias<dia)
                    {
                
                        
                       extras += dia - horaxdias;
                        
                        
                    }
                    if (marca1 > entrada)
                    {
                        tardia += marca1 - entrada;
                    }
                    if ( marca2>salida)
                    {
                        anticipada += marca2 - salida;
                    }
                    horas += dia;
                    aux = true;
                } while (aux==false);
             
            }
            if (puestos==1 && horas<16 || horas<18)
            {
                Console.WriteLine("Registro invalido. Menos de 2 dias laborados");
            }
            else
            {
                Console.WriteLine("Registro exitoso.");

            }

        }
        public static double marcas(int i, bool morning)
        {
            bool aux = false;
            string marca;
            string ampm;
            string d;
            int hora=0;
            if (morning == true)
            {
                d = "entrada";
            }
            else
            {
                d = "salida";
            }
            do
            {
                Console.WriteLine("Ingrese la hora de "+d+" del dia: " + (i + 1));
                Console.WriteLine("Utilice formato AM/PM");
                Console.WriteLine("(Si no trabajo ingresar 12am en entrada y salida)");
                marca = Console.ReadLine();
                marca=marca.ToUpper();
                ampm = marca.Substring(marca.Length - 2);
                if (marca.Length > 4 || ampm!="PM" && ampm!= "AM")
                {
                    Console.WriteLine("Datos incorrectos");
                }
                else
                {
                    hora = int.Parse(marca.Substring(0, marca.Length - 2));
                    if(hora <= 0 || hora>12)
                    {
                        Console.WriteLine("Datos incorrectos");
                        continue;
                    }
                    else if (ampm=="PM")
                    {
                        hora += 12;
                    }
                    aux = true;
                }
                    


            } while (aux == false);
            return hora;
        }

        static void reporte()
        {
            double salariobase = ((horas - extras) * salario);
            double extra = (extras * salario);
            double bono = 0;
            double amonestacion = 0;
            
            Console.WriteLine(nombre);
            if (puestos == 1)
            {
                Console.WriteLine("Gerente");
            }
            else if (puestos == 2)
            {
                Console.WriteLine("Supervisor");
            }
            else 
            {
                Console.WriteLine("Operario");
            }
            Console.WriteLine("Salario por hora: " + "¢"+ salario);
            Console.WriteLine("Horas trabajadas: " + horas);
            Console.WriteLine("Horas extras: " + extras);
            if (tardia>0)
            {
                Console.WriteLine("Horas tardias: " + tardia);
            }
            if (anticipada > 0)
            {
                Console.WriteLine("Horas de Salidas anticipadas: " + anticipada);
            }
            Console.WriteLine("Salario base: +¢"+ salariobase);
            Console.WriteLine("Extras: +¢" + extra);
            if (tardia != 0 || anticipada != 0)
            {
                bono = ((salariobase + extra) * 0.1);
                Console.WriteLine("Bono 10%: +¢"+bono);
            }
            else
            {
                Console.WriteLine("Amonestacion: -¢"+amonestacion);
                amonestacion = ((salariobase + extra) * ((tardia + anticipada) * 0.002));

            }
            double salariobruto = ((salariobase + extra + bono - amonestacion));
            double deducciones = (salariobruto * 0.1067);
            double salarioneto = (salariobruto - deducciones);
            Console.WriteLine("Deducciones de ley: -¢"+deducciones);
            Console.WriteLine("Total: ¢"+salarioneto);
        }

        static void Main(string[] args)
        {
            do
            {
              menu();

            }while (option!="0");

            Console.ReadLine();

        }
    }
}
