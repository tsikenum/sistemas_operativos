using proyecto_sistemas_op.Data;



// See https://aka.ms/new-console-template for more information

AccesoDatos data = new AccesoDatos();
List<Dictionary<string,string>> parser_data=data.parser_data();
foreach (var dato in parser_data)
{
    Console.WriteLine("Edad "+dato["edad"]);
    Console.WriteLine("cantidad_hombres " + dato["cantidad_hombres"]);
    Console.WriteLine("cantidad_mujeres " + dato["cantidad_mujeres"]);
    Console.WriteLine("primaria_incompleta " + dato["primaria_incompleta"]);
    Console.WriteLine("primaria_completa " + dato["primaria_completa"]);
    Console.WriteLine("secundaria_completa " + dato["secundaria_completa"]);
    Console.WriteLine("secundaria_incompleta " + dato["secundaria_incompleta"]);
    Console.WriteLine("universitaria_completa " + dato["universitaria_completa"]);
    Console.WriteLine("universitaria_incompleta " + dato["universitaria_incompleta"]);
    Console.WriteLine("sin_estudios " + dato["sin_estudios"] + "\n");
}
