using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace examenMeli
{
    class Program
    {
        static void Main(string[] args)
        {
            //DETERMINAR SI UN PATRON DE ADN ES MUTANTE O NO ES MUTANTE DE ACUERDO A SU BASE NITROGENADA
            string[] dna = new string[6];

            //VARIABLES PARA CONTABILIZAR EL NUMERO DE 
            int i = 0;
            string linea = "";

            //VARIABLES PARA LA CONSISTENCIA DEL INGRESO DE TEXTO
            int contador = 0;
            bool terminado = false;
            string cadena = "ATCG";

            while (i < 6)
            {
                Console.Write("Ingrese Base Nitrogenada {0} =>  ", i);
                do
                {
                    Thread.Sleep(500);
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo tecla = Console.ReadKey(true);
                        if (cadena.Contains(tecla.KeyChar.ToString().ToUpper()))
                        {
                            Console.Write(tecla.KeyChar.ToString().ToUpper());
                            linea = linea + tecla.Key.ToString();
                            contador++;
                        }
                        if (contador == 6) terminado = true;
                    }
                } while (!terminado);
                dna[i] = linea;
                contador = 0;
                linea = "";
                terminado = false;
                i++;
                Console.WriteLine();
            }

            bool encontrado=isMutant(dna);
            Console.WriteLine(encontrado);

            Console.ReadKey();

        }
        static bool isMutant(string[] adn)
        {
            int contHorizontal = 0, contVertical=0, contDiagonal = 0;
            int totalBusqueda = 0;
            contHorizontal = buscar(adn);//BUSCAR Y ACUMULAR COINCIDENCIAS DE PATRON EN FORMA HORIZONTAL
            contVertical = buscar(vectorVertical(adn));//BUSCAR Y ACUMULAR COINCIDENCIAS DE PATRON EN FORMA VERTICAL
            contDiagonal = buscar(vectorDiagonal(adn));//BUSCAR Y ACUMULAR COINCIDENCIAS DE PATRON EN FORMA DIAGONAL
            totalBusqueda = contDiagonal + contVertical + contHorizontal;//REALIZAR LA SUMA DE LOS PATRONES ENCONTRADOS

            //SI SE ENCUENTRA 2 O MÁS PATRONES IGUALES ES MUTANTE(TRUE), EN CASO CONTRARIO NO ES MUTANTE(FALSE)
            if (totalBusqueda >= 2) return true;
            else return false;
        }
        static int buscar(string[] adn)
        {
            int adnEncontrado = 0;
            //PATRONES DE BUSQUEDA
            string cadenaA = "AAAA";
            string cadenaC = "CCCC";
            string cadenaT = "TTTT";
            string cadenaG = "GGGG";
            //BUSCAR POR LOS PATRONES POR CADA FILA
            for (int j = 0; j < adn.Length; j++)
            {
                if (adn[j].Contains(cadenaA) || adn[j].Contains(cadenaC) || adn[j].Contains(cadenaT) || adn[j].Contains(cadenaG)) adnEncontrado++; //SI ENCUENTRA COINCIDENCIAS ACUMULA
            }
            return adnEncontrado;
        }
        static string[] vectorVertical(string[] adn)
        {
            string[] invertirVector = new string[adn.Length];
            //CONVERTIR COLUMNAS DEL VECTOR EN FILAS
            for (int j = 0; j < adn.Length; j++)
            {
                for (int h = 0; h < invertirVector.Length; h++)
                {
                    invertirVector[h] = invertirVector[h] + adn[j].Substring(h, 1);
                }
            }
            return invertirVector;
        }
        static string[] vectorDiagonal(string[] adn)
        {
            //CONVERTIR DIAGONALES DEL VECTOR EN FILAS
            string[] construirDiagonal = new string[adn.Length-1];
            int letraFinal = 2, letraInicial = 0, pos = 2;
            int pos1, pos2;
            for (int j = 0; j < adn.Length; j++)
            {
                if(j<=2) pos++;
                if (j > 3) pos--;
                pos1 = letraInicial;
                pos2 = letraFinal;
                for (int h = 0; h<pos; h++)
                {
                    construirDiagonal[pos1] = construirDiagonal[pos1] + adn[j].Substring(pos2, 1);
                    pos1++;
                    pos2--;
                }
                if (letraFinal == construirDiagonal.Length) letraInicial++;
                else
                {
                    letraFinal++; letraInicial = 0;
                }
            }
            return construirDiagonal;
        }
    }
}
