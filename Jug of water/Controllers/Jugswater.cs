using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jug_of_water.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Jugswater : ControllerBase
    {
        [HttpPost("resultado")]
        public IActionResult Resultado([FromBody] JarrasCapacidad capacidad)
        {
            //if: to not allow the entry of non-integer values
            if (capacidad.X <= 0 || capacidad.Y <= 0 || capacidad.Z <= 0) 
            {
                return BadRequest(new { error = "Not allowed values only positive numbers and integers" });
            }

            //Var: to determine if the exercise has a solution with the data entered
            var solucion = SolucionJarras(capacidad.X, capacidad.Y, capacidad.Z);

            if (solucion == null)
            {
                return BadRequest(new { error = "There is no solution" });
            }

            return Ok(new { solucion });
        }



        //list: sequence of steps that the program executes to solve the exercise
        public List<Secuencia> SolucionJarras(int X, int Y, int Z)
        {
            //If: which executes the function with the greatest common divisor of the data
            if (Z > Math.Max(X, Y) || Z % MaximoComunDivisor(X, Y) != 0)

            {
                return null;
            }
            //Function that determines which jar to start with to achieve the most optimal result
            bool inicio = EleccionDeJarra(X, Y, Z);

            //steps to follow with the while loop until you have the result
            var paso = new List<Secuencia>();

            int xActual = 0; int yActual = 0;

            while (xActual != Z && yActual != Z)
            {
                if (inicio)
                {
                    if (xActual == 0)
                    {
                        xActual = X;
                        paso.Add(new Secuencia
                        {
                            Steps = paso.Count + 1,
                            JugX = xActual,
                            JugY = yActual,
                            Action = "Fill Jar X"
                        });
                    }

                    else if (yActual == Y)


                    {
                        yActual = 0;
                        paso.Add(new Secuencia
                        {
                            Steps = paso.Count + 1,
                            JugX = xActual,
                            JugY = yActual,
                            Action = "Empty Jar Y"
                        });
                    }

                    else
                    {
                        int transferirAgua = Math.Min(xActual, Y - yActual);
                        xActual -= transferirAgua;
                        yActual += transferirAgua;
                        paso.Add(new Secuencia
                        {
                            Steps = paso.Count + 1,
                            JugX = xActual,
                            JugY = yActual,
                            Action = "Transfer from Jar x to jar Y"
                        });
                    }

                }
                else
                {
                    if (yActual == 0)
                    {
                        yActual = Y;
                        paso.Add(new Secuencia
                        {
                            Steps = paso.Count + 1,
                            JugX = xActual,
                            JugY = yActual,
                            Action = "Fill Jar Y "
                        });
                    }
                    else if (xActual == X)
                    {
                        xActual = 0;
                        paso.Add(new Secuencia
                        {
                            Steps = paso.Count + 1,
                            JugX = xActual,
                            JugY = yActual,
                            Action = "Empty Jar X"
                        });
                    }
                    else
                    {
                        int transferirAgua = Math.Min(yActual, X - xActual);
                        yActual -= transferirAgua;
                        xActual += transferirAgua;
                        paso.Add(new Secuencia
                        {
                            Steps = paso.Count + 1,
                            JugX = xActual,
                            JugY = yActual,
                            Action = "Transfer from Jar y to Jar x "
                        });
                    }
                }
            }

            return paso;
        }

        //greatest common divisor function
        public int MaximoComunDivisor( int a, int b)
        {
            while (b != 0)
            {
                int Div = b;
                b = a % b;
                a = Div;
            }
            return a;
        }

        //Function to choose which jar to start 
        public bool EleccionDeJarra(int X, int Y, int Z)
        {
            if (Z <= X)
            {
                return true;
            }
            else if (Z <= Y)
            {
                return false;
            }
            else
            {
                return X <= Y;
            }
        }

        //class of data requested from the user
        public class JarrasCapacidad
        {
            
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

        }

        //data that is displayed each time a step is taken in the exercise
        public class Secuencia
        {
            public int Steps { get; set; }
            public int JugX { get; set; }
            public int JugY { get; set; }

            public string Action {  get; set; }
        }

    }
}
