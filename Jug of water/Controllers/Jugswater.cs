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
            //If: 
            if (capacidad.X <= 0 || capacidad.Y <= 0 || capacidad.Z <= 0) 
            {
                return BadRequest(new { error = "Not allowed values only positive numbers and integers" });
            }

            var solucion = SolucionJarras(capacidad.X, capacidad.Y, capacidad.Z);

            if (solucion == null)
            {
                return BadRequest(new { error = "There is no solution" });
            }

            return Ok(new { solucion });
        }


        

        public List<Secuencia> SolucionJarras(int X, int Y, int Z)
        {
            if (Z > Math.Max(X, Y) || Z % MaximoComunDivisor(X, Y) != 0)

            {
                return null;
            }

            bool inicio = EleccionDeJarra(X, Y, Z);

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


        public class JarrasCapacidad
        {
            
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

        }

        public class Secuencia
        {
            public int Steps { get; set; }
            public int JugX { get; set; }
            public int JugY { get; set; }

            public string Action {  get; set; }
        }

    }
}
