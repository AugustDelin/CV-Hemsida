//Den här koden definierar en klass med namnet TidigareErfarenhet,
//som ärver från en annan klass Info.
//Den innehåller en egenskap Type av typen sträng för att
//representera typen av tidigare erfarenhet.

namespace CVModels
{
    public class TidigareErfarenhet : Info
    {
        public string Type { get; set; }
    }
}
