using DatabaseAccess.Entities.Abstractions;

namespace DatabaseAccess.Entities
{
    public class MainFisitkaForProjectModel : BaseEntity
    {
        public Guid Id { get; set; }
        public int ConditionCode { get; set; }
        public ConditionModel Condition { get; set; }
        public int GenreId { get; set; }
        public GenreModel Genre { get; set; }
    }
}
