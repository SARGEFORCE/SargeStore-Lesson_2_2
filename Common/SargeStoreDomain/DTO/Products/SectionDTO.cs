using SargeStoreDomain.Entities.Base.Interfaces;

public class SectionDTO : INamedEntity
{
    public int Id { get; set; }

    public string Name { get; set; }
}
