using System.ComponentModel.DataAnnotations.Schema;

namespace CVModels
{
    [Table("Information")]
    public abstract class Info
    {
        public string? Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public DateTime? StartDatum { get; set; }
        public DateTime? SlutDatum { get; set; }
        public int CvId { get; set; }
        [ForeignKey(nameof(CvId))]
        public virtual CV Cv { get; set; }
    }
}

