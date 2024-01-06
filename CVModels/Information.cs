//Koden definierar en abstrakt klass Info för att hantera information relaterad till ett CV.
//Den inkluderar egenskaper som titel, beskrivning och
//datum för denna information, samt en främmande nyckel för att koppla den till
//ett specifikt CV i en databastabell med namnet "Information".

using System.ComponentModel.DataAnnotations.Schema;

namespace CVModels
{
    [Table("Information")]
    public abstract class Info
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public DateTime? StartDatum { get; set; }
        public DateTime? SlutDatum { get; set; }
        public int CvId { get; set; }
        [ForeignKey(nameof(CvId))]
        public virtual CV Cv { get; set; }
    }
}

