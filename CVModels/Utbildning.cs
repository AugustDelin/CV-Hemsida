//Klassen Utbildning ärver från en annan klass och
//representerar en utbildning, med en egenskap för typen av utbildning den representerar.

using System.ComponentModel.DataAnnotations.Schema;

namespace CVModels
{
    public class Utbildning : Info
    {
        public string Type { get; set; }
    }
}

